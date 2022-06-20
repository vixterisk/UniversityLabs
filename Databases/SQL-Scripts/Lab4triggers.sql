CREATE TABLE Spec(
    id              int             PRIMARY KEY,
    table_name      varchar(50)     NOT NULL,
    column_name     varchar(50)     NOT NULL,
    curMax          int             Not NULL
);

INSERT INTO Spec(id, table_name, column_name, curMax)
    VALUES(1, 'spec', 'id', 1);

--Создаем функцию, формирующую триггер
CREATE OR REPLACE FUNCTION update_spec_curmax()
	RETURNS trigger AS
	$$
    declare
        maxvalue integer;
	BEGIN
		EXECUTE format('select max(%I) from new', quote_ident(tg_argv[1])) INTO maxvalue;
		UPDATE spec
		SET curmax = maxvalue
		where table_name = tg_argv[0] and column_name = tg_argv[1] and curmax < maxvalue;
		RETURN NULL;
	END;
	$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_next_number(_table_name varchar, _column_name varchar, out incremented_id integer) AS
    $$
    BEGIN
        IF NOT EXISTS(SELECT * FROM spec where table_name = (_table_name) and column_name = (_column_name)) THEN
            EXECUTE format('SELECT coalesce(max(%I)+1,1) FROM %I',quote_ident(_column_name), quote_ident(_table_name)) INTO incremented_id;
            INSERT INTO Spec(id, table_name, column_name, curMax)
               VALUES(get_next_number('spec', 'id'), (_table_name), (_column_name),incremented_id);
            EXECUTE format('CREATE TRIGGER spec_curmax_insert_%I
	                            AFTER INSERT
	                            ON %I
                                REFERENCING NEW TABLE AS new
	                            FOR EACH STATEMENT
                            EXECUTE FUNCTION update_spec_curmax(%I, %I);
                            CREATE TRIGGER spec_curmax_update_%I
	                            AFTER UPDATE
	                            ON %I
                                REFERENCING NEW TABLE AS new
	                            FOR EACH STATEMENT
                            EXECUTE FUNCTION update_spec_curmax(%I, %I);',
                quote_ident(_column_name), quote_ident(_table_name), quote_ident(_table_name), quote_ident(_column_name),
                quote_ident(_column_name), quote_ident(_table_name), quote_ident(_table_name), quote_ident(_column_name))
            using _column_name;
        ELSE
            UPDATE Spec
            SET curMax = curMax+1
            WHERE table_name = (_table_name) and column_name = (_column_name)
            returning curMax into incremented_id;
        END IF;
    END;
    $$ LANGUAGE plpgsql;

CREATE TABLE test(
    id              int             NOT NULL
);

INSERT INTO test(id)
    VALUES(10);

SELECT get_next_number('test', 'id');

--Добавляем значение больше максимума в обход процедуре
INSERT INTO test(id)
    VALUES(155);

SELECT get_next_number('test', 'id');

--Добавляем значение меньше максимума
INSERT INTO test(id)
    VALUES(112);

SELECT get_next_number('test', 'id');

--Обновляем одно из значений на число больше максимума
UPDATE test
set id = 200
where id = 112;

SELECT get_next_number('test', 'id');

--Обновляем одно из значений на число меньше максимума
UPDATE test
set id = 112
where id = 200;

SELECT get_next_number('test', 'id');

--Вставляем сразу несколько значений
INSERT INTO test(id)
    VALUES(301), (302), (303);

SELECT * from Spec;

--Обновляем сразу несколько значений
UPDATE test
set id = id+100
where id > 300;

SELECT * from Spec;

CREATE TABLE test2(
    num_value1              int             NOT NULL,
    num_value2              int             NOT NULL
);

--Проверяем, создаются ли триггеры на несколько столбцов одной таблицы
SELECT get_next_number('test2', 'num_value1');
SELECT get_next_number('test2', 'num_value2');
SELECT * from Spec;

INSERT INTO test2(num_value1,num_value2)
    VALUES(112, 112);

SELECT * from Spec;

INSERT INTO test2(num_value1,num_value2)
    VALUES(55, 55);

SELECT * from Spec;

INSERT INTO test2(num_value1,num_value2)
    VALUES(155, 55);

SELECT * from Spec;

update test2
set num_value1 = 200
where num_value1 = 55;

SELECT * from Spec;

update test2
set num_value1 = 55
where num_value1 = 200;

SELECT * from Spec;

DROP trigger spec_curmax_insert_id on test;
DROP trigger spec_curmax_update_id on test;
DROP trigger spec_curmax_insert_num_value1 on test2;
DROP trigger spec_curmax_insert_num_value2 on test2;
DROP trigger spec_curmax_update_num_value1 on test2;
DROP trigger spec_curmax_update_num_value2 on test2;

drop function update_spec_curmax();
DROP FUNCTION  get_next_number(_table_name varchar, _column_name varchar);

DROP TABLE Spec;
DROP TABLE test;
DROP TABLE test2;