//объектно-реляционное отображение ORM ОРО

//работаем с ListViewUD. Выибраем extensions в tools, качаем npgsql
//entitiy framework (у java гибернейт)
// Через nuget пакеты entityFramework6.Npgsql 388к скачиваний
//сходить в updates, обновить все
//если не работает открыть консоль диспетчера пакетов Update-Package -reinstall
//Выбираем проект, выбираем добавить новый объект - ADO.NET Entity data model канистра с диаграммой, назовем opendataContext
//4 способа
//EF designer from database: БД -> Диаграмма -> код
//Empty EF Designer model: БД <- Диаграмма -> код
//Empty code first model: БД <- код
//Code first from database: БД -> код

//выбираем 4 вариант
//new connection в появившихся вариантах data source выбрать постгрес
//yes, include the sensitive data in connection string

InitializeListViewMu()

using (vat ctx = new OpendataContext())
{
	foreach(var mu in ctx.municipal_units)
	{
		var lvi = newListViewItem(new[] {	mu.name, mu.head, mu.address, mu.modified_date.ToLongDateString()	}) { Tag = mu.id };
	}
}

//удаление
using (vat ctx = new OpendataContext())
{
	foreach(var selectedItem in lvMu.SelectedItems)
	{
		var mu = (municipal_units)selectionItem.Tag;
		ctx.municipal_units.Attach(mu);
		ctx.municipal_units.Remove(mu);
		lvMu.Items.Remove(selectedItem);
	}
	ctx.SaveChanges();
}

//добавление
if (formMuInsert.ShowDialog() == DialogResult.OK)
using (var ctx = new OpendataContext())
{
	var mu = new municipal_units
	{
		name = formMuInsert.MuName//...
	};
	ctx.municipal_units.Add(mu);
	ctx.SaveChanges();
	var lvi = new ListViewItem(new [] {formMuInsert.MuName, formMuInsert.MuHead, ...}) {Tag = mu };
	lvMu.Items.Add(lvi);
}