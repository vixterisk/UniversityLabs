--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1
-- Dumped by pg_dump version 12.2

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

--
-- Name: postgres; Type: DATABASE; Schema: -; Owner: postgres
--

--CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


ALTER DATABASE postgres OWNER TO postgres;

--\connect postgres

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

--
-- Name: DATABASE postgres; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- Name: pcbs; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA pcbs;


ALTER SCHEMA pcbs OWNER TO postgres;

--
-- Name: mod_table_instead_of_delete(); Type: FUNCTION; Schema: pcbs; Owner: postgres
--

CREATE FUNCTION pcbs.mod_table_instead_of_delete() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    video_card_id_var integer;
    pc_id_var integer;
BEGIN
    SELECT video_card_id from video_card_information where old.video_card_model = video_card_information.video_card_model INTO video_card_id_var;
    SELECT pc_id from pc_information where old.pc_name = pc_information.pc_name INTO pc_id_var;
    delete from pc_video_card where pc_id_var = pc_video_card.pc_id and video_card_id_var=pc_video_card.video_card_id and pc_video_card.video_card_slot_number = old.video_card_slot_number;
    RETURN old;
END;
$$;


ALTER FUNCTION pcbs.mod_table_instead_of_delete() OWNER TO postgres;

--
-- Name: mod_table_instead_of_insert(); Type: FUNCTION; Schema: pcbs; Owner: postgres
--

CREATE FUNCTION pcbs.mod_table_instead_of_insert() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    video_card_id_var integer;
    pc_id_var integer;
BEGIN
    SELECT video_card_id from video_card_information where new.video_card_model = video_card_information.video_card_model INTO video_card_id_var;
    if video_card_id_var is NULL then
        insert into video_card_information(video_card_model) values (new.video_card_model) returning video_card_id INTO video_card_id_var;
    end if;
    SELECT pc_id from pc_information where new.pc_name = pc_information.pc_name INTO pc_id_var;
    if pc_id_var is NULL then
        insert into pc_information(pc_name) values (new.pc_name) returning pc_id INTO pc_id_var;
    end if;
    insert into pc_video_card(pc_id, video_card_slot_number, video_card_id) VALUES (pc_id_var, new.video_card_slot_number, video_card_id_var)
    on conflict(pc_id, video_card_slot_number) do update set video_card_id = video_card_id_var;
    RETURN new;
END;
$$;


ALTER FUNCTION pcbs.mod_table_instead_of_insert() OWNER TO postgres;

--
-- Name: mod_table_instead_of_update(); Type: FUNCTION; Schema: pcbs; Owner: postgres
--

CREATE FUNCTION pcbs.mod_table_instead_of_update() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    video_card_id_var integer;
    pc_id_var integer;
    old_video_card_id integer;
    old_pc_id integer;
    new_pc_id integer;
    new_video_card_id integer;
    new_slot integer;
BEGIN
    SELECT video_card_id from video_card_information where old.video_card_model = video_card_information.video_card_model INTO old_video_card_id;
    SELECT video_card_id from video_card_information where new.video_card_model = video_card_information.video_card_model INTO video_card_id_var;
    if video_card_id_var is NULL then
        insert into video_card_information(video_card_model) values (new.video_card_model) returning video_card_id INTO video_card_id_var;
    end if;
    SELECT pc_id from pc_information where old.pc_name = pc_information.pc_name INTO old_pc_id;
    SELECT pc_id from pc_information where new.pc_name = pc_information.pc_name INTO pc_id_var;
    if pc_id_var is NULL then
        insert into pc_information(pc_name) values (new.pc_name) returning pc_id INTO pc_id_var;
    end if;
    if new.video_card_slot_number is null then new_slot = old.video_card_slot_number;
    else new_slot = new.video_card_slot_number;
    end if;
    if new.pc_name is null then new_pc_id = old_pc_id;
    else new_pc_id = pc_id_var;
    end if;
    if new.video_card_model is null then new_video_card_id = old_video_card_id;
    else new_video_card_id = video_card_id_var;
    end if;
    update pc_video_card
    set pc_id = new_pc_id, video_card_id = new_video_card_id, video_card_slot_number = new_slot
    where pc_id = old_pc_id and video_card_id = old_video_card_id and video_card_slot_number = old.video_card_slot_number;
    RETURN new;
END;
$$;


ALTER FUNCTION pcbs.mod_table_instead_of_update() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: case_form_factor; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.case_form_factor (
    case_form_factor_id integer NOT NULL,
    form_factor character varying NOT NULL
);


ALTER TABLE pcbs.case_form_factor OWNER TO postgres;

--
-- Name: case_form_factor_case_form_factor_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.case_form_factor_case_form_factor_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.case_form_factor_case_form_factor_id_seq OWNER TO postgres;

--
-- Name: case_form_factor_case_form_factor_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.case_form_factor_case_form_factor_id_seq OWNED BY pcbs.case_form_factor.case_form_factor_id;


--
-- Name: case_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.case_information (
    case_id integer NOT NULL,
    case_model character varying NOT NULL,
    case_form_factor_id integer NOT NULL,
    case_maker_id integer NOT NULL
);


ALTER TABLE pcbs.case_information OWNER TO postgres;

--
-- Name: case_information_case_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.case_information_case_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.case_information_case_id_seq OWNER TO postgres;

--
-- Name: case_information_case_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.case_information_case_id_seq OWNED BY pcbs.case_information.case_id;


--
-- Name: cpu_core_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.cpu_core_information (
    core_id integer NOT NULL,
    core_name character varying,
    cpu_core_maker_id integer
);


ALTER TABLE pcbs.cpu_core_information OWNER TO postgres;

--
-- Name: cpu_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.cpu_information (
    cpu_model character varying,
    core_id integer,
    cpu_id integer NOT NULL,
    cpu_maker_id integer
);


ALTER TABLE pcbs.cpu_information OWNER TO postgres;

--
-- Name: ddr_generation; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.ddr_generation (
    generation_id integer NOT NULL,
    generation_name character varying
);


ALTER TABLE pcbs.ddr_generation OWNER TO postgres;

--
-- Name: ddr_generation_generation_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.ddr_generation_generation_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.ddr_generation_generation_id_seq OWNER TO postgres;

--
-- Name: ddr_generation_generation_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.ddr_generation_generation_id_seq OWNED BY pcbs.ddr_generation.generation_id;


--
-- Name: hdd_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.hdd_information (
    hdd_id integer NOT NULL,
    hdd_model character varying,
    hdd_memory_size integer,
    hdd_maker_id integer
);


ALTER TABLE pcbs.hdd_information OWNER TO postgres;

--
-- Name: hdd_information_hdd_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.hdd_information_hdd_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.hdd_information_hdd_id_seq OWNER TO postgres;

--
-- Name: hdd_information_hdd_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.hdd_information_hdd_id_seq OWNED BY pcbs.hdd_information.hdd_id;


--
-- Name: maker; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.maker (
    maker_id integer NOT NULL,
    maker_name character varying
);


ALTER TABLE pcbs.maker OWNER TO postgres;

--
-- Name: pc_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.pc_information (
    pc_id integer NOT NULL,
    cpu_id integer,
    hdd_id integer,
    case_id integer
);


ALTER TABLE pcbs.pc_information OWNER TO postgres;

--
-- Name: pc_information_pc_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.pc_information_pc_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.pc_information_pc_id_seq OWNER TO postgres;

--
-- Name: pc_information_pc_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.pc_information_pc_id_seq OWNED BY pcbs.pc_information.pc_id;


--
-- Name: pc_ram; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.pc_ram (
    pc_id integer NOT NULL,
    ram_slot_number integer NOT NULL,
    ram_id integer
);


ALTER TABLE pcbs.pc_ram OWNER TO postgres;

--
-- Name: pc_video_card; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.pc_video_card (
    pc_id integer NOT NULL,
    video_card_slot_number integer NOT NULL,
    video_card_id integer
);


ALTER TABLE pcbs.pc_video_card OWNER TO postgres;

--
-- Name: pc_video_card_pc_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.pc_video_card_pc_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.pc_video_card_pc_id_seq OWNER TO postgres;

--
-- Name: pc_video_card_pc_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.pc_video_card_pc_id_seq OWNED BY pcbs.pc_video_card.pc_id;


--
-- Name: ram_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.ram_information (
    ram_id integer NOT NULL,
    ram_model character varying,
    ddr_generation integer,
    ram_memory_size integer,
    ram_maker_id integer
);


ALTER TABLE pcbs.ram_information OWNER TO postgres;

--
-- Name: ram_information_ram_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.ram_information_ram_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.ram_information_ram_id_seq OWNER TO postgres;

--
-- Name: ram_information_ram_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.ram_information_ram_id_seq OWNED BY pcbs.ram_information.ram_id;


--
-- Name: resolution; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.resolution (
    resolution_id integer NOT NULL,
    resolution character varying
);


ALTER TABLE pcbs.resolution OWNER TO postgres;

--
-- Name: video_card_information; Type: TABLE; Schema: pcbs; Owner: postgres
--

CREATE TABLE pcbs.video_card_information (
    video_card_id integer NOT NULL,
    video_card_model character varying,
    resolution_id integer,
    video_card_maker_id integer
);


ALTER TABLE pcbs.video_card_information OWNER TO postgres;

--
-- Name: video_card_information_video_card_id_seq; Type: SEQUENCE; Schema: pcbs; Owner: postgres
--

CREATE SEQUENCE pcbs.video_card_information_video_card_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE pcbs.video_card_information_video_card_id_seq OWNER TO postgres;

--
-- Name: video_card_information_video_card_id_seq; Type: SEQUENCE OWNED BY; Schema: pcbs; Owner: postgres
--

ALTER SEQUENCE pcbs.video_card_information_video_card_id_seq OWNED BY pcbs.video_card_information.video_card_id;


--
-- Name: case_form_factor case_form_factor_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.case_form_factor ALTER COLUMN case_form_factor_id SET DEFAULT nextval('pcbs.case_form_factor_case_form_factor_id_seq'::regclass);


--
-- Name: case_information case_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.case_information ALTER COLUMN case_id SET DEFAULT nextval('pcbs.case_information_case_id_seq'::regclass);


--
-- Name: ddr_generation generation_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.ddr_generation ALTER COLUMN generation_id SET DEFAULT nextval('pcbs.ddr_generation_generation_id_seq'::regclass);


--
-- Name: hdd_information hdd_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.hdd_information ALTER COLUMN hdd_id SET DEFAULT nextval('pcbs.hdd_information_hdd_id_seq'::regclass);


--
-- Name: pc_information pc_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_information ALTER COLUMN pc_id SET DEFAULT nextval('pcbs.pc_information_pc_id_seq'::regclass);


--
-- Name: pc_video_card pc_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_video_card ALTER COLUMN pc_id SET DEFAULT nextval('pcbs.pc_video_card_pc_id_seq'::regclass);


--
-- Name: ram_information ram_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.ram_information ALTER COLUMN ram_id SET DEFAULT nextval('pcbs.ram_information_ram_id_seq'::regclass);


--
-- Name: video_card_information video_card_id; Type: DEFAULT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.video_card_information ALTER COLUMN video_card_id SET DEFAULT nextval('pcbs.video_card_information_video_card_id_seq'::regclass);


--
-- Data for Name: case_form_factor; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.case_form_factor (case_form_factor_id, form_factor) VALUES (2, 'Midi-Tower');
INSERT INTO pcbs.case_form_factor (case_form_factor_id, form_factor) VALUES (1, 'Full Tower');


--
-- Data for Name: case_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.case_information (case_id, case_model, case_form_factor_id, case_maker_id) VALUES (3, 'Matrexx 55 Black', 2, 9);
INSERT INTO pcbs.case_information (case_id, case_model, case_form_factor_id, case_maker_id) VALUES (1, 'Zofos Evo', 1, 1);
INSERT INTO pcbs.case_information (case_id, case_model, case_form_factor_id, case_maker_id) VALUES (2, 'ATX N2', 2, 6);
INSERT INTO pcbs.case_information (case_id, case_model, case_form_factor_id, case_maker_id) VALUES (20, 'ATX i3', 2, 6);


--
-- Data for Name: cpu_core_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.cpu_core_information (core_id, core_name, cpu_core_maker_id) VALUES (1, 'Coffee Lake S', 4);
INSERT INTO pcbs.cpu_core_information (core_id, core_name, cpu_core_maker_id) VALUES (2, 'Summit Ridge', 8);


--
-- Data for Name: cpu_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.cpu_information (cpu_model, core_id, cpu_id, cpu_maker_id) VALUES ('i7-8700K', 1, 1, 4);
INSERT INTO pcbs.cpu_information (cpu_model, core_id, cpu_id, cpu_maker_id) VALUES ('i7-8700', 1, 2, 4);
INSERT INTO pcbs.cpu_information (cpu_model, core_id, cpu_id, cpu_maker_id) VALUES ('Ryzen 5 1600', 2, 3, 8);


--
-- Data for Name: ddr_generation; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.ddr_generation (generation_id, generation_name) VALUES (10, 'DDR3');
INSERT INTO pcbs.ddr_generation (generation_id, generation_name) VALUES (11, 'DDR34');


--
-- Data for Name: hdd_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.hdd_information (hdd_id, hdd_model, hdd_memory_size, hdd_maker_id) VALUES (22, 'BarraCuda ST500DM009-1', 500, 2);
INSERT INTO pcbs.hdd_information (hdd_id, hdd_model, hdd_memory_size, hdd_maker_id) VALUES (21, 'UD Pro', 256, 6);


--
-- Data for Name: maker; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (1, 'Raijintek');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (2, 'Gigabyte');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (4, 'Intel');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (5, 'Seagate');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (6, 'Zalman');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (7, 'KFA2');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (8, 'AMD');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (9, 'Deepcool');
INSERT INTO pcbs.maker (maker_id, maker_name) VALUES (3, 'MSI');


--
-- Data for Name: pc_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.pc_information (pc_id, cpu_id, hdd_id, case_id) VALUES (2, 1, 22, 1);
INSERT INTO pcbs.pc_information (pc_id, cpu_id, hdd_id, case_id) VALUES (1, 1, 21, 1);
INSERT INTO pcbs.pc_information (pc_id, cpu_id, hdd_id, case_id) VALUES (3, 2, 22, 2);
INSERT INTO pcbs.pc_information (pc_id, cpu_id, hdd_id, case_id) VALUES (4, 3, 22, 3);
INSERT INTO pcbs.pc_information (pc_id, cpu_id, hdd_id, case_id) VALUES (679, 1, 21, 3);


--
-- Data for Name: pc_ram; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.pc_ram (pc_id, ram_slot_number, ram_id) VALUES (1, 1, 1);
INSERT INTO pcbs.pc_ram (pc_id, ram_slot_number, ram_id) VALUES (1, 2, 1);
INSERT INTO pcbs.pc_ram (pc_id, ram_slot_number, ram_id) VALUES (4, 1, 1);
INSERT INTO pcbs.pc_ram (pc_id, ram_slot_number, ram_id) VALUES (4, 2, 4);
INSERT INTO pcbs.pc_ram (pc_id, ram_slot_number, ram_id) VALUES (2, 1, 1);
INSERT INTO pcbs.pc_ram (pc_id, ram_slot_number, ram_id) VALUES (3, 1, 2);


--
-- Data for Name: pc_video_card; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.pc_video_card (pc_id, video_card_slot_number, video_card_id) VALUES (1, 1, 1);
INSERT INTO pcbs.pc_video_card (pc_id, video_card_slot_number, video_card_id) VALUES (1, 2, 2);
INSERT INTO pcbs.pc_video_card (pc_id, video_card_slot_number, video_card_id) VALUES (2, 2, 1);
INSERT INTO pcbs.pc_video_card (pc_id, video_card_slot_number, video_card_id) VALUES (3, 1, 3);
INSERT INTO pcbs.pc_video_card (pc_id, video_card_slot_number, video_card_id) VALUES (4, 1, 1);
INSERT INTO pcbs.pc_video_card (pc_id, video_card_slot_number, video_card_id) VALUES (4, 3, 1);


--
-- Data for Name: ram_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.ram_information (ram_id, ram_model, ddr_generation, ram_memory_size, ram_maker_id) VALUES (1, 'AORUS RGB Memory 16GB (2x8GB)', 10, 8, 2);
INSERT INTO pcbs.ram_information (ram_id, ram_model, ddr_generation, ram_memory_size, ram_maker_id) VALUES (2, 'Radeon R5 Entertainment Series', 11, 2, 8);
INSERT INTO pcbs.ram_information (ram_id, ram_model, ddr_generation, ram_memory_size, ram_maker_id) VALUES (4, 'radeon r5 entertainment serie', 10, 4, 8);


--
-- Data for Name: resolution; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.resolution (resolution_id, resolution) VALUES (1, '7680x4320');
INSERT INTO pcbs.resolution (resolution_id, resolution) VALUES (2, '4096x2160');


--
-- Data for Name: video_card_information; Type: TABLE DATA; Schema: pcbs; Owner: postgres
--

INSERT INTO pcbs.video_card_information (video_card_id, video_card_model, resolution_id, video_card_maker_id) VALUES (1, 'AORUS Radeon RX580 8G', 1, 2);
INSERT INTO pcbs.video_card_information (video_card_id, video_card_model, resolution_id, video_card_maker_id) VALUES (2, 'AMD Radeon RX 580 GAMING X 8G', 1, 3);
INSERT INTO pcbs.video_card_information (video_card_id, video_card_model, resolution_id, video_card_maker_id) VALUES (3, 'GeForce GTX 1050 Ti', 2, 7);


--
-- Name: case_form_factor_case_form_factor_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.case_form_factor_case_form_factor_id_seq', 29, true);


--
-- Name: case_information_case_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.case_information_case_id_seq', 23, true);


--
-- Name: ddr_generation_generation_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.ddr_generation_generation_id_seq', 13, true);


--
-- Name: hdd_information_hdd_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.hdd_information_hdd_id_seq', 53, true);


--
-- Name: pc_information_pc_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.pc_information_pc_id_seq', 679, true);


--
-- Name: pc_video_card_pc_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.pc_video_card_pc_id_seq', 1, false);


--
-- Name: ram_information_ram_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.ram_information_ram_id_seq', 250, true);


--
-- Name: video_card_information_video_card_id_seq; Type: SEQUENCE SET; Schema: pcbs; Owner: postgres
--

SELECT pg_catalog.setval('pcbs.video_card_information_video_card_id_seq', 667, true);


--
-- Name: case_form_factor case_form_factor_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.case_form_factor
    ADD CONSTRAINT case_form_factor_pk PRIMARY KEY (case_form_factor_id);


--
-- Name: case_information case_id_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.case_information
    ADD CONSTRAINT case_id_pk PRIMARY KEY (case_id);


--
-- Name: cpu_core_information cpu_core_information_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.cpu_core_information
    ADD CONSTRAINT cpu_core_information_pk PRIMARY KEY (core_id);


--
-- Name: cpu_information cpu_information_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.cpu_information
    ADD CONSTRAINT cpu_information_pk PRIMARY KEY (cpu_id);


--
-- Name: ddr_generation ddr_generation_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.ddr_generation
    ADD CONSTRAINT ddr_generation_pk PRIMARY KEY (generation_id);


--
-- Name: hdd_information hdd_information_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.hdd_information
    ADD CONSTRAINT hdd_information_pk PRIMARY KEY (hdd_id);


--
-- Name: maker maker_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.maker
    ADD CONSTRAINT maker_pk PRIMARY KEY (maker_id);


--
-- Name: pc_information pc_information_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_information
    ADD CONSTRAINT pc_information_pk PRIMARY KEY (pc_id);


--
-- Name: pc_ram pc_ram_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_ram
    ADD CONSTRAINT pc_ram_pk PRIMARY KEY (pc_id, ram_slot_number);


--
-- Name: pc_video_card pc_video_card_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_video_card
    ADD CONSTRAINT pc_video_card_pk PRIMARY KEY (pc_id, video_card_slot_number);


--
-- Name: ram_information ram_information_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.ram_information
    ADD CONSTRAINT ram_information_pk PRIMARY KEY (ram_id);


--
-- Name: resolution resolution_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.resolution
    ADD CONSTRAINT resolution_pk PRIMARY KEY (resolution_id);


--
-- Name: video_card_information video_card_information_pk; Type: CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.video_card_information
    ADD CONSTRAINT video_card_information_pk PRIMARY KEY (video_card_id);


--
-- Name: case_information case_information_case_form_factor_case_form_factor_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.case_information
    ADD CONSTRAINT case_information_case_form_factor_case_form_factor_id_fk FOREIGN KEY (case_form_factor_id) REFERENCES pcbs.case_form_factor(case_form_factor_id);


--
-- Name: case_information case_information_maker_maker_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.case_information
    ADD CONSTRAINT case_information_maker_maker_id_fk FOREIGN KEY (case_maker_id) REFERENCES pcbs.maker(maker_id);


--
-- Name: cpu_core_information cpu_core_information_maker_maker_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.cpu_core_information
    ADD CONSTRAINT cpu_core_information_maker_maker_id_fk FOREIGN KEY (cpu_core_maker_id) REFERENCES pcbs.maker(maker_id);


--
-- Name: cpu_information cpu_information_cpu_core_information_core_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.cpu_information
    ADD CONSTRAINT cpu_information_cpu_core_information_core_id_fk FOREIGN KEY (core_id) REFERENCES pcbs.cpu_core_information(core_id);


--
-- Name: cpu_information cpu_information_maker_maker_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.cpu_information
    ADD CONSTRAINT cpu_information_maker_maker_id_fk FOREIGN KEY (cpu_maker_id) REFERENCES pcbs.maker(maker_id);


--
-- Name: hdd_information hdd_information_maker_maker_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.hdd_information
    ADD CONSTRAINT hdd_information_maker_maker_id_fk FOREIGN KEY (hdd_maker_id) REFERENCES pcbs.maker(maker_id);


--
-- Name: pc_information pc_information_case_information_case_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_information
    ADD CONSTRAINT pc_information_case_information_case_id_fk FOREIGN KEY (case_id) REFERENCES pcbs.case_information(case_id);


--
-- Name: pc_information pc_information_cpu_information_cpu_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_information
    ADD CONSTRAINT pc_information_cpu_information_cpu_id_fk FOREIGN KEY (cpu_id) REFERENCES pcbs.cpu_information(cpu_id);


--
-- Name: pc_information pc_information_hdd_information_hdd_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_information
    ADD CONSTRAINT pc_information_hdd_information_hdd_id_fk FOREIGN KEY (hdd_id) REFERENCES pcbs.hdd_information(hdd_id) ON DELETE SET NULL;


--
-- Name: pc_ram pc_ram_pc_information_pc_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_ram
    ADD CONSTRAINT pc_ram_pc_information_pc_id_fk FOREIGN KEY (pc_id) REFERENCES pcbs.pc_information(pc_id);


--
-- Name: pc_ram pc_ram_ram_information_ram_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_ram
    ADD CONSTRAINT pc_ram_ram_information_ram_id_fk FOREIGN KEY (ram_id) REFERENCES pcbs.ram_information(ram_id) ON DELETE CASCADE;


--
-- Name: pc_video_card pc_video_card_pc_information_pc_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_video_card
    ADD CONSTRAINT pc_video_card_pc_information_pc_id_fk FOREIGN KEY (pc_id) REFERENCES pcbs.pc_information(pc_id);


--
-- Name: pc_video_card pc_video_card_video_card_information_video_card_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.pc_video_card
    ADD CONSTRAINT pc_video_card_video_card_information_video_card_id_fk FOREIGN KEY (video_card_id) REFERENCES pcbs.video_card_information(video_card_id);


--
-- Name: ram_information ram_information_ddr_generation_generation_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.ram_information
    ADD CONSTRAINT ram_information_ddr_generation_generation_id_fk FOREIGN KEY (ddr_generation) REFERENCES pcbs.ddr_generation(generation_id);


--
-- Name: ram_information ram_information_maker_maker_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.ram_information
    ADD CONSTRAINT ram_information_maker_maker_id_fk FOREIGN KEY (ram_maker_id) REFERENCES pcbs.maker(maker_id);


--
-- Name: video_card_information video_card_information_maker_maker_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.video_card_information
    ADD CONSTRAINT video_card_information_maker_maker_id_fk FOREIGN KEY (video_card_maker_id) REFERENCES pcbs.maker(maker_id);


--
-- Name: video_card_information video_card_information_resolution_resolution_id_fk; Type: FK CONSTRAINT; Schema: pcbs; Owner: postgres
--

ALTER TABLE ONLY pcbs.video_card_information
    ADD CONSTRAINT video_card_information_resolution_resolution_id_fk FOREIGN KEY (resolution_id) REFERENCES pcbs.resolution(resolution_id);


--
-- PostgreSQL database dump complete
--

