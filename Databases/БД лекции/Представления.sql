--SELECT *
--FROM information_schema.columns
--WHERE table_sclema = 'public';

--SELECT *
--FROM information_schema.tables;

CREATE OR REPLACE PROCEDURE error_example(
    _mu_name municipal_units.name%type, _command varchar) AS
$$
BEGIN
    --существет ли?
    ASSERT EXISTS (SELECT * FROM municipal_units WHERE name=_mu_name),
    'МО с таким названием нет'
    EXECUTE _command USING _mu_name;
END;
$$ LANGUAGE plpgsql;
--анонимный блок кода, который выполняется сразу после объявления
DO $$
    BEGIN
        CALL error_example(_mu_name: '...', _command: 'SELECT $1');
        RAISE EXCERTION 'текст ошибки (%)', 'переменная')
            USING HINT = 'Не делайте так';
        --CALL error_example(_mu_name: '<не существует>', _command: NULL);   
    EXCERTION
        WHEN assert_faiure THEN
        RAISE NOTICE 'Ой';
    EXCERTION
        WHEN OTHERS 
        THEN
            RAISE NOTICE 'Ой на все остальное'; --поймали все ошибки		
    END;
$$;
________________________________________________________________________________________________
CREATE VIEW animals_cool --поименовали наш запрос
AS
SELECT *
FROM animals
WHERE total>100; 
--появилась новая папка views
--использовать сохр запрос, где от нас ждут таблицу
--теперь можно написать так 
SELECT *
FROM animals_cool

CREATE VIEW animals_super_coolinfor
AS
SELECT *
FROM animals_cool
WHERE name ILIKE 'Б%'
ORDER BY total DESC;

SELECT* 
FROM animals_super_cool;

DROP VIEW animals_cool CASCADE; --каскадное удаление (удаление и тех, что были основанны на этом)


правая кнопка мыши ->эксплейн аналайз
SELECT *
FROM (SELECT*
      FROM animals
      WHERE total>100) AS tmp
WHERE name ILIKE 'Б%'
ORDER BY total DESC;

Можем узнать какие таблицы есть в нашей базе данных
SELECT *
FROM information_shema/columns
WHERE table_shema = 'public';

________________________________________________________________________________________________

SELECT *
FROM addrobj
WHERE currstatus = 0 --верхний уровень иерархии
    AND parentguid IS NULL

SELECT a1.formalname, a2.formalname
FROM addrobj AS a1 JOIN addrobj AS a2 ON a1.aoguid = a2.parentguid
WHERE currstatus = 0 
    AND parentguid IS NULL
________________________________________________________________________________________________
--доп таблица толко в рамках этого запроса
WITH RECURSIVE nested AS (
    SELECT concat(formalname, ' ', shortname) AS full_name, aoguid, aolevel
    FROM addrobj
    WHERE currstatus = 0
        AND parentguid IS NULL
    Union ALL
    SELECT concat(full_name, '|', a1.shortname, ' ', a2.formalname), a1.aoguid, a1.aolevel
    FROM addrobj AS a1 
        JOIN nested AS a2 ON a2.aoguid = a1.parentguid
)
SELECT*
FROM nested;
--автоматически создавать на основе предыущей