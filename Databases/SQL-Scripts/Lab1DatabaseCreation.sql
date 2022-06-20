SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

CREATE SCHEMA biology;

SET default_tablespace = '';

SET default_table_access_method = heap;

CREATE TABLE biology.genus (
    id integer NOT NULL,
    name character varying NOT NULL,
    subfamily_id integer NOT NULL
);

CREATE SEQUENCE biology.genus_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE biology.genus_id_seq OWNED BY biology.genus.id;

CREATE TABLE biology.species (
    id integer NOT NULL,
    name character varying NOT NULL,
    genus_id integer NOT NULL
);

CREATE SEQUENCE biology.species_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE biology.species_id_seq OWNED BY biology.species.id;

CREATE TABLE biology.subfamily (
    id integer NOT NULL,
    name character varying NOT NULL
);

CREATE SEQUENCE biology.subfamily_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE biology.subfamily_id_seq OWNED BY biology.subfamily.id;

ALTER TABLE ONLY biology.genus ALTER COLUMN id SET DEFAULT nextval('biology.genus_id_seq'::regclass);

ALTER TABLE ONLY biology.species ALTER COLUMN id SET DEFAULT nextval('biology.species_id_seq'::regclass);

ALTER TABLE ONLY biology.subfamily ALTER COLUMN id SET DEFAULT nextval('biology.subfamily_id_seq'::regclass);

INSERT INTO biology.genus (id, name, subfamily_id) VALUES (1, 'Дымчатые леопарды', 1);
INSERT INTO biology.genus (id, name, subfamily_id) VALUES (2, 'Пантеры', 1);
INSERT INTO biology.genus (id, name, subfamily_id) VALUES (3, 'Гепарды', 2);
INSERT INTO biology.genus (id, name, subfamily_id) VALUES (4, 'Кошки', 2);
INSERT INTO biology.genus (id, name, subfamily_id) VALUES (5, 'Рыси', 2);

INSERT INTO biology.species (id, name, genus_id) VALUES (2, 'Манул', 4);
INSERT INTO biology.species (id, name, genus_id) VALUES (3, 'Канадская рысь', 5);
INSERT INTO biology.species (id, name, genus_id) VALUES (4, 'Рыжая рысь', 5);
INSERT INTO biology.species (id, name, genus_id) VALUES (5, 'Лесная кошка', 4);
INSERT INTO biology.species (id, name, genus_id) VALUES (6, 'Лев', 1);
INSERT INTO biology.species (id, name, genus_id) VALUES (7, 'Тигр', 1);

INSERT INTO biology.subfamily (id, name) VALUES (1, 'Большие кошки');
INSERT INTO biology.subfamily (id, name) VALUES (2, 'Малые кошки');
INSERT INTO biology.subfamily (id, name) VALUES (3, 'Саблезубые кошки');
INSERT INTO biology.subfamily (id, name) VALUES (4, 'Проаилурус');

SELECT pg_catalog.setval('biology.genus_id_seq', 5, true);

SELECT pg_catalog.setval('biology.species_id_seq', 7, true);

SELECT pg_catalog.setval('biology.subfamily_id_seq', 4, true);

ALTER TABLE ONLY biology.genus
    ADD CONSTRAINT genus_pk PRIMARY KEY (id);

ALTER TABLE ONLY biology.species
    ADD CONSTRAINT species_pk PRIMARY KEY (id);

ALTER TABLE ONLY biology.subfamily
    ADD CONSTRAINT subfamily_pk PRIMARY KEY (id);

CREATE UNIQUE INDEX genus_id_uindex ON biology.genus USING btree (id);

CREATE UNIQUE INDEX genus_name_uindex ON biology.genus USING btree (name);

CREATE UNIQUE INDEX species_id_uindex ON biology.species USING btree (id);

CREATE UNIQUE INDEX species_name_uindex ON biology.species USING btree (name);

CREATE UNIQUE INDEX subfamily_id_uindex ON biology.subfamily USING btree (id);

CREATE UNIQUE INDEX subfamily_name_uindex ON biology.subfamily USING btree (name);

ALTER TABLE ONLY biology.genus
    ADD CONSTRAINT genus_subfamily_id_fk FOREIGN KEY (subfamily_id) REFERENCES biology.subfamily(id);

ALTER TABLE ONLY biology.species
    ADD CONSTRAINT species_genus_id_fk FOREIGN KEY (genus_id) REFERENCES biology.genus(id);