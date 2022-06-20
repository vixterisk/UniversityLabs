CREATE OR REPLACE FUNCTION resources_number_by_name(
	_mu_name varchar,
	_animal_name asnimals.name%type
)
    RETURNS integer AS 
    $$
    BEGIN
	RAISE NOTICE 'Called resources_by_name(%,%)', _mu_name, _animal_name;
        RETURN (SELECT number
                FROM animals
			JOIN resources
				ON animals.id = resources.animal_id
			JOIN municipal_units
		...
		WHERE animals.name = _animal_name...);
    END;
    $$ LANGUAGE plpgsql
RETURNS NULL ON NULL INPUT;

SELECT resources_number_by_name(_mu_name:'јлександровский муниципальный район', _animal_name:'¬олк');
SELECT resources_number_by_name(_animal_name: _animal_name: := '¬олк', _mu_name: _mu_name := 'јлександровский муниципальный район');
SELECT name, resources_number_by_name(_mu_name: name, _animal_name:'¬олк') FROM municipal_unitls;

-------------------------------------------------

CREATE OR REPLACE FUNCTION resources_per_mu(_mu_name municipal_units.name%type)
	RETURNS table	(
		animals_name animals.name%type,
		number resources.number%type
			) 
AS
$$
BEGIN
	Return QUEry
		select animals.name, number
		from animals
	join resources on...
END;
$$ Language  plpgsql RETURNS NULL on nULL input;

SELECT * FROM municipal_units CROSS JOIN LATERAL resources_per_mu(_mu_name: 'municipal_units.name');

--------------------------------------

CREATE OR REPLACE FUNCTION upsert_resource(
	_mu_name municipal_units.name%type,
...
					) 
RETURNS void
AS $$
	DECLARE
	_nu_id municipal_units.id%type;
	_animal_id animal.id%type;
	_resource_id resources.id%type;
BEGIN --спросить второй вариант у миши
	SELECT id INTO _mu_id FROM municipal_units WHERE name = _mu_name;
	SELECT id INTO _animal_id FROM animals WHERE name = _animal_name;
	SELECT id INTO resource_id FROM resources WHERE municipal_unit_id = _mu_id AND anima_id = _animal_id;
	IF (resource_id IS NULL)
		INSERT INTO resources(municipal_unit_id,animal_id,number) VALUES (_mu_id, animal_id, number);
	ELSE 
		UPDATE resources SET number = _number WHERE id = _resource_id;
	END IF;
END;
$$
LangUAGE plpgsql;

------------------------------------------------
--next_id('Animals','id'); -> 64, 65
--table_name | column_name | max_val
--вспомогательна€ таблица table1, если пусто, то ищем текущий макс идентификатор дл€ строки animals id, ¬озвращаем его
--если пуста€ энималс, то возвращаем диницу
--дл€ существующих увеличиваем на 1 в таблице и возвращаем увеличенное
--вызываем рекурсивно, не находим стоки, макс дл€ путсой 1
--1 table1 id 2
--2 animals id 64
--EXECUTE 'SELECT MAX (%s) FROM (%s)', _column_name, _table_name
