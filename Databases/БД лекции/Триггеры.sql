--Триггеры
-- 1) Построчные
--	new - новое состояние строки
--	old - строка со старыми значениями
-- 2) Покомандные
--	new - все новые значения - таблица
--	old - все старые

ALTER TABLE animals
    ADD total BIGINT NOT NULL DEFAULT 0;
	
SELECT animal_id, SUM(number)
FROM resources
GROUP BY animal_id;



UPDATE animals
SET total = 
FROM (SELECT animal_id, SUM(number) AS sum_number
           FROM resources
           GROUP BY animal_id) AS totals

--UPDATE resources SET number = 0 WHERE FALSE ничего не изменится, вызовется покомандный, не вызовется построчный

--Построчный триггер на синхронизацию двух таблиц
CREATE OR REPLACE FUNCTION update_animals_total_on_resources_iud()
    RETURNS trigger AS
	$$
	BEGIN
		IF tg_op = 'DELETE' THEN 
			UPDATE animals SET total = total - old.number WHERE id = old.animal_id;
		ELSIF tg_op = 'INSERT' THEN 
			UPDATE animals SET total = total + new.nuber WHERE id = new.animal_id;
		ELSE tg_op = 'UPDATE' THEN
			UPDATE animals SET total = total + new.number - old.number WHERE id = new.animal_id;
		END IF;
	    RETURN NULL;
	END;
	$$ LANGUAGE plpgsql;
	
CREATE TRIGGER resources_on_iud
    AFTER INSERT OR UPDATE OR DELETE
	ON resources
	FOR EACH ROW --построчный
EXECUTE FUNCTION update_animals_total_on_resources_iud();

DROP TRIGGER resources_on_iud ON --недописала

--Покомандный не допускает триггер сразу на три действия
CREATE OR REPLACE FUNCTION update_animals_total_on_resources_insert()
	RETURNS trigger AS
	$$
	BEGIN
		UPDATE animals
		SET total = total + toals.sum_number
		FROM (
				SELECT animal_id, SUM(number) AS sum_number
				FROM new
				GROUP BY animal_id
		) as totals
		WHERE animals.id = totals.animal_id;
		RETURN NULL;
	END;
	$$ LANGUAGE plpgsql;
	
CREATE TRIGGER update_animals_total_on_resources_insert
	AFTER INSERT
	ON resources
	REFERENCING NEW TABLE AS new --REFERENCING NEW TABLE AS new OLD TABLE AS old
	FOR EACH STATEMENT
EXECUTE FINCTION update_animals_total_on_resources_insert();

--Обновление подзапрос

FROM (
		SELECT animal_id, SUM(signed_number) AS sum_number
		FROM (
				SELECT animal_id, number AS signed_number
				FROM new
				UNION ALL
				SELECT animal_id, -number
				FROM old
				) AS signed_totals
		GROUP BY animal_id
	)  AS totals