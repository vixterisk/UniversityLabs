--1
CREATE TABLE public.Spec(
    id              int             PRIMARY KEY,
    table_name      varchar(50)     NOT NULL,
    column_name     varchar(50)     NOT NULL,
    curMax          int             Not NULL
);

--2
INSERT INTO public.Spec(id, table_name, column_name, curMax)
    VALUES(1, 'spec', 'id', 1);

--3
CREATE OR REPLACE FUNCTION get_next_number(_table_name varchar, _column_name varchar, out incremented_id integer) AS
    $$
    BEGIN
        IF NOT EXISTS(SELECT * FROM spec where table_name = (_table_name) and column_name = (_column_name)) THEN
            EXECUTE format('SELECT coalesce(max(%I)+1,1) FROM %I',quote_ident(_column_name), quote_ident(_table_name)) INTO incremented_id;
            INSERT INTO public.Spec(id, table_name, column_name, curMax)
               VALUES(get_next_number('spec', 'id'), (_table_name), (_column_name),incremented_id);
        ELSE
            UPDATE Spec
            SET curMax = curMax+1
            WHERE table_name = (_table_name) and column_name = (_column_name)
            returning curMax into incremented_id;
        END IF;
    END;
    $$ LANGUAGE plpgsql;

--4
SELECT get_next_number('spec', 'id');

--5
SELECT * FROM Spec;

--6
SELECT get_next_number('spec', 'id');

--7
SELECT * FROM Spec;

--8
CREATE TABLE public.test(
    id              int             NOT NULL
);

--9
INSERT INTO public.test(id)
    VALUES(10);

--10
SELECT get_next_number('test', 'id');

--11
SELECT * FROM Spec;

--12
SELECT get_next_number('test', 'id');

--13
SELECT * FROM Spec;

--14
CREATE TABLE public.test2(
    num_value1              int             NOT NULL,
    num_value2              int             NOT NULL
);

--15
SELECT get_next_number('test2', 'num_value1');

--16
SELECT * FROM Spec;

--17
SELECT get_next_number('test2', 'num_value1');

--18
SELECT * FROM Spec;

--19
INSERT INTO public.test2(num_value1, num_value2)
    VALUES(2, 13);

--20
SELECT get_next_number('test2', 'num_value2');

--21
SELECT * FROM Spec;

--22
SELECT get_next_number('test2', 'num_value1');
SELECT get_next_number('test2', 'num_value1');
SELECT get_next_number('test2', 'num_value1');
SELECT get_next_number('test2', 'num_value1');
SELECT get_next_number('test2', 'num_value1');

--23
SELECT * FROM Spec;

--24
DROP FUNCTION  get_next_number(_table_name varchar, _column_name varchar);

--25
DROP TABLE public.Spec;
DROP TABLE public.test;
DROP TABLE public.test2;
