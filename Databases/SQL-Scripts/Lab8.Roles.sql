--Гость может только просматривать отдельные компоненты пк и названия производителей
--Не имеет доступа к таблицам, объединяющим компоненты в сборку ПК.
CREATE USER pcbs_guest WITH PASSWORD '1234';

GRANT USAGE on schema pcbs to pcbs_guest;

GRANT Select (ram_model, ddr_generation, ram_memory_size, ram_maker_id) on ram_information to pcbs_guest;
GRANT Select on ddr_generation to pcbs_guest;
GRANT Select (video_card_model, resolution_id, video_card_maker_id) on video_card_information to pcbs_guest;
GRANT Select on resolution to pcbs_guest;
GRANT Select on maker to pcbs_guest;
GRANT Select (case_model, case_form_factor_id, case_maker_id) on case_information to pcbs_guest;
GRANT Select on case_form_factor to pcbs_guest;
GRANT Select on cpu_core_information to pcbs_guest;
GRANT Select (hdd_model, hdd_memory_size, hdd_maker_id) on hdd_information to pcbs_guest;
GRANT Select (cpu_model, cpu_maker_id, core_id) on cpu_information to pcbs_guest;

SET ROLE pcbs_guest;

select ram_model, ram_memory_size, maker_name from ram_information, maker
    where ram_maker_id = maker_id;
--Пробуем прочитать больше того, что разрешено
DO
$$
    BEGIN
        select * from ram_information;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;
--Пробуем прочитать из таблицы, которую нам не дали
DO
$$
    BEGIN
        select * from pc_information;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;
--Пробуем вставить в таблицу, к которой нет доступа на запись
DO
$$
    BEGIN
        insert into ram_information (ram_model) values ('hey, this is pcbs_guest');
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;
--Пробуем изменить строки в таблице, у которой нет доступа на запись
DO
$$
    BEGIN
        update ram_information set ram_model = 'I broke everything' where true;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;
--Пробуем удалить из таблицы, к которой нет доступа на запись
DO
$$
    BEGIN
        delete from ram_information where true;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

SET ROLE None;
--Пользователи могут просматривать все таблицы, но не могут ничего редактировать
CREATE USER pcbs_user WITH PASSWORD 'qwerty';

GRANT USAGE on schema pcbs to pcbs_user;

GRANT USAGE ON SCHEMA pcbs TO pcbs_user;
Grant Select on
    pcbs.case_form_factor,
    pcbs.case_information,
    pcbs.cpu_core_information,
    pcbs.cpu_information,
    pcbs.ddr_generation,
    pcbs.hdd_information,
    pcbs.maker,
    pcbs.pc_information,
    pcbs.pc_ram,
    pcbs.pc_video_card,
    pcbs.ram_information,
    pcbs.resolution,
    pcbs.video_card_information to pcbs_user;

SET ROLE pcbs_user;

select pc_id, hdd_model, hdd_memory_size, cpu_model, core_name, case_model, form_factor
from pc_information, hdd_information, cpu_information, cpu_core_information,case_information,case_form_factor
where pc_information.hdd_id = hdd_information.hdd_id and
      pc_information.cpu_id = cpu_information.cpu_id and
      cpu_information.core_id = cpu_core_information.core_id and
      pc_information.case_id = case_information.case_id and
      case_form_factor.case_form_factor_id = case_information.case_form_factor_id;

 DO
$$
    BEGIN
        insert into ram_information (ram_model) values ('hey, this is pcbs_user');
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

DO
$$
    BEGIN
        update ram_information set ram_model = 'I broke everything' where true;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

DO
$$
    BEGIN
        delete from ram_information where true;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

SET ROLE None;
--Техники могут просматривать и редактировать таблицы, относящиеся к сборке и конфигурации пк,
--только просматривать таблицы, относящиеся к моделям оборудования и производителям
CREATE USER pcbs_configurator WITH PASSWORD '2468';

GRANT pcbs_user to pcbs_configurator;
GRANT INSERT, UPDATE, DELETE ON TABLE pc_information, pc_ram, pc_video_card to pcbs_configurator;
GRANT USAGE, SELECT on pc_information_pc_id_seq to pcbs_configurator;

SET ROLE pcbs_configurator;

select pc_id, pc_name, hdd_model, hdd_memory_size, cpu_model, core_name, case_model, form_factor
from pc_information, hdd_information, cpu_information, cpu_core_information,case_information,case_form_factor
where pc_information.hdd_id = hdd_information.hdd_id and
      pc_information.cpu_id = cpu_information.cpu_id and
      cpu_information.core_id = cpu_core_information.core_id and
      pc_information.case_id = case_information.case_id and
      case_form_factor.case_form_factor_id = case_information.case_form_factor_id;

insert into pc_information (cpu_id, hdd_id, case_id, pc_name) values (1,21,2,'pc2');

select pc_id, pc_name, hdd_model, hdd_memory_size, cpu_model, core_name, case_model, form_factor
from pc_information, hdd_information, cpu_information, cpu_core_information,case_information,case_form_factor
where pc_information.hdd_id = hdd_information.hdd_id and
      pc_information.cpu_id = cpu_information.cpu_id and
      cpu_information.core_id = cpu_core_information.core_id and
      pc_information.case_id = case_information.case_id and
      case_form_factor.case_form_factor_id = case_information.case_form_factor_id;

update pc_information set pc_name = 'pc3' where pc_name = 'pc2';

select pc_id, pc_name, hdd_model, hdd_memory_size, cpu_model, core_name, case_model, form_factor
from pc_information, hdd_information, cpu_information, cpu_core_information,case_information,case_form_factor
where pc_information.hdd_id = hdd_information.hdd_id and
      pc_information.cpu_id = cpu_information.cpu_id and
      cpu_information.core_id = cpu_core_information.core_id and
      pc_information.case_id = case_information.case_id and
      case_form_factor.case_form_factor_id = case_information.case_form_factor_id;

delete from pc_information where pc_name = 'pc3';

select pc_id, pc_name, hdd_model, hdd_memory_size, cpu_model, core_name, case_model, form_factor
from pc_information, hdd_information, cpu_information, cpu_core_information,case_information,case_form_factor
where pc_information.hdd_id = hdd_information.hdd_id and
      pc_information.cpu_id = cpu_information.cpu_id and
      cpu_information.core_id = cpu_core_information.core_id and
      pc_information.case_id = case_information.case_id and
      case_form_factor.case_form_factor_id = case_information.case_form_factor_id;

DO
$$
    BEGIN
        delete from ram_information where true;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

SET ROLE None;
--Производители могут просматривать сборки ПК,
-- просматривать информацию о моделях оборудования и редактировать информацию о своих
-- просматривать информацию о производителях и редактировать свою
CREATE USER pcbs_maker WITH PASSWORD '1357';

GRANT USAGE on schema pcbs to pcbs_maker;
Grant Select, INSERT, UPDATE on
    pcbs.case_form_factor,
    pcbs.case_information,
    pcbs.cpu_core_information,
    pcbs.cpu_information,
    pcbs.ddr_generation,
    pcbs.hdd_information,
    pcbs.ram_information,
    pcbs.resolution,
    pcbs.video_card_information to pcbs_maker;

Grant Select on pcbs.maker to pcbs_maker;
Grant update (maker_information) on pcbs.maker to pcbs_maker;

GRANT USAGE, Select on
    case_form_factor_case_form_factor_id_seq,
    case_information_case_id_seq,
    hdd_information_hdd_id_seq,
    pc_information_pc_id_seq,
    pc_video_card_pc_id_seq,
    ram_information_ram_id_seq,
    video_card_information_video_card_id_seq to pcbs_maker;

Alter table maker
enable row level security;

create policy maker_policy
on maker
using (lower(maker_name) = lower(current_user));

Alter table case_information
enable row level security;

create policy case_policy
on case_information
using (case_maker_id in (select maker_id from maker where lower(maker_name) = lower(current_user)));

Alter table cpu_core_information
enable row level security;

create policy core_policy
on cpu_core_information
using (cpu_core_maker_id in (select maker_id from maker where lower(maker_name) = lower(current_user)));

Alter table cpu_information
enable row level security;

create policy cpu_policy
on cpu_information
using (cpu_maker_id in (select maker_id from maker where lower(maker_name) = lower(current_user)));

Alter table hdd_information
enable row level security;

create policy hdd_policy
on hdd_information
using (hdd_maker_id in (select maker_id from maker where lower(maker_name) = lower(current_user)));

Alter table ram_information
enable row level security;

create policy ram_policy
on ram_information
using (ram_maker_id in (select maker_id from maker where lower(maker_name) = lower(current_user)));

Alter table video_card_information
enable row level security;

create policy video_card_policy
on video_card_information
using (video_card_maker_id in (select maker_id from maker where lower(maker_name) = lower(current_user)));

create user msi;
grant pcbs_maker to msi;

set role msi;

select * from maker;
update maker set maker_information = 'We`re making motherboards';

DO
$$
    BEGIN
        insert into maker (maker_id, maker_name, maker_information) values (100, 'razer', 'Annotation');
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;
DO
$$
    BEGIN
        delete from maker;
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

set role None;

create user zalman;
grant pcbs_maker to zalman;

set role zalman;

select * from case_information;

insert into case_information (case_model, case_form_factor_id, case_maker_id) values ('ATX i3', 1, 6);
update case_information set case_form_factor_id = 2;

DO
$$
    BEGIN
        insert into case_information (case_model, case_form_factor_id, case_maker_id) values ('ATX i3', 1, 5);
    EXCEPTION
        WHEN OTHERS THEN RAISE NOTICE
            'Ошибка доступа: %', sqlerrm;
    END;
$$;

set role None;
-- Удаление всего
alter table maker
disable row level security;
drop policy maker_policy on maker;

alter table case_information
disable row level security;
drop policy case_policy on case_information;

alter table cpu_core_information
disable row level security;
drop policy core_policy on cpu_core_information;

alter table cpu_information
disable row level security;
drop policy cpu_policy on cpu_information;

alter table hdd_information
disable row level security;
drop policy hdd_policy on hdd_information;

alter table ram_information
disable row level security;
drop policy ram_policy on ram_information;

alter table video_card_information
disable row level security;
drop policy video_card_policy on video_card_information;

drop owned by pcbs_guest;
drop user pcbs_guest;

drop owned by pcbs_user;
drop user pcbs_user;

drop owned by pcbs_configurator;
drop user pcbs_configurator;

drop owned by pcbs_maker;
drop user pcbs_maker;

drop owned by msi;
drop user msi;

drop owned by zalman;
drop user zalman;