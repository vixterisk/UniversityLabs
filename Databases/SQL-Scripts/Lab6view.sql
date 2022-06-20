--Создаем представление
Create or replace view mod_table as
SELECT distinct pc_name, video_card_slot_number,video_card_model
from pc_information
join pc_video_card on pc_video_card.pc_id = pc_information.pc_id
join video_card_information on pc_video_card.video_card_id = video_card_information.video_card_id
order by pc_name, video_card_slot_number;

--Функция для триггера вставки
CREATE OR REPLACE FUNCTION mod_table_instead_of_insert()
    RETURNS TRIGGER AS
$$
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
$$ LANGUAGE plpgsql;

--Создание триггера
CREATE TRIGGER mod_table_insert_trigger
    INSTEAD OF INSERT
    ON mod_table
    FOR EACH ROW
EXECUTE FUNCTION mod_table_instead_of_insert();

--Добавляем новые кортежи с записями, отсутствующими в обеих таблицах
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('jun14', 1, 'Strix R3 280');

--Добавляем новые кортежи с записями, одна из которых отсутствует в таблице видеокарт
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('jun14', 1, 'Strix R7 370');

--Добавляем новые кортежи с записями, одна из которых отсутствует в таблице ПК
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 1, 'Strix R7 370');

--Добавляем новые кортежи с записями, которые имеются в обеих таблицах
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 1, 'Strix R7 370');

--Добавляем уже существующие кортежи с записями, которые имеются в обеих таблицах, но с другим слотом
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 2, 'Strix R7 370');

--Добавляем несколько кортежей
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('user1', 1, 'videocard1'),('user2', 2, 'videocard2'), ('user3', 1, 'videocard3');

table mod_table;

--Функция для триггера удаления
CREATE OR REPLACE FUNCTION mod_table_instead_of_delete()
    RETURNS TRIGGER AS
$$
DECLARE
    video_card_id_var integer;
    pc_id_var integer;
BEGIN
    SELECT video_card_id from video_card_information where old.video_card_model = video_card_information.video_card_model INTO video_card_id_var;
    SELECT pc_id from pc_information where old.pc_name = pc_information.pc_name INTO pc_id_var;
    delete from pc_video_card where pc_id_var = pc_video_card.pc_id and video_card_id_var=pc_video_card.video_card_id and pc_video_card.video_card_slot_number = old.video_card_slot_number;
    RETURN old;
END;
$$ LANGUAGE plpgsql;

--Создание триггера
CREATE TRIGGER mod_table_delete_trigger
    INSTEAD OF DELETE
    ON mod_table
    FOR EACH ROW
EXECUTE FUNCTION mod_table_instead_of_delete();

--Удаляем одну запись, где ПК имеет 2 одинаковые видеокарты в 2 слотах
DELETE FROM mod_table where video_card_model = 'Strix R7 370' and pc_name = 'Uni' and video_card_slot_number = 1;

insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 1, 'Strix R7 370');
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 2, 'Strix R7 370');
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 3, 'Strix R3 280');

--Удаляем запись по ПК
DELETE FROM mod_table where pc_name = 'Uni';

insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 1, 'Strix R7 370');
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 2, 'Strix R7 370');
insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('Uni', 3, 'Strix R3 280');

--Удаляем запись по видеокарте
DELETE FROM mod_table where video_card_model = 'Strix R7 370';

--Функция для триггера обновления
CREATE OR REPLACE FUNCTION mod_table_instead_of_update()
    RETURNS TRIGGER AS
$$
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
$$ LANGUAGE plpgsql;

--Создание триггера
CREATE TRIGGER mod_table_update_trigger
    INSTEAD OF UPDATE
    ON mod_table
    FOR EACH ROW
EXECUTE FUNCTION mod_table_instead_of_update();

insert into mod_table(pc_name, video_card_slot_number, video_card_model) VALUES ('user1', 1, 'videocard3'),('user2', 2, 'videocard3'), ('user3', 1, 'videocard3');

--Меняем видеокарту на уже существующую
update mod_table set video_card_model = 'GeForce GTX 1050 Ti' where pc_name = 'user2';

--Меняем видеокарту на новую
update mod_table set video_card_model = 'videocard3' where pc_name = 'user2';
--Меняем сразу все
update mod_table set video_card_model = 'update test videocard', pc_name = 'update test user' where video_card_model = 'videocard3' and pc_name = 'user2';

drop trigger mod_table_delete_trigger on mod_table;
drop function mod_table_instead_of_delete();
drop trigger mod_table_insert_trigger on mod_table;
drop function mod_table_instead_of_insert();
drop trigger mod_table_update_trigger on mod_table;
drop function mod_table_instead_of_update();
drop view mod_table;
