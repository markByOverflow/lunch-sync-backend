BEGIN;

INSERT INTO collections (id, name, description, landmark_lat, landmark_lng, coverage_radius_meters, status, created_at)
VALUES ('94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid, 'Bitexco Tower', NULL, 10.771706, 106.704375, 250, 'active', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    description = EXCLUDED.description,
    landmark_lat = EXCLUDED.landmark_lat,
    landmark_lng = EXCLUDED.landmark_lng,
    coverage_radius_meters = EXCLUDED.coverage_radius_meters,
    status = EXCLUDED.status;

INSERT INTO collections (id, name, description, landmark_lat, landmark_lng, coverage_radius_meters, status, created_at)
VALUES ('f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid, 'Sunwah Tower', NULL, 10.773818, 106.703162, 250, 'active', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    description = EXCLUDED.description,
    landmark_lat = EXCLUDED.landmark_lat,
    landmark_lng = EXCLUDED.landmark_lng,
    coverage_radius_meters = EXCLUDED.coverage_radius_meters,
    status = EXCLUDED.status;

INSERT INTO collections (id, name, description, landmark_lat, landmark_lng, coverage_radius_meters, status, created_at)
VALUES ('c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid, 'Times Square', NULL, 10.773906, 106.70472, 250, 'active', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    description = EXCLUDED.description,
    landmark_lat = EXCLUDED.landmark_lat,
    landmark_lng = EXCLUDED.landmark_lng,
    coverage_radius_meters = EXCLUDED.coverage_radius_meters,
    status = EXCLUDED.status;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('8617c0d9-5337-586f-b184-3de75fd5e3e3'::uuid, 'Buffet Liberty Central Saigon Riverside', '17 Tôn Đức Thắng, Quận 1, TP. HCM', NULL, '40_70k', 7.282, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lw9qkqq433rv4b@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('848bd4a5-6305-5e45-a520-305c6a61edbe'::uuid, 'The Royal Pavilion - Long Triều - Nhà Hàng Hoa', 'Tầng 4 Times Square, 57 – 69F Đồng Khởi (22 - 36 Nguyễn Huệ), Quận 1, TP. HCM', NULL, 'over_120k', 7.054, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwhi5jgpzye392@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('05c4e799-54df-5cc4-9f29-9f6b6bbe1afe'::uuid, 'Kabin Restaurant - Renaissance Riverside Hotel', 'Renaissance Riverside Hotel, 8 - 15 Tôn Đức Thắng, Quận 1, TP. HCM', NULL, 'over_120k', 7.156, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lw9ysr6hw063d1@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('58074ce8-6d99-5788-a53f-7aa88f0d7c37'::uuid, 'Rooftop Grand Lounge', 'Tầng 20 Grand Hotel, 8 Đồng Khởi, Quận 1, TP. HCM', NULL, 'over_120k', 7.59, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwg17mhyrjrt22@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('66248e15-4a89-5779-b8c8-cdae2c390dc2'::uuid, 'Stoker Woodfired Grill & Bar', '44 Mạc Thị Bưởi, Quận 1, TP. HCM', NULL, 'over_120k', 6.95, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwgjri5rtfixd0@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('e8558584-acce-58ce-9ed4-9801541fef98'::uuid, 'SH Garden - Sơn Hà Quán - Đồng Khởi', '26 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 7.2, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwbmyrlwmti1fa@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('d958c292-7baa-5806-a456-4bb867acb600'::uuid, 'Nhân Sushi Bito - Fresh Sashimi', '62 Ngô Đức Kế, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 6.956, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lw8j9qqsrwm3fb@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('4ac16e89-e72b-5be9-87c6-c871fb0711da'::uuid, 'Maxim''s - Nhà Hàng Sang Trọng', '13 - 15 - 17 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 6.894, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwdt7mujhuxn9d@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('2b74249d-be51-5e16-b198-8b22ca8ba15c'::uuid, 'Việt Kitchen Buffet - Renaissance Riverside Hotel', '8 - 15 Tôn Đức Thắng, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 7.404, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lw8otuftfi6x76@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('7c912e56-2bed-5ac0-a86f-3e364980b33d'::uuid, 'EON 52 Heli Bar - Bitexco Tower', 'Tầng 52, Bitexco Tower, 2 Hải Triều, Quận 1, TP. HCM', NULL, 'over_120k', 7.576, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwe6d7wo58m13c@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('55cf75bd-6d2d-56c1-96a0-9a298cafc2c2'::uuid, 'R&J - Italian Lounge & Restaurant', '57-69F Đồng Khởi (22-26 Nguyễn Huệ), Quận 1, TP. HCM', NULL, 'over_120k', 8.688, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwhcaj0ygrmj31@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('a6cf10d4-b572-57c5-b26f-9bfe7fc888ab'::uuid, 'ROSÉ Brasserie - Đồng Khởi', '8 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 8.102, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwa4pqc9qdwb9d@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('672603cd-e16b-5ee4-bab2-a99c385d8840'::uuid, 'Saigon Cafe - Sheraton Saigon Hotel & Towers', 'Tầng 1 Sheraton Saigon Hotel & Towers, 88 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 7.936, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwgdvlzeqajfe6@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('64099db5-9555-5a8c-8675-4124b668b19c'::uuid, 'Lobby Lounge - Sheraton Saigon Hotel & Towers', 'Sheraton Saigon Hotel & Towers, 88 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 7.914, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwdhnyjhtvizf0@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('0f1c6700-1c69-5a0f-bf55-fae522d931e5'::uuid, 'Mãn Tự Vegan - Quán Chay', '14/2 Tôn Thất Đạm, P. Nguyễn Thái Bình, Quận 1, TP. HCM', NULL, '40_70k', 7.482, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwer1uiry8d78c@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('097f76d4-3170-5843-a0c5-980f80154272'::uuid, 'Spicy Box - Tokbokki Hot Pot - Saigon Centre', 'Lầu B2 Saigon Centre, 65 Lê Lợi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 7.032, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwal66doqa57f2@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('0b36aa24-5b6d-529a-8085-3269b0f5b81c'::uuid, 'Oscar Saigon Buffet - Nguyễn Huệ', '68 Nguyễn Huệ, Quận 1, TP. HCM', NULL, '40_70k', 6.722, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwd1ctcyv2sbe1@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('badc125d-f994-5441-8bf9-b4334f8918ba'::uuid, 'Buffet Gánh 3 Miền - Khách Sạn Bông Sen', '117 - 123 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 6.854, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwaoqemjdgwr67@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('bd795354-3620-5485-83b4-cb7f6c964dfc'::uuid, 'Buffet Nướng - Khách Sạn Hương Sen', '66 - 70 Đồng Khởi, Quận 1, TP. HCM', NULL, '40_70k', 7.582, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lweqyilpik0p6e@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('db26c99b-36dd-5c95-ab48-a0b8e29bcafd'::uuid, 'Riverside Cafe Buffet - Renaissance Riverside Hotel', 'Renaissance Riverside Hotel, 8 - 15 Tôn Đức Thắng, Quận 1, TP. HCM', NULL, '40_70k', 7.612, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lw8eshv3h52152@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('5d76e93c-55ff-510b-b09f-4b3453649711'::uuid, 'Buffet Gánh - Palace Hotel Saigon', 'Tầng 05, Khách sạn Palace Sài Gòn, 56 - 66  Nguyễn Huệ, Quận 1, TP. HCM', NULL, '40_70k', 7.22, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwar9m3egdej90@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('b6b0713f-e632-5a8b-8f36-28806a4565b4'::uuid, 'The Grill Restaurant Buffet - Duxton Hotel', '63 Nguyễn Huệ, Quận 1, TP. HCM', NULL, '40_70k', 6.978, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwf9nwf4fmxn4e@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('b574c351-ebba-54dc-9634-7d937c5eb1ec'::uuid, 'Krauts - German Beer & BBQ', '62 Bis Huỳnh Thúc Kháng, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 3.896, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwaf4o1pfniz45@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('b92734fe-2db8-50f5-8f71-91e9e540a14e'::uuid, 'Lạc Thái 4 - Mì Quảng Lạc', '71/6 Mạc Thị Bưởi, Quận 1, TP. HCM', NULL, '40_70k', 6.4, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwba40c9gdln69@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('979e9f3c-adca-5440-8ac5-b3a81f560f76'::uuid, 'Lạc Thái 1', '71/2 Mạc Thị Bưởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 7.324, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lstpmjnxyvgk0e@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'Baozi - Ẩm Thực Đài Loan - Hồ Tùng Mậu', '79 Hồ Tùng Mậu, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 6.38, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lsu9go24uhllab@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('aa645a39-cb4a-5502-8dff-ddab2c06a082'::uuid, 'Osaka Ohsho Saigon Center - Quán Mì Ramen - Gyoza Nhật Bản', 'Lầu 5, Saigon Centre, 65 Lê Lợi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 7.36, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwacr86ml6mx3d@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('5746ade3-4342-5123-b89e-a4de35399640'::uuid, 'Tsukiji Nakajima Suisan - Sushi & Sashimi - TTTM Takashimaya', 'Tầng Hầm 2 - TTTM Takashimaya, 92 - 94 Nam Kỳ Khởi Nghĩa, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 7.104, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwcy0s6t1nmh55@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('85123d3c-4397-56eb-89e5-61ed1f815a9f'::uuid, 'Nhà Hàng Sài Gòn Thái - Vi Cá & Hải Sản', '54 - 56 Mạc Thị Bưởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 6.95, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwapivwt168pa6@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('3c01647e-1632-508c-afb9-72bb18230f3b'::uuid, 'Măng’s Mania - Ẩm Thực Chay', 'Lầu 1, 86 Nguyễn Huệ, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 5.998, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lsu0vdxmahfobf@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('7f7d99f6-19ae-5966-9361-c76d67ed8949'::uuid, 'Maha Vegan - Nhà Hàng Chay', '16 Pasteur, P. Nguyễn Thái Bình, Quận 1, TP. HCM', NULL, '40_70k', 6.8, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwhpa36optqx53@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('a1ffd1b1-2093-58e6-9600-59e201a23f57'::uuid, 'Cỏ Nội Vegetarian - Bông Sen Hotel', 'Tầng 7 Bông Sen Hotel, 117 - 123 Đồng Khởi, Quận 1, TP. HCM', NULL, '40_70k', 8.312, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwbvvtdj8nvff3@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('986e79c8-3f7f-5133-9816-c9bbd63f059f'::uuid, 'Thanh Xuân - Hủ Tiếu Cua Mỹ Tho', '62 Tôn Thất THiệp, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 5.3, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lstq0npuj7vd05@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('2429a718-9fb5-595e-b121-7d46bafcc4d9'::uuid, 'Trà Sữa MayCha - Nguyễn Huệ', '90 Nguyễn Huệ, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'under_40k', 7.818, 'https://down-vn.img.susercontent.com/vn-11134513-81ztc-mkkt617zp79h23@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('6ae0a9e8-fa7f-51cb-adb6-a7af2c8a9a31'::uuid, 'Bigbro Korean Hotdog', '85 Hồ Tùng Mậu, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 7.616, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lxavz13ymbizfd@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('4f4462bb-a7f0-5bab-be36-83e4f96f71d5'::uuid, 'Phở SOL - Hải Triều', '27 Hải Triều, P. Bến Nghé, Quận 1, TP. HCM', NULL, 'over_120k', 6.6, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lswi39lj7fdl55@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('20ede67b-7b6d-5ce6-84a5-6f28e732c651'::uuid, 'Nam Lợi Quán - Hủ Tiếu Mì Cá', '43 Tôn Thất Đạm, P. Nguyễn Thái Bình, Quận 1, TP. HCM', NULL, 'over_120k', 4.678, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lstqcntw5fb846@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('4c41ed38-50f3-540e-bffd-555b115480fa'::uuid, 'Dynasty House - HongKong Dimsum & Hotpot', 'Tầng Trệt, 2-4-6 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 9.754, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwamjnbyheqh5f@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('ebf30fd5-2c40-552a-81d0-27af0bde23ff'::uuid, 'Moo Beef Steak - Đồng Khởi', '44 Đồng Khởi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 8.204, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lw9iulx2m6sbec@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('e7d089b4-ff92-55de-90ef-392de0d80a67'::uuid, 'McDonald''s - Saigon Centre', 'Saigon Centre, 65 Lê Lợi, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 7.158, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwfehmj4c7obda@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('e3ddefb0-7f9d-5d4f-81d6-3bae421c5d96'::uuid, 'Hội Quán CK - Nguyễn Công Trứ', '52 Nguyễn Công Trứ, Quận 1, TP. HCM', NULL, '40_70k', 7.2, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwh8rw7rum6x89@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('d2b234e8-d7ec-594f-a6d5-7e8bd240c2d4'::uuid, 'Ăn Vặt 3 Giáp - Shop Online', '129 Tôn Thất Đạm, P. Bến Nghé, Quận 1, TP. HCM', NULL, '40_70k', 6.1, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lsu521wqyjes32@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, 'H&M Café', 'Lầu 3 Chung cư 40E, Ngô Đức Kế, P. Bến Nghé, Quận 1, TP. HCM', NULL, '70_120k', 7.668, 'https://down-vn.img.susercontent.com/vn-11134513-7r98o-lsu16da5qvus2c@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('68f24b1a-5816-5384-ab7c-fadc6f6a65b5'::uuid, 'Chấm Đỏ - Bitexco Tower', 'Tầng 4 Food Court Bitexco Tower, 2 Hải Triều, Quận 1, TP. HCM', NULL, '40_70k', 6.25, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwhlfs4qvqhn66@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurants (id, name, address, google_maps_url, price_tier, rating, thumbnail_url, status, source, created_at)
VALUES ('41eb90fe-8c0c-52e6-90c1-b3f609a95bb2'::uuid, 'The World Of Heineken', '36 Hồ Tùng Mậu, Quận 1, TP. HCM', NULL, '40_70k', 8.422, 'https://down-vn.img.susercontent.com/vn-11134259-7r98o-lwhhvyowkk4994@resize_ss640x400', 'active', 'admin', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    address = EXCLUDED.address,
    google_maps_url = EXCLUDED.google_maps_url,
    price_tier = EXCLUDED.price_tier,
    rating = EXCLUDED.rating,
    thumbnail_url = EXCLUDED.thumbnail_url,
    status = EXCLUDED.status,
    source = EXCLUDED.source;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('d958c292-7baa-5806-a456-4bb867acb600'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('b6b0713f-e632-5a8b-8f36-28806a4565b4'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('20ede67b-7b6d-5ce6-84a5-6f28e732c651'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('7c912e56-2bed-5ac0-a86f-3e364980b33d'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('55cf75bd-6d2d-56c1-96a0-9a298cafc2c2'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('848bd4a5-6305-5e45-a520-305c6a61edbe'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('e3ddefb0-7f9d-5d4f-81d6-3bae421c5d96'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('0f1c6700-1c69-5a0f-bf55-fae522d931e5'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('7f7d99f6-19ae-5966-9361-c76d67ed8949'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('41eb90fe-8c0c-52e6-90c1-b3f609a95bb2'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('68f24b1a-5816-5384-ab7c-fadc6f6a65b5'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('6ae0a9e8-fa7f-51cb-adb6-a7af2c8a9a31'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('d2b234e8-d7ec-594f-a6d5-7e8bd240c2d4'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('4f4462bb-a7f0-5bab-be36-83e4f96f71d5'::uuid, '94bb6f77-c32f-476b-808b-2ae9be975df8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('85123d3c-4397-56eb-89e5-61ed1f815a9f'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('979e9f3c-adca-5440-8ac5-b3a81f560f76'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('db26c99b-36dd-5c95-ab48-a0b8e29bcafd'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('64099db5-9555-5a8c-8675-4124b668b19c'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('4ac16e89-e72b-5be9-87c6-c871fb0711da'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('05c4e799-54df-5cc4-9f29-9f6b6bbe1afe'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('d958c292-7baa-5806-a456-4bb867acb600'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('58074ce8-6d99-5788-a53f-7aa88f0d7c37'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('b6b0713f-e632-5a8b-8f36-28806a4565b4'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('672603cd-e16b-5ee4-bab2-a99c385d8840'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('0b36aa24-5b6d-529a-8085-3269b0f5b81c'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('7c912e56-2bed-5ac0-a86f-3e364980b33d'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('5d76e93c-55ff-510b-b09f-4b3453649711'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('bd795354-3620-5485-83b4-cb7f6c964dfc'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('8617c0d9-5337-586f-b184-3de75fd5e3e3'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('55cf75bd-6d2d-56c1-96a0-9a298cafc2c2'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('848bd4a5-6305-5e45-a520-305c6a61edbe'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('ebf30fd5-2c40-552a-81d0-27af0bde23ff'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('66248e15-4a89-5779-b8c8-cdae2c390dc2'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('badc125d-f994-5441-8bf9-b4334f8918ba'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('b92734fe-2db8-50f5-8f71-91e9e540a14e'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('2b74249d-be51-5e16-b198-8b22ca8ba15c'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('e8558584-acce-58ce-9ed4-9801541fef98'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('41eb90fe-8c0c-52e6-90c1-b3f609a95bb2'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('68f24b1a-5816-5384-ab7c-fadc6f6a65b5'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('a1ffd1b1-2093-58e6-9600-59e201a23f57'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('6ae0a9e8-fa7f-51cb-adb6-a7af2c8a9a31'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('4c41ed38-50f3-540e-bffd-555b115480fa'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('a6cf10d4-b572-57c5-b26f-9bfe7fc888ab'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('4f4462bb-a7f0-5bab-be36-83e4f96f71d5'::uuid, 'c8b61171-e99f-4d76-ad0e-f088574d2cc1'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('979e9f3c-adca-5440-8ac5-b3a81f560f76'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('64099db5-9555-5a8c-8675-4124b668b19c'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('d958c292-7baa-5806-a456-4bb867acb600'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('986e79c8-3f7f-5133-9816-c9bbd63f059f'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('b6b0713f-e632-5a8b-8f36-28806a4565b4'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('672603cd-e16b-5ee4-bab2-a99c385d8840'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('0b36aa24-5b6d-529a-8085-3269b0f5b81c'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('5d76e93c-55ff-510b-b09f-4b3453649711'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('bd795354-3620-5485-83b4-cb7f6c964dfc'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('55cf75bd-6d2d-56c1-96a0-9a298cafc2c2'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('848bd4a5-6305-5e45-a520-305c6a61edbe'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('ebf30fd5-2c40-552a-81d0-27af0bde23ff'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('badc125d-f994-5441-8bf9-b4334f8918ba'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('b574c351-ebba-54dc-9634-7d937c5eb1ec'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('b92734fe-2db8-50f5-8f71-91e9e540a14e'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('e7d089b4-ff92-55de-90ef-392de0d80a67'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('aa645a39-cb4a-5502-8dff-ddab2c06a082'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('5746ade3-4342-5123-b89e-a4de35399640'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('3c01647e-1632-508c-afb9-72bb18230f3b'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('e8558584-acce-58ce-9ed4-9801541fef98'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('41eb90fe-8c0c-52e6-90c1-b3f609a95bb2'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('68f24b1a-5816-5384-ab7c-fadc6f6a65b5'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('a1ffd1b1-2093-58e6-9600-59e201a23f57'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('6ae0a9e8-fa7f-51cb-adb6-a7af2c8a9a31'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('d2b234e8-d7ec-594f-a6d5-7e8bd240c2d4'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('097f76d4-3170-5843-a0c5-980f80154272'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_collections (restaurant_id, collection_id)
VALUES ('2429a718-9fb5-595e-b121-7d46bafcc4d9'::uuid, 'f21c4447-ee6d-4e39-93dc-58cf905eefff'::uuid)
ON CONFLICT DO NOTHING;

COMMIT;
