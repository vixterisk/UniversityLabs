CREATE TABLE Spec
(
    id          int PRIMARY KEY,
    table_name  varchar(50) NOT NULL,
    column_name varchar(50) NOT NULL,
    curMax      int         Not NULL
);

INSERT INTO Spec(id, table_name, column_name, curMax)
VALUES (1, 'spec', 'id', 1);

CREATE OR REPLACE FUNCTION update_spec_curmax()
    RETURNS trigger AS
$$
declare
    maxvalue integer;
BEGIN
    EXECUTE format('select max(%I) from new', quote_ident(tg_argv[1])) INTO maxvalue;
    UPDATE spec
    SET curmax = maxvalue
    where table_name = tg_argv[0]
      and column_name = tg_argv[1]
      and curmax < maxvalue;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_next_number(_schema_name varchar, _table_name varchar, _column_name varchar,
                                           out incremented_id integer) AS
$$
DECLARE
    triggerNumber integer;
BEGIN
    ASSERT EXISTS(
            SELECT * FROM information_schema.tables WHERE table_schema = _schema_name and table_name = _table_name),
        'таблицы с таким названием не существует';
    ASSERT EXISTS(SELECT *
                  FROM information_schema.columns
                  WHERE table_schema = _schema_name
                    and table_name = _table_name
                    and column_name = _column_name),
        'такого столбца в таблице не существует';
    ASSERT EXISTS(SELECT *
                  FROM information_schema.columns
                  WHERE table_schema = _schema_name
                    and table_name = _table_name
                    and column_name = _column_name
                    and data_type = 'integer'),
        'Передан столбец, тип данных которого не является целочисленным';
    IF NOT EXISTS(SELECT * FROM spec where table_name = (_table_name) and column_name = (_column_name)) THEN
        EXECUTE format('SELECT coalesce(max(%I)+1,1) FROM %I', _column_name, _table_name) INTO incremented_id;
        INSERT INTO Spec(id, table_name, column_name, curMax)
        VALUES (get_next_number(_schema_name, 'spec', 'id'), (_table_name), (_column_name), incremented_id);
        triggerNumber = (SELECT count(*) + 1 from
                         (select distinct trigger_name
                         FROM information_schema.triggers
                         where event_object_schema = _schema_name
                           and event_object_table = _table_name) as t);
        loop
            if exists(select *
                      from information_schema.triggers
                      where event_object_schema = _schema_name and event_object_table = _table_name and
                            trigger_name = _table_name || '_' || _column_name || '_' || triggerNumber) then
                triggerNumber = triggerNumber + 1;
            else
                exit;
            end if;
        end loop;
        EXECUTE format('CREATE TRIGGER %I
	                            AFTER INSERT
	                            ON %I
                                REFERENCING NEW TABLE AS new
	                            FOR EACH STATEMENT
                            EXECUTE FUNCTION update_spec_curmax(%I, %I);',
                       _table_name || '_' || _column_name || '_' || triggerNumber, _table_name, _table_name, _column_name);
        triggerNumber = triggerNumber + 1;
        loop
            if exists(select trigger_name
                      from information_schema.triggers
                      where event_object_schema = _schema_name and event_object_table = _table_name and
                            trigger_name = _table_name || '_' || _column_name || '_' || triggerNumber) then
                triggerNumber = triggerNumber + 1;
            else
                exit;
            end if;
        end loop;
        EXECUTE format('CREATE TRIGGER %I
	                            AFTER UPDATE
	                            ON %I
                                REFERENCING NEW TABLE AS new
	                            FOR EACH STATEMENT
                            EXECUTE FUNCTION update_spec_curmax(%I, %I);',
                       quote_ident(_table_name) || '_' || _column_name || '_' || triggerNumber, _table_name, _table_name, _column_name);
    ELSE
        UPDATE Spec
        SET curMax = curMax + 1
        WHERE table_name = (_table_name)
          and column_name = (_column_name)
        returning curMax into incremented_id;
    END IF;
END;
$$ LANGUAGE plpgsql;

select get_next_number('public', 'spec', 'id');

CREATE TABLE "'; drop table droptest;"
(
    "'; drop table droptest;"          int PRIMARY KEY,
    table_name  varchar(50) NOT NULL,
    column_name varchar(50) NOT NULL,
    curMax      int         Not NULL
);

SELECT get_next_number('public', '''; drop table droptest;', '''; drop table droptest;');

CREATE TABLE test
(
    id  int PRIMARY KEY,
    id2 int     NOT NULL,
    str varchar Not NULL
);

do $$
    begin
SELECT get_next_number('public', 'test1', 'id');

EXCEPTION WHEN assert_failure THEN
           raise notice '%', SQLERRM;
end;$$;

do $$
    begin
SELECT get_next_number('public', 'test', 'id1');

EXCEPTION WHEN assert_failure THEN
           raise notice '%', SQLERRM;
end;$$;

do $$
    begin
SELECT get_next_number('public', 'test', 'str');

EXCEPTION WHEN assert_failure THEN
           raise notice '%', SQLERRM;
end;$$;

CREATE TRIGGER test_id_2
    AFTER INSERT
    ON test
    REFERENCING NEW TABLE AS new
    FOR EACH STATEMENT
EXECUTE FUNCTION update_spec_curmax('test', 'id');

CREATE TRIGGER test_id_3
    AFTER UPDATE
    ON test
    REFERENCING NEW TABLE AS new
    FOR EACH STATEMENT
EXECUTE FUNCTION update_spec_curmax('test', 'id');

CREATE OR REPLACE FUNCTION foo() RETURNS trigger AS
$$
begin
    return null;
end
$$ language plpgsql;

CREATE TRIGGER foo_tr
    AFTER INSERT OR UPDATE OR DELETE
    ON test
    FOR EACH ROW
EXECUTE FUNCTION foo();

SELECT get_next_number('public', 'test', 'id');

INSERT INTO test(id, id2, str)
VALUES (10, 15, 'str');

SELECT get_next_number('public', 'test', 'id');

SELECT get_next_number('public', 'test', 'id2');

DROP TABLE Spec;
DROP TABLE test;
drop table "'; drop table droptest;";

drop function foo();
drop function update_spec_curmax();
DROP FUNCTION get_next_number(_schema_name varchar, _table_name varchar, _column_name varchar);