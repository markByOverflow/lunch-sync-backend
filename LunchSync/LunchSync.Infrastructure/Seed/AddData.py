import json
import os
import psycopg2
import uuid
from psycopg2.extras import execute_values, Json

# Namespace để tạo UUID (có thể chọn bất kỳ UUID hợp lệ nào làm gốc)
NAMESPACE_RESTAURANT = uuid.UUID('12345678-1234-5678-1234-567812345678')

DB_CONFIG = {
    "host": os.getenv("DB_HOST", "localhost"),        # Mặc định là localhost nếu không có biến môi trường
    "database": os.getenv("DB_NAME", "lunchsync_db"),
    "user": os.getenv("DB_USER", "lunchsync"),
    "password": os.getenv("DB_PASS", "Password@123"),
    "port": int(os.getenv("DB_PORT", 5432))
}

def generate_uuid_from_id(old_id):
    """Tạo UUID cố định từ ID số của nhà hàng"""
    return str(uuid.uuid5(NAMESPACE_RESTAURANT, str(old_id)))

def migrate_data():
    conn = psycopg2.connect(**DB_CONFIG)
    cur = conn.cursor()

    try:
        # 1. INSERT DISHES (Dữ liệu đã có UUID sẵn trong file)
        with open('dishes.json', 'r', encoding='utf-8') as f:
            dishes = json.load(f)

        dish_values = [
            (d['id_dish'], d['name_dish'], d['category_dish'], Json(d['profile_dish']))
            for d in dishes
        ]
        execute_values(cur, """
            INSERT INTO dishes (id, name, category, profile)
            VALUES %s ON CONFLICT (name) DO NOTHING
        """, dish_values)
        print("✓ Đã chèn xong dishes.")

        # 2. INSERT RESTAURANTS (Convert ID số -> UUID)
        with open('district1restaurants.json', 'r', encoding='utf-8') as f:
            dist1 = json.load(f)

        res_values = []
        for cat in dist1.get('catData', []):
            for r in cat.get('restaurant'):
                # Tạo UUID từ restaurant_id cũ
                new_uuid = generate_uuid_from_id(r['restaurant_id'])

                # Logic phân loại giá (price_tier)
                max_p = r.get('shopeefood_data', {}).get('price_range', {}).get('max_price', 0)
                tier = 'Under40k'
                if max_p > 120000: tier = 'Over120k'
                elif max_p > 70000: tier = 'From70To120k'
                elif max_p > 40000: tier = 'From40To70k'

                res_values.append((
                    new_uuid, r['name'], r['address'],
                    r['avg_rating_foody'], tier, r.get('url_photo')
                ))

        execute_values(cur, """
            INSERT INTO restaurants (id, name, address, rating, price_tier, thumbnail_url)
            VALUES %s ON CONFLICT (id) DO NOTHING
        """, res_values)
        print(f"✓ Đã chèn xong {len(res_values)} restaurants.")

        # 3. INSERT RESTAURANT_DISHES (Bảng trung gian)
        with open('RestaurantDishSeedData.json', 'r', encoding='utf-8') as f:
            seed_data = json.load(f)

        rel_values = []
        for item in seed_data:
            # Dùng cùng hàm generate_uuid_from_id để lấy đúng UUID đã tạo ở bước 2
            res_uuid = generate_uuid_from_id(item['restaurant_id'])
            for d_id in item['dishes']:
                rel_values.append((res_uuid, d_id))

        execute_values(cur, """
            INSERT INTO restaurant_dishes (restaurant_id, dish_id)
            VALUES %s ON CONFLICT DO NOTHING
        """, rel_values)
        print(f"✓ Đã map xong {len(rel_values)} quan hệ nhà hàng - món ăn.")

        # 4. INSERT COLLECTIONS & RESTAURANT_COLLECTIONS
        with open('Collections.json', 'r', encoding='utf-8') as f:
            collections_data = json.load(f)

        col_values = []
        rel_col_values = []

        for col in collections_data:
            col_id = col['collection_id'] # Giả định ID trong JSON đã là UUID như file của bạn

            # Dữ liệu bảng collections
            col_values.append((
                col_id,
                col['collection_name'],
                f"Bộ sưu tập gần {col['collection_name']}", # Description tạm thời
                col['collection_lat'],
                col['collection_lon'],
                250 # coverage_radius_meters mặc định
            ))

            # Dữ liệu bảng trung gian restaurant_collections
            for res in col.get('restaurants', []):
                # Quan trọng: Dùng lại hàm tạo UUID từ ID số để khớp với bảng restaurants
                res_uuid = generate_uuid_from_id(res['restaurant_id'])
                rel_col_values.append((res_uuid, col_id))

        # Thực thi chèn vào bảng collections
        execute_values(cur, """
            INSERT INTO collections (id, name, description, landmark_lat, landmark_lng, coverage_radius_meters)
            VALUES %s ON CONFLICT (id) DO NOTHING
        """, col_values)
        print(f"✓ Đã chèn xong {len(col_values)} collections.")

        # Thực thi chèn vào bảng trung gian restaurant_collections
        if rel_col_values:
            execute_values(cur, """
                INSERT INTO restaurant_collections (restaurant_id, collection_id)
                VALUES %s ON CONFLICT DO NOTHING
            """, rel_col_values)
            print(f"✓ Đã map xong {len(rel_col_values)} quan hệ nhà hàng - bộ sưu tập.")

        conn.commit()
    except Exception as e:
        conn.rollback()
        print(f"✗ Lỗi rồi: {e}")
    finally:
        cur.close()
        conn.close()

if __name__ == "__main__":
    migrate_data()
