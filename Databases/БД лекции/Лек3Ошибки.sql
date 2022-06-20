--ошибка класса n - первые 2 цифры, если после них идут нули, ловятся все ошибки класса

CREATE OR REPLACE PROCEDURE error_example(
		_mu_name municipal_units.name%type, 
		_command varchar) AS
	$$
	BEGIN
		ASSERT EXISTS (SELECT * FROM municipal_units WHERE name = _mu_name),
		'МО с таким названием нет';
		EXECUTE _command USING _mu_name;
	END;
	$$ LANGUAGE plpgsql;
	
	--анонимный блок кода, выполняется сразу после своего объявления
	DO $$
		BEGIN
			CALL error_example( 'Александровский муниципальный район',  'SELECT $1');
			RAISE EXCEPTION 'Текст ошибки %', 'переменная' 
				USING HINT = 'Подсказка - не делайте так';
			CALL error_example( 'Александровский муниципальный район',  'SELECT $348670rokdj409%$$#^^1');
			CALL error_example( 'Центральный муниципальный район',  NULL);
		EXCEPTION
			WHEN  assert_failure THEN
				RAISE NOTICE 'ой';
			WHEN syntax_error_or_access_rule_violation THEN
				RAISE NOTICE 'ой на класс';
			WHEN OTHERS
			THEN
				RAISE NOTICE 'ой на все остальное';
		END;
		$$