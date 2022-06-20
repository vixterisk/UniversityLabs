//ListView, MenuStrip
//FullRowSelect - true
//Правка - добавить, удалить, изменить
//Npgsql

//после initializeComponent initializeForm

//Поле в форме
private readonly string sConnStr = new NpgsqlConnectionStringBuilder
{
	Host = Database.Default.Host,
	Port = Database.default.Port.
	Database = Database.Default.Name
	Username = Enviroment.GetEnviromentVariable("POSTGRESQL_USERNAME"),
	Password = Enviroment.GetEnviromentVariable("POSTGRESQL_PASSWORD"),  
	AutoPrepareMinUsages = 2, //сколько запросов для запоминания
	MaxAutoPrepare = 10 //сколько действий запоминать
}.Connection.String;

//Добавить файл настроек для проекта - Host, Port, Name, Setting все стринг кроме порта (инт)
//Переменные окружения для логина и пароля подключения 
//Панель управления - учетные записи юзеров - поменять переменные окружения

//InitializeForm
{
	lvMu.Columns.Add("Название");
	lvMu.Columns.Add("Глава"); /... добавляем столбцы в листВью
	using (var sCOnn = new NpgsqlConnection(sConnStr))
	{
		s.Conn.Open(); 
		var sCommand = new NpgsqlCommand
		{
			Connection = sConn,
			CommandText = @"
				SELECT id, name, head, adress, modified_date
				FROM municipal_units"
		};
		var reader = sCommand.ExecuteReader();
		while(reader.Read())
		{
			var lvi = new ListViewItem(new []
			{
				(string)reader["name"],
				//...
				((DateTime) reader["modified_date"]).ToLongDateString(),
			});
			lvi.Tag = reader["id"];
			lvMu.Items.Add(lvi);
		}
	}
	lvMu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
	lvMu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
}

//Нажатие на удалить
{
	using (var sCOnn = new NpgsqlConnection(sConnStr))
	{
		s.Conn.Open(); 
		var sCommand = new NpgsqlCommand
		{
			Connection = sConn,
			CommandText = @"
				DELETE FROM municipal_units WHERE id = @muId"
		};
		foreach (ListViewItem item in lvMu.SelectedItems)
		{
			sCommand.Parameters.AddWithValue("@muId", item.Tag);
			sCommand.Parameters.Clear();
			sCommand.ExecuteNonQuary();
			lvMu.Items.Remove(item);
		}
		/*Либо
		var sCommand = new NpgsqlCommand
		{
			Connection = sConn,
			CommandText = @"
				DELETE FROM municipal_units WHERE id = ANY (@muIds)"
		};
		var muIds =
			from item
			in lvMu.SelectedItems.Cast<ListViewItem>()
			select (int)item.Tag;
		sCommand.Parameters.AddWithValue("@muIds", muIds.ToArray());
		sCommand.ExecuteNonQuary();
		foreach (ListViewItem item in lvMu.SelectedItems)
		{
			lvMu.Items.Remove(item);
		}
		*/
	}
}

/*DialogResult - OK у кнопки на формочке регистрации
Свойства у формочки, возвращающие текст в текстбоксах
enum - add, change
case FormType.Insert:
	btOk.Txt = @"Добавить";
Сохранять modified_date в виде кортежа id, modified_date
ExecuteScalar -  вставка данных

Insert Into municipal...
Values(...)
Returning id

Для сохранения в тег

MuName = selectedItem.SubItems[0].Text;
*/