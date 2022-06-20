//GroupBox для логина, textBox растягиваем по формочке, чтобы она была резиновой
//padding обеспечивает отступы в групбоксе, чтобы строка не растягивалась на все окошко
//Кнопку регистрации прикрепить к самому низу, пикчербокс заполняет все жоступное пространство
//картинку либо добавлять в последннюю очередь, либо пикчербокс на пкм - отправить на передний план
//Кнопка загрузить в левом верхнем углу под логином, опенфайлдайалог (ofdImage) не на формочке
//errorProvider тоже не на формочке, можно выбрать иконку называем erMain
//стретчимедж в режиме пичкербокса
//пакет npgSql
//по умолчанию кнопка регистрации неактивна
//Bounty Castle - криптографическая бобилоитека для шарпа
//В поле регистрации если прописать фильтры, диалог файл не позволить выбирать что-то кроме картинок
//Блокировать при регистрации кнопку после первого нажатия, чтобы не ронять программу
//генерируем строчку, приписываем к паролю, считаем хэш

WarmUpConnection()
{
	Task.Run(() => 
	{
		using (var sConn = ...)
		{
		
		}
	}
}

private readonly Regex loginRegex = new Regex("[а-я0-9]{6,50}", RegexOptions.IgnoreCase);
private readonly string sConnStr = new NpgsqlConnectionStringBuilder
{
	Host = "192.168.56.1",//
	Port = 5432,
	Database = "opendata_pk",
	Username = "postgres",
	Password = "root"
}.ConnectionString;

//обработчик нажатия на загрузить
id (ofdImage.ShowDialog() == DialogResult.OK)
{
	pbPassword.ImageLocation = ofdImage.FileName;
	
}

//Логин изменен
if (!loginRegex.IsMatch(tbLogin.Text))
{
	erMain.SetError(tbLogin, "Логин кириллицей,6-50 символов"); //errorProvider
	btRegister.Enabled = false;
}
using (var sConn = new NpgsqlConnection(sConnStr))
{
	sConn.Open();
	var sCommand = new NpgsqlCommand
	{
		Connection = sConn,
		CommandText = @"
		SELECT COUNT(*) FROM users WHERE lower(login)=lower(@loginFromTb)"
	};
	sCommand.Parameters.AddWithValue("@loginFromTb", tbLogin.Text);
	if ((long)  sCommand.ExecuteScalar() > 0)
	{
		erMain.SetError(tbLogin, "Такой логин уже зарегистрирован"); //errorProvider
		btRegister.Enabled = false;
	}
	else
	{
		erMain.SetError(tbLogin, ""); //Чтобы перестал ругаться
		btRegister.Enabled = true;
	}
}

