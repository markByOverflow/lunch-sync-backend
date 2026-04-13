BEGIN;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('20ede67b-7b6d-5ce6-84a5-6f28e732c651'::uuid, '208da432-4f1e-59bc-8509-18a16c48f1ab'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('20ede67b-7b6d-5ce6-84a5-6f28e732c651'::uuid, '61baaef5-2009-53a7-a52c-f911bcb8a4d3'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('20ede67b-7b6d-5ce6-84a5-6f28e732c651'::uuid, '9723cfe5-13b1-5cef-abec-74ce6e964919'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('20ede67b-7b6d-5ce6-84a5-6f28e732c651'::uuid, 'a5f733c2-9b6c-593c-92b4-3b0724b63fbf'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, '106485fb-378b-4d80-8e78-7704c8bdb170'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, '208da432-4f1e-59bc-8509-18a16c48f1ab'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('28126343-2281-55b9-847f-4202af42c152'::uuid, '8be2305a-64fc-5bc9-94ef-2db302d35cfe'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('3c01647e-1632-508c-afb9-72bb18230f3b'::uuid, '088fab81-9662-5744-bd39-126a879e5e90'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('3c01647e-1632-508c-afb9-72bb18230f3b'::uuid, 'be31e376-b4c8-463e-90ae-6cfb64c758d6'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('4f4462bb-a7f0-5bab-be36-83e4f96f71d5'::uuid, 'f8c3b38a-862c-42d7-92b8-0bc55dfc90a7'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '106485fb-378b-4d80-8e78-7704c8bdb170'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '2b7e8a9b-4ee4-5f27-93e2-4188e6d282cb'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '5b633f94-eb34-5d95-aa23-1347f651633c'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '5efb5a51-149e-5775-93e7-50efab2405b8'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '66078685-00db-5e46-98f3-cdfc1747e40c'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '7b8ce7e9-1371-56c8-9155-ddf19dd80107'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, '9b3137ae-98c7-5f21-b3b6-bf8e46334404'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'abd7a42f-c11b-50b5-931b-a85fc8955c13'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'ae2bf5ae-9c4c-51f5-bd3e-8c6159eb078b'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'ed63877f-5aa2-410a-9d9b-c4dd6474793f'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('7b740dfa-3f76-5bde-8454-bb552cca420d'::uuid, 'ef5a087f-2c55-4bfb-9e63-f1d3f32016e5'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('979e9f3c-adca-5440-8ac5-b3a81f560f76'::uuid, '208da432-4f1e-59bc-8509-18a16c48f1ab'::uuid)
ON CONFLICT DO NOTHING;

INSERT INTO restaurant_dishes (restaurant_id, dish_id)
VALUES ('979e9f3c-adca-5440-8ac5-b3a81f560f76'::uuid, '96976e89-fe12-4bf4-8c57-eedc92e988b0'::uuid)
ON CONFLICT DO NOTHING;

COMMIT;
