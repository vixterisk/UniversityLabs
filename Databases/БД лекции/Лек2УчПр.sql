DROP TABLE IF EXISTS users;
CREATE TABLE users
(
	id bigint GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
	login varchar NOT NULL,
	image_hash bytea
);

CREATE UNIQUE INDEX ON users(lower(login));

ALTER TABLE users ADD CHECK (login ~* '[а-я0-9]{6-50}') --проверка на регулярное выражение в постгресе 
-- ~ - проверка на регулярное выражение по шаблону
-- * - регистронезависимая 

INSERT INTO users(login) Values ('Кит');
INSERT INTO users(login) Values ('КИТ');