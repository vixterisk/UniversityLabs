create or replace view mu_totals as
SELECT municipal_units.id, SUM (number)
FROM municipal_units
JOIN resources on municipal_units.id = resources.municipal_unit_id
Group by municipal_units.id
order by municipal_units.id;

select *
from mu_totals;

create table nu_totals_cached as 
select * from mu_totals;

-- создаем вместо представления таблицу, где хранится информация
create table nu_totals_cached as 
SELECT municipal_units.id, SUM (number)
FROM municipal_units
JOIN resources on municipal_units.id = resources.municipal_unit_id
Froup by municipal_units.id
order by municipal_units.id;

truncate m_totals_cached; --удаляет(чистит) таблицу, быстрее delete

insert into mu_totals_cached(id, name,sum)
create table nu_totals_cached as 
SELECT municipal_units.id, SUM (number)
FROM municipal_units
JOIN resources on municipal_units.id = resources.municipal_unit_id
Froup by municipal_units.id
order by municipal_units.id;

--материализованые представления - кэшируем результаты запроса, который сам по себе долго выполняется
create materialized view mu_totals_mat as
SELECT municipal_units.id, SUM (number)
FROM municipal_units
JOIN resources on municipal_units.id = resources.municipal_unit_id
Group by municipal_units.id
order by municipal_units.id;

INSERT INTO resources(municipal_units_id, animal_id, number) --до этого удалили все строки где мун юнитс айди = 9
values (9,1,900);

select * from mu_totals; -- будет строка с 9
select * from mu_totals_mat; -- не будет, поскольку запомнило таблицу без 9 - работает быстрее за счет дублирования

REFRESH materialized view mu_totals_mat; 

--обновляемые представления
create or replace view mu_2 as --строки с четными айди
select *
from municipal_units
where id % 2 = 0;

select * from mu_2
order by id desc;

update mu_2 set name = 'test 4' where id = 54; --
delete from mu_2 where id = 54; 
insert into mu_2(name,head,address) values ('test 6', 'глава 6', 'адрес 6');

select * from mu_2
order by id desc;


create or replace view mu_2 as --строки с четными айди
select *
from municipal_units
where id % 2 = 0
with check option; --если модифицировать таблицу, результаты обновления должны выдаваться, или упадет с ошибкой

insert into mu_2(name,head,address) values ('test 8', 'глава 6', 'адрес 6'); -- если выдаст нечетный айди, инсерт упадет, если запустить после этого еще раз, выдаст уже четный и вставка сработает
--есть возможность создать представление на 3 таблицы, пригодное для выборки, инсерта, апдейта и делита
select municipal_units.name as mu_name, animals.name as animal_name, resources.number
from municipal_units
join resources on mubicipal_units.id = resources.municipal_units_id
join animals on resources.animal_id = animals.id;
--generate ddl to console - узнать код представления (также есть в метаданных)
table resources_modifiable_view; --(select * from ) - синт сахар
insert into resources_modifiable_view (mu_name, animal_name, number)
values ('ГО г.Кунгур','снежный баран',2); --instead of триггер, будет вставляться в таблицу resources

create or replace function resources_modifiable_table_view_instead_of_insert()
returns trigger as $$
declare
_mu_id municipal_units.id%type;
_animal_id animals.id%type;
begin
	select id from municipal_units where name = new.mu_name into _mu_id;
	select id from animals where name = new.animal_name into _animal_id;
	insert into resources(municipal_unit_id, animal_id, number)
	values (_mu_id,_animal_id, new.number)
	on conflict (municipal_unit_id, animal_id)
	do update set number = excluded.number;
	return new;
end;
$$ language plpgsql;

CREATE trigger resources_modifiable_table_view_instead_of_insert_trigger
	instead of insert
	on resources_modifiable_view
	for each row
execute function resources_modifiable_view_instead_of_insert();