BEGIN;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('16e5af8e-71b8-4566-a6c4-9990ed446211'::uuid, 'Cơm tấm', 'Cơm', '{"soupy": -0.9, "temperature": 0.5, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": -0.2, "texture_complexity": 0.6, "time_required": -0.5, "novelty": -0.9, "healthy": -0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('78ec3239-e78c-4cc9-a783-a7600896fe29'::uuid, 'Cơm gà', 'Cơm', '{"soupy": -0.9, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.5, "spicy": -0.5, "texture_complexity": 0.5, "time_required": -0.5, "novelty": -0.8, "healthy": -0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ed63877f-5aa2-410a-9d9b-c4dd6474793f'::uuid, 'Cơm chiên', 'Cơm', '{"soupy": -0.9, "temperature": 0.8, "heaviness": 0.8, "flavor_intensity": 0.6, "spicy": -0.3, "texture_complexity": 0.6, "time_required": -0.4, "novelty": -0.9, "healthy": -0.6, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('106485fb-378b-4d80-8e78-7704c8bdb170'::uuid, 'Cơm văn phòng', 'Cơm', '{"soupy": -0.9, "temperature": 0.4, "heaviness": 0.6, "flavor_intensity": 0.4, "spicy": -0.5, "texture_complexity": 0.3, "time_required": -0.7, "novelty": -1.0, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('c7a79da3-2a13-41c8-bd21-ec467f5b17a7'::uuid, 'Cơm sườn nướng', 'Cơm', '{"soupy": -0.9, "temperature": 0.6, "heaviness": 0.8, "flavor_intensity": 0.8, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.5, "novelty": -0.8, "healthy": -0.3, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('e7b0a2d9-2581-4113-a931-f39f08dbe02b'::uuid, 'Cơm rang dưa bò', 'Cơm', '{"soupy": -0.8, "temperature": 0.8, "heaviness": 0.8, "flavor_intensity": 0.7, "spicy": -0.2, "texture_complexity": 0.6, "time_required": -0.4, "novelty": -0.6, "healthy": -0.4, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('91e20ce1-c018-4d5c-8b78-a093f1440548'::uuid, 'Cơm cháy chạo tôm', 'Cơm', '{"soupy": -1.0, "temperature": 0.5, "heaviness": 0.6, "flavor_intensity": 0.6, "spicy": -0.4, "texture_complexity": 0.9, "time_required": -0.3, "novelty": -0.4, "healthy": -0.3, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8602edd9-4c07-41a3-ae02-742f16c7bc04'::uuid, 'Cơm niêu', 'Cơm', '{"soupy": -0.9, "temperature": 0.9, "heaviness": 0.7, "flavor_intensity": 0.5, "spicy": -0.5, "texture_complexity": 0.8, "time_required": 0.4, "novelty": -0.3, "healthy": 0.1, "communal": 0.6}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('292bd453-6529-4274-b9b8-af4fed8ea731'::uuid, 'Cơm hến', 'Cơm', '{"soupy": -0.6, "temperature": 0.2, "heaviness": 0.4, "flavor_intensity": 0.8, "spicy": 0.7, "texture_complexity": 0.7, "time_required": -0.5, "novelty": -0.2, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('9d2f9baf-50ee-4c2b-9103-2da4e9df236e'::uuid, 'Cơm tay cầm', 'Cơm', '{"soupy": -0.8, "temperature": 0.9, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.3, "texture_complexity": 0.5, "time_required": 0.2, "novelty": -0.2, "healthy": 0.1, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('621dc429-b703-45f5-8baa-829029057a3f'::uuid, 'Cơm trộn Hàn Quốc', 'Cơm', '{"soupy": -0.7, "temperature": 0.6, "heaviness": 0.8, "flavor_intensity": 0.8, "spicy": 0.6, "texture_complexity": 0.6, "time_required": -0.3, "novelty": -0.1, "healthy": 0.3, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ab925aef-15b7-4b60-a20e-8ced7dd01009'::uuid, 'Kimbap', 'Cơm', '{"soupy": -1.0, "temperature": -0.3, "heaviness": 0.5, "flavor_intensity": 0.3, "spicy": -0.8, "texture_complexity": 0.4, "time_required": -0.8, "novelty": -0.4, "healthy": 0.4, "communal": -0.5}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('f8c3b38a-862c-42d7-92b8-0bc55dfc90a7'::uuid, 'Phở bò', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.1, "flavor_intensity": 0.6, "spicy": -0.3, "texture_complexity": 0.4, "time_required": 0.2, "novelty": -0.9, "healthy": 0.3, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('e73c65f0-4346-4d07-8ca1-5816e9b2883d'::uuid, 'Phở gà', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.1, "flavor_intensity": 0.4, "spicy": -0.5, "texture_complexity": 0.3, "time_required": 0.1, "novelty": -0.9, "healthy": 0.5, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('c2baaf46-4dbc-450e-832f-1ca6f2d2dbee'::uuid, 'Bún bò Huế', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.9, "heaviness": 0.5, "flavor_intensity": 0.9, "spicy": 0.7, "texture_complexity": 0.6, "time_required": 0.3, "novelty": -0.3, "healthy": 0.1, "communal": -0.7}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('db81ab9a-ef6c-4da9-9962-1b24ca1e68d7'::uuid, 'Bún riêu', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.7, "spicy": -0.2, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.7, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('b9f1fc61-e4cc-44c3-a7dd-db3a6ca9b3f9'::uuid, 'Bún mọc', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.8, "heaviness": 0.2, "flavor_intensity": 0.3, "spicy": -0.6, "texture_complexity": 0.4, "time_required": 0.1, "novelty": -0.8, "healthy": 0.4, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('9549caee-2a3e-47c4-b084-d5e92db79ad6'::uuid, 'Bún cá', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.5, "spicy": -0.3, "texture_complexity": 0.6, "time_required": 0.1, "novelty": -0.5, "healthy": 0.4, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('20342cca-3bf5-4f5b-9229-2ec1cbb59957'::uuid, 'Mì Quảng', 'Phở & Bún nước', '{"soupy": 0.2, "temperature": 0.6, "heaviness": 0.5, "flavor_intensity": 0.8, "spicy": 0.3, "texture_complexity": 0.7, "time_required": -0.2, "novelty": -0.4, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('73571140-b87e-48c5-985c-fb4e2c35e0b7'::uuid, 'Cao lầu', 'Phở & Bún nước', '{"soupy": 0.1, "temperature": 0.5, "heaviness": 0.6, "flavor_intensity": 0.7, "spicy": 0.2, "texture_complexity": 0.8, "time_required": -0.2, "novelty": -0.3, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8d91a500-a03f-4d94-b83c-c3eef2173689'::uuid, 'Hủ tiếu Nam Vang', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.7, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8e740ae4-24c0-495a-937e-e1da5de60e09'::uuid, 'Hủ tiếu Mỹ Tho', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.2, "texture_complexity": 0.6, "time_required": 0.1, "novelty": -0.6, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('223247a7-c7ae-43fe-87ed-6ca01b208c62'::uuid, 'Bánh canh', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.5, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": 0.2, "time_required": 0.1, "novelty": -0.6, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('80fa80cd-a642-4877-8a91-100e1f745fdd'::uuid, 'Mì vằn thắn', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.5, "texture_complexity": 0.6, "time_required": 0.1, "novelty": -0.6, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('56cec4aa-fc07-4cb7-be7f-28df4571d751'::uuid, 'Bún thịt nướng', 'Bún khô/trộn', '{"soupy": -0.7, "temperature": 0.2, "heaviness": 0.6, "flavor_intensity": 0.8, "spicy": 0.1, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.7, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('d83ed636-2589-4ae3-8dfa-78e509044b76'::uuid, 'Bún chả', 'Bún khô/trộn', '{"soupy": 0.3, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.8, "spicy": -0.1, "texture_complexity": 0.6, "time_required": 0.2, "novelty": -0.6, "healthy": -0.1, "communal": -0.4}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('2eba9e97-c17f-4460-b4d0-ae8c947e0699'::uuid, 'Bún đậu mắm tôm', 'Bún khô/trộn', '{"soupy": -0.9, "temperature": -0.2, "heaviness": 0.8, "flavor_intensity": 1.0, "spicy": 0.1, "texture_complexity": 0.8, "time_required": 0.3, "novelty": -0.5, "healthy": -0.3, "communal": 0.7}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('251c81df-c444-47db-a0e9-ca7fe1f2e2b8'::uuid, 'Bún nem', 'Bún khô/trộn', '{"soupy": -0.8, "temperature": 0.4, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": -0.2, "texture_complexity": 0.9, "time_required": -0.2, "novelty": -0.6, "healthy": -0.4, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8a824fbd-97d7-4dc0-b78e-30c0f3933f1f'::uuid, 'Miến trộn', 'Bún khô/trộn', '{"soupy": -0.8, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.3, "texture_complexity": 0.6, "time_required": -0.4, "novelty": -0.5, "healthy": 0.3, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('4c8c66bb-a646-4739-bb4f-a8ca50381057'::uuid, 'Bún mắm (nước)', 'Phở & Bún nước', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.8, "flavor_intensity": 1.0, "spicy": 0.4, "texture_complexity": 0.6, "time_required": 0.3, "novelty": -0.2, "healthy": -0.2, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('3b26d250-6614-42e3-a1ea-4285a8cc7c39'::uuid, 'Bánh mì thịt', 'Bánh', '{"soupy": -1.0, "temperature": 0.3, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": 0.3, "texture_complexity": 0.9, "time_required": -0.9, "novelty": -0.9, "healthy": -0.1, "communal": -1.0}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('d0190fa4-51b4-420e-8386-ef4241a1b562'::uuid, 'Bánh cuốn', 'Bánh', '{"soupy": -0.6, "temperature": 0.5, "heaviness": 0.3, "flavor_intensity": 0.4, "spicy": -0.6, "texture_complexity": -0.5, "time_required": -0.3, "novelty": -0.7, "healthy": 0.4, "communal": -0.7}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('5a999130-ec3e-4f47-8e35-b10aaf0d5636'::uuid, 'Bánh xèo', 'Bánh', '{"soupy": -0.9, "temperature": 0.8, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.9, "time_required": 0.2, "novelty": -0.5, "healthy": -0.3, "communal": 0.4}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('eabb661c-ac0b-4a83-b083-efe68df3b775'::uuid, 'Bánh tráng trộn', 'Bánh', '{"soupy": -1.0, "temperature": -0.1, "heaviness": 0.2, "flavor_intensity": 0.9, "spicy": 0.7, "texture_complexity": 0.8, "time_required": -0.5, "novelty": -0.4, "healthy": -0.5, "communal": 0.3}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('73245843-c77b-4453-b4a2-0e4c3085b926'::uuid, 'Bánh ướt', 'Bánh', '{"soupy": -0.7, "temperature": 0.2, "heaviness": 0.3, "flavor_intensity": 0.4, "spicy": -0.6, "texture_complexity": -0.6, "time_required": -0.4, "novelty": -0.7, "healthy": 0.3, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('be3e9638-613b-41e1-af0f-a26da15210a6'::uuid, 'Bánh khọt', 'Bánh', '{"soupy": -0.9, "temperature": 0.7, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.8, "time_required": -0.1, "novelty": -0.4, "healthy": -0.3, "communal": 0.4}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ff487652-b943-4107-ae11-3d0d19f2a6d5'::uuid, 'Cháo lòng', 'Cháo & Súp', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.6, "flavor_intensity": 0.7, "spicy": 0.1, "texture_complexity": 0.3, "time_required": 0.1, "novelty": -0.7, "healthy": -0.3, "communal": -0.6}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('4ad88b69-c42d-4a95-876b-1c27fc3522c4'::uuid, 'Cháo sườn', 'Cháo & Súp', '{"soupy": 0.9, "temperature": 0.8, "heaviness": 0.4, "flavor_intensity": 0.4, "spicy": -0.7, "texture_complexity": -0.8, "time_required": -0.2, "novelty": -0.8, "healthy": 0.2, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('883b5062-1a42-48eb-820f-e2fc57c47ce4'::uuid, 'Súp nui', 'Cháo & Súp', '{"soupy": 0.9, "temperature": 0.8, "heaviness": 0.4, "flavor_intensity": 0.3, "spicy": -0.8, "texture_complexity": -0.2, "time_required": -0.3, "novelty": -0.5, "healthy": 0.3, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('e734405a-8833-4ea6-8beb-5980dc7ea101'::uuid, 'Cháo vịt', 'Cháo & Súp', '{"soupy": 0.9, "temperature": 0.8, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": 0.2, "texture_complexity": 0.2, "time_required": 0.2, "novelty": -0.6, "healthy": 0.3, "communal": -0.5}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('59e826b6-7c03-45fc-a22e-4679abfc326f'::uuid, 'Xôi', 'Street food', '{"soupy": -1.0, "temperature": 0.5, "heaviness": 0.8, "flavor_intensity": 0.4, "spicy": -0.5, "texture_complexity": 0.4, "time_required": -0.8, "novelty": -0.9, "healthy": -0.2, "communal": -1.0}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('be31e376-b4c8-463e-90ae-6cfb64c758d6'::uuid, 'Gỏi cuốn', 'Street food', '{"soupy": -0.8, "temperature": -0.5, "heaviness": 0.2, "flavor_intensity": 0.4, "spicy": -0.4, "texture_complexity": 0.5, "time_required": -0.6, "novelty": -0.7, "healthy": 0.8, "communal": -0.3}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('07e9227f-8ac9-4476-87a1-eebda543fcc5'::uuid, 'Há cảo/Dimsum', 'Street food', '{"soupy": -0.5, "temperature": 0.7, "heaviness": 0.4, "flavor_intensity": 0.4, "spicy": -0.6, "texture_complexity": 0.3, "time_required": -0.2, "novelty": -0.3, "healthy": 0.2, "communal": 0.5}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('f446bc1c-ec8c-4b73-8473-49a629109cc8'::uuid, 'Hoành thánh', 'Street food', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.4, "spicy": -0.6, "texture_complexity": 0.2, "time_required": -0.3, "novelty": -0.6, "healthy": 0.3, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ef5a087f-2c55-4bfb-9e63-f1d3f32016e5'::uuid, 'Mì xào', 'Street food', '{"soupy": -0.9, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.3, "texture_complexity": 0.5, "time_required": -0.5, "novelty": -0.7, "healthy": -0.4, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('709c205d-e29c-4823-a4ff-5a6a7e6acf0d'::uuid, 'Hủ tiếu xào', 'Street food', '{"soupy": -0.8, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": -0.3, "texture_complexity": 0.6, "time_required": -0.4, "novelty": -0.6, "healthy": -0.3, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('57e4c0f1-f181-4402-9352-96a4acd87194'::uuid, 'Bò lúc lắc', 'Street food', '{"soupy": -0.8, "temperature": 0.8, "heaviness": 0.8, "flavor_intensity": 0.8, "spicy": -0.2, "texture_complexity": 0.5, "time_required": -0.1, "novelty": -0.4, "healthy": -0.2, "communal": 0.3}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('cb655a47-0295-4c3d-9f03-8fe8309a874b'::uuid, 'Cơm cuộn', 'Street food', '{"soupy": -1.0, "temperature": -0.2, "heaviness": 0.5, "flavor_intensity": 0.3, "spicy": -0.7, "texture_complexity": 0.4, "time_required": -0.8, "novelty": -0.4, "healthy": 0.4, "communal": -0.4}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8069c41d-65f4-4556-8829-d02e0598f6a5'::uuid, 'Salad bowl', 'Healthy', '{"soupy": -0.9, "temperature": -0.8, "heaviness": -0.6, "flavor_intensity": -0.3, "spicy": -0.8, "texture_complexity": 0.6, "time_required": -0.4, "novelty": 0.1, "healthy": 0.9, "communal": -0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('e723d181-21dd-4d8a-8824-146a2b0a01fa'::uuid, 'Sandwich', 'Healthy', '{"soupy": -1.0, "temperature": -0.4, "heaviness": 0.3, "flavor_intensity": 0.2, "spicy": -0.8, "texture_complexity": 0.3, "time_required": -0.8, "novelty": -0.2, "healthy": 0.4, "communal": -1.0}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('01a8c921-4a95-4236-9d09-490dce4cc0a4'::uuid, 'Healthy wrap', 'Healthy', '{"soupy": -1.0, "temperature": -0.3, "heaviness": 0.2, "flavor_intensity": 0.3, "spicy": -0.7, "texture_complexity": 0.5, "time_required": -0.7, "novelty": 0.2, "healthy": 0.8, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('6c9483a3-ab66-4e56-846e-6b0a2b70957d'::uuid, 'Poke bowl', 'Healthy', '{"soupy": -0.8, "temperature": -0.6, "heaviness": 0.2, "flavor_intensity": 0.4, "spicy": -0.4, "texture_complexity": 0.5, "time_required": -0.3, "novelty": 0.6, "healthy": 0.8, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('847bf30e-6845-473a-8b8c-cf039e5b3457'::uuid, 'Cơm gạo lứt', 'Healthy', '{"soupy": -0.9, "temperature": 0.4, "heaviness": 0.3, "flavor_intensity": -0.2, "spicy": -0.6, "texture_complexity": 0.6, "time_required": -0.4, "novelty": -0.3, "healthy": 0.9, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('bc8b923a-64ea-4f59-bf60-a1306eb1060c'::uuid, 'Lẩu mini', 'Chia sẻ', '{"soupy": 1.0, "temperature": 1.0, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": 0.4, "texture_complexity": 0.5, "time_required": 0.6, "novelty": -0.1, "healthy": 0.2, "communal": 0.3}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('96976e89-fe12-4bf4-8c57-eedc92e988b0'::uuid, 'Nướng BBQ', 'Chia sẻ', '{"soupy": -1.0, "temperature": 1.0, "heaviness": 0.9, "flavor_intensity": 0.9, "spicy": 0.3, "texture_complexity": 0.8, "time_required": 0.9, "novelty": 0.1, "healthy": -0.6, "communal": 0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('29f6563e-49c6-4dfe-9477-69f2f4708cc5'::uuid, 'Set cơm Nhật', 'Chia sẻ', '{"soupy": -0.5, "temperature": 0.4, "heaviness": 0.6, "flavor_intensity": 0.3, "spicy": -0.7, "texture_complexity": 0.5, "time_required": 0.3, "novelty": 0.2, "healthy": 0.6, "communal": -0.7}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('4a8f90a1-c4ff-4ae8-b929-adad2e824712'::uuid, 'Mì cay Hàn Quốc', 'Chia sẻ', '{"soupy": 0.9, "temperature": 1.0, "heaviness": 0.6, "flavor_intensity": 0.9, "spicy": 1.0, "texture_complexity": 0.6, "time_required": 0.2, "novelty": 0.1, "healthy": -0.3, "communal": -0.5}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('3fea11d1-ecad-4ba5-b10b-df20442cc3e5'::uuid, 'Tokbokki', 'Chia sẻ', '{"soupy": 0.4, "temperature": 0.9, "heaviness": 0.9, "flavor_intensity": 0.9, "spicy": 0.8, "texture_complexity": 0.8, "time_required": 0.4, "novelty": 0.3, "healthy": -0.5, "communal": 0.7}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ea268757-8920-440f-a8b6-05bf62bac37a'::uuid, 'Pasta / Mì Ý', 'Fusion', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.5, "texture_complexity": 0.4, "time_required": 0.3, "novelty": 0.4, "healthy": -0.2, "communal": -0.6}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('5d2a8c99-b929-4a6f-b278-e9754c287e68'::uuid, 'Burger', 'Fusion', '{"soupy": -1.0, "temperature": 0.6, "heaviness": 0.9, "flavor_intensity": 0.8, "spicy": -0.4, "texture_complexity": 0.5, "time_required": -0.5, "novelty": 0.3, "healthy": -0.7, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ca787209-2543-43d2-bfe5-e4a40aa7f32d'::uuid, 'Pizza', 'Fusion', '{"soupy": -1.0, "temperature": 0.7, "heaviness": 0.9, "flavor_intensity": 0.7, "spicy": -0.5, "texture_complexity": 0.6, "time_required": 0.2, "novelty": 0.2, "healthy": -0.6, "communal": 0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('87a9eaaf-d17f-4c5d-9441-5a157eb1cbbf'::uuid, 'Taco/Burrito', 'Fusion', '{"soupy": -1.0, "temperature": 0.4, "heaviness": 0.8, "flavor_intensity": 0.8, "spicy": 0.4, "texture_complexity": 0.7, "time_required": -0.4, "novelty": 0.7, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('60fc35fd-e246-409b-8296-2ca71c89213a'::uuid, 'Pad Thai', 'Fusion', '{"soupy": -0.7, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.8, "spicy": 0.4, "texture_complexity": 0.6, "time_required": 0.1, "novelty": 0.4, "healthy": 0.2, "communal": -0.5}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('98f78734-5a0c-4404-810d-aad34f4e3a2e'::uuid, 'Bò kho', 'Đặc sản vùng', '{"soupy": 0.7, "temperature": 0.9, "heaviness": 0.8, "flavor_intensity": 0.9, "spicy": 0.3, "texture_complexity": 0.5, "time_required": 0.2, "novelty": -0.3, "healthy": 0.1, "communal": -0.4}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ee6e1744-f0eb-4c79-802c-6fe799ad71c9'::uuid, 'Hủ tiếu gõ', 'Đặc sản vùng', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.3, "flavor_intensity": 0.4, "spicy": -0.3, "texture_complexity": 0.4, "time_required": -0.5, "novelty": -0.6, "healthy": 0.1, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('cc3b4294-c634-45a9-8254-302c23004dcd'::uuid, 'Bánh bèo', 'Đặc sản vùng', '{"soupy": -0.6, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": -0.3, "time_required": -0.2, "novelty": -0.1, "healthy": 0.2, "communal": 0.4}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('84b34a8f-ee04-4ab2-98dc-9abb507b9d35'::uuid, 'Bún chả cá', 'Đặc sản vùng', '{"soupy": 0.9, "temperature": 0.9, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": 0.1, "novelty": -0.2, "healthy": 0.5, "communal": -0.9}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('4b1064cb-53e8-4ae0-88d5-7761e1711efe'::uuid, 'Nem nướng Nha Trang', 'Đặc sản vùng', '{"soupy": -0.9, "temperature": 0.4, "heaviness": 0.7, "flavor_intensity": 0.8, "spicy": 0.2, "texture_complexity": 0.6, "time_required": 0.4, "novelty": 0.1, "healthy": 0.3, "communal": 0.8}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('492149f1-31c2-4a1e-a2b8-3a561ef3a646'::uuid, 'Gỏi khô bò', 'Đặc sản vùng', '{"soupy": -0.8, "temperature": -0.1, "heaviness": 0.2, "flavor_intensity": 0.8, "spicy": 0.6, "texture_complexity": 0.7, "time_required": -0.4, "novelty": -0.3, "healthy": -0.1, "communal": 0.2}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('3da9446a-5ad8-457b-94d9-4db3c3332444'::uuid, 'Cơm hấp lá sen', 'Đặc sản vùng', '{"soupy": -0.8, "temperature": 0.8, "heaviness": 0.6, "flavor_intensity": 0.4, "spicy": -0.6, "texture_complexity": 0.5, "time_required": 0.4, "novelty": 0.3, "healthy": 0.4, "communal": 0.3}'::jsonb, 1, NOW(), 'source_import', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('61baaef5-2009-53a7-a52c-f911bcb8a4d3'::uuid, 'Hủ tiếu cá', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.5, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('a5f733c2-9b6c-593c-92b4-3b0724b63fbf'::uuid, 'Hủ tiếu gà', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.5, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('9723cfe5-13b1-5cef-abec-74ce6e964919'::uuid, 'Hủ tiếu đặc biệt gà + cá', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.5, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('7b8ce7e9-1371-56c8-9155-ddf19dd80107'::uuid, 'Mì bò Đài Loan', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.5, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('5efb5a51-149e-5775-93e7-50efab2405b8'::uuid, 'Mì bò Hong Kong', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.5, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ae2bf5ae-9c4c-51f5-bd3e-8c6159eb078b'::uuid, 'Mì xào bò Mỹ', 'Xào/khô', '{"soupy": -0.8, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": 0.0, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.4, "healthy": -0.1, "communal": -0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('1d571d4b-9e46-5091-b895-a4e9453c14d2'::uuid, 'Mì xào hải sản', 'Xào/khô', '{"soupy": -0.8, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": 0.0, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.4, "healthy": -0.1, "communal": -0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('f5a7da20-1fcb-544d-a48c-f70c83758764'::uuid, 'Hủ tiếu xào bò', 'Xào/khô', '{"soupy": -0.8, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": 0.0, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.4, "healthy": -0.1, "communal": -0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('9283c41e-2192-546b-b55b-0a0adeca20db'::uuid, 'Hủ tiếu xào hải sản', 'Xào/khô', '{"soupy": -0.8, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": 0.0, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.4, "healthy": -0.1, "communal": -0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8e898450-943b-5b5a-b22f-5dbc1160914e'::uuid, 'Nui xào bò', 'Xào/khô', '{"soupy": -0.8, "temperature": 0.7, "heaviness": 0.7, "flavor_intensity": 0.7, "spicy": 0.0, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.4, "healthy": -0.1, "communal": -0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ccc7284c-418e-5ff8-a12c-d0eded1c090f'::uuid, 'Há cảo tôm', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('8be2305a-64fc-5bc9-94ef-2db302d35cfe'::uuid, 'Xíu mại', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('b8e3e48d-f374-5d97-bd1e-a20a11e849be'::uuid, 'Chân gà tàu xì', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('abd7a42f-c11b-50b5-931b-a85fc8955c13'::uuid, 'Bánh bao xá xíu', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('d4578c7c-85b2-5074-b79b-28ecac1ca39e'::uuid, 'Hoành thánh chiên', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('d8fcfb47-7ff1-55d2-95a9-533ad1c95338'::uuid, 'Vịt quay', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('5b633f94-eb34-5d95-aa23-1347f651633c'::uuid, 'Xá xíu', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('6e43856d-9804-50a4-8bd8-216f415d69fc'::uuid, 'Cơm chiên kiểu Hoa', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('d9106eea-a32f-50e1-b0de-5ca578c83096'::uuid, 'Sashimi', 'Nhật', '{"soupy": -0.2, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.2, "healthy": 0.3, "communal": -0.7}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('a430119f-3573-56ce-afac-1df0d84fab10'::uuid, 'Udon', 'Nhật', '{"soupy": -0.2, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.2, "healthy": 0.3, "communal": -0.7}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('04697e29-528e-5986-a88b-2f881c90902f'::uuid, 'Ramen', 'Nhật', '{"soupy": -0.2, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.2, "healthy": 0.3, "communal": -0.7}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('c010c1c5-50cd-57bd-95f9-810cd3321399'::uuid, 'Bento Nhật', 'Nhật', '{"soupy": -0.2, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.2, "healthy": 0.3, "communal": -0.7}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('17c4739c-39f4-55ce-8438-26a1389d4607'::uuid, 'Steak bò', 'Âu', '{"soupy": -0.5, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.1, "healthy": -0.1, "communal": -0.6}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('e23ae8c1-bba8-5b80-b4e0-0b9837822894'::uuid, 'Salad gà', 'Âu', '{"soupy": -0.5, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.1, "healthy": -0.1, "communal": -0.6}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('3ece5b0a-54e5-528a-b0fa-ff931fb6b4b5'::uuid, 'Soup Âu', 'Âu', '{"soupy": -0.5, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.1, "healthy": -0.1, "communal": -0.6}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('f88128fd-062e-5558-8dc2-b2e6ee57cf2c'::uuid, 'Lẩu nấm', 'Lẩu/Buffet', '{"soupy": 0.3, "temperature": 0.8, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": 0.1, "texture_complexity": 0.6, "time_required": 0.4, "novelty": -0.2, "healthy": 0.0, "communal": 0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('2b692e3c-2bfd-53f1-8569-235dbe8f1ced'::uuid, 'Lẩu Thái', 'Lẩu/Buffet', '{"soupy": 0.3, "temperature": 0.8, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": 0.1, "texture_complexity": 0.6, "time_required": 0.4, "novelty": -0.2, "healthy": 0.0, "communal": 0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('ecf30f0e-885e-5e73-8980-dadfa277ed08'::uuid, 'Buffet', 'Lẩu/Buffet', '{"soupy": 0.3, "temperature": 0.8, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": 0.1, "texture_complexity": 0.6, "time_required": 0.4, "novelty": -0.2, "healthy": 0.0, "communal": 0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('9b3137ae-98c7-5f21-b3b6-bf8e46334404'::uuid, 'Dimsum', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('2b7e8a9b-4ee4-5f27-93e2-4188e6d282cb'::uuid, 'Món Hoa', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('a17e7ed5-049d-5374-92e0-89054a714616'::uuid, 'Món Nhật', 'Nhật', '{"soupy": -0.2, "temperature": 0.3, "heaviness": 0.4, "flavor_intensity": 0.5, "spicy": -0.4, "texture_complexity": 0.6, "time_required": -0.2, "novelty": -0.2, "healthy": 0.3, "communal": -0.7}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('3dc817f3-e708-54fd-8871-ea0548f63def'::uuid, 'Món Âu', 'Âu', '{"soupy": -0.5, "temperature": 0.6, "heaviness": 0.7, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.1, "healthy": -0.1, "communal": -0.6}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('208da432-4f1e-59bc-8509-18a16c48f1ab'::uuid, 'Hải sản', 'Lẩu/Buffet', '{"soupy": 0.3, "temperature": 0.8, "heaviness": 0.5, "flavor_intensity": 0.6, "spicy": 0.1, "texture_complexity": 0.6, "time_required": 0.4, "novelty": -0.2, "healthy": 0.0, "communal": 0.8}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('66078685-00db-5e46-98f3-cdfc1747e40c'::uuid, 'Bánh bao', 'Dimsum & Hoa', '{"soupy": -0.4, "temperature": 0.6, "heaviness": 0.4, "flavor_intensity": 0.6, "spicy": -0.2, "texture_complexity": 0.7, "time_required": -0.3, "novelty": -0.2, "healthy": 0.0, "communal": 0.1}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

INSERT INTO dishes (id, name, category, profile, version, updated_at, updated_by, created_at)
VALUES ('088fab81-9662-5744-bd39-126a879e5e90'::uuid, 'Hủ tiếu', 'Phở & Bún nước', '{"soupy": 0.8, "temperature": 0.8, "heaviness": 0.3, "flavor_intensity": 0.6, "spicy": -0.1, "texture_complexity": 0.5, "time_required": 0.1, "novelty": -0.5, "healthy": 0.2, "communal": -0.9}'::jsonb, 1, NOW(), 'manual_normalization', NOW())
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    category = EXCLUDED.category,
    profile = EXCLUDED.profile,
    version = EXCLUDED.version,
    updated_at = NOW(),
    updated_by = EXCLUDED.updated_by;

COMMIT;
