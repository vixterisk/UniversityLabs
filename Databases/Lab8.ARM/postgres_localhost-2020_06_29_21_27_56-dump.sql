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

CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


ALTER DATABASE postgres OWNER TO postgres;

\connect postgres

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
-- Name: users; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA users;


ALTER SCHEMA users OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: user; Type: TABLE; Schema: users; Owner: postgres
--

CREATE TABLE users."user" (
    id integer NOT NULL,
    login character varying NOT NULL,
    password character varying NOT NULL,
    salt character varying NOT NULL,
    reg_date date NOT NULL,
    is_admin boolean
);


ALTER TABLE users."user" OWNER TO postgres;

--
-- Name: user_id_seq; Type: SEQUENCE; Schema: users; Owner: postgres
--

CREATE SEQUENCE users.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE users.user_id_seq OWNER TO postgres;

--
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: users; Owner: postgres
--

ALTER SEQUENCE users.user_id_seq OWNED BY users."user".id;


--
-- Name: user id; Type: DEFAULT; Schema: users; Owner: postgres
--

ALTER TABLE ONLY users."user" ALTER COLUMN id SET DEFAULT nextval('users.user_id_seq'::regclass);


--
-- Data for Name: user; Type: TABLE DATA; Schema: users; Owner: postgres
--

INSERT INTO users."user" (id, login, password, salt, reg_date, is_admin) VALUES (101, 'lapa', '99F399CF65DCC51E0499336B62B75B2D40A773C0EF2274AA31E473F0F2657D2E', '471d08ae-58ba-432e-a1be-6ad114ecbf69', '2020-06-22', false);
INSERT INTO users."user" (id, login, password, salt, reg_date, is_admin) VALUES (102, 'deadlapa', 'D3534C6D20EADCE3A9815EB998C3587D2FA5DAF5F61C9EFF963826B70918D8B7', 'c4ca0e21-ff44-48da-a7fa-25f8cce24867', '2020-06-25', true);
INSERT INTO users."user" (id, login, password, salt, reg_date, is_admin) VALUES (94, 'vixterisk', 'D61D67D4C968AD21DEA8366E21C6FC818721D75270286D9AB6286DE1A1C5ABA5', '647f8355-27d2-4f0a-ad0d-731b24e550b5', '2020-05-16', true);


--
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: users; Owner: postgres
--

SELECT pg_catalog.setval('users.user_id_seq', 104, true);


--
-- Name: user user_pk; Type: CONSTRAINT; Schema: users; Owner: postgres
--

ALTER TABLE ONLY users."user"
    ADD CONSTRAINT user_pk PRIMARY KEY (id);


--
-- Name: user_login_uindex; Type: INDEX; Schema: users; Owner: postgres
--

CREATE UNIQUE INDEX user_login_uindex ON users."user" USING btree (login);


--
-- PostgreSQL database dump complete
--

