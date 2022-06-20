//datagridview растягиваем на все окно autosizecolumnsmode = fill, rowValidating =, userDeletingRow = ,

private void InitializeDgvMu()
{
	dgvMu.Columns.Add(new DataGridViewTextBoxColumn{
		Name = "id",
		Visible = false
	});
	dgvMu.Columns.Add("name", "Название");
	dgv.Columns.Add("head", "Глава");
	dgv.Columns.Add("adress","Адрес");
	dgv.Columns.Add(new CalendarColumn{
		Name = "modified_date",
		HeaderText = "Дата последнего внесения изменений"
	});
	using (var sConn = new NpgsqlConnection(sConnStr))
	{
		sConn.Open();
		var sCommand = new NpgsqlCommand
		{
			Connection = sConn,
			CommandText = "SELECT id,name,head,address,modified_date from municipal_units"
		};
		var reader = sCommand.ExecuteReader();
		while(reader.Read())
		{
			dgvMu.Rows.Add(reader["id"], reader["name"], reader["head"], reader["address"],reader["modified_date"]);
		}
	}
	//host controls in wondows forms datagridview cell - взять весь класс calendarColumn
}

private void dgvMu_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
{
	if (!dgvMu.IsCurrentRowDirty)
		return;
	var row = dgvMu.Rows[e.RowIndex];
	var cellWithPotentialErrors = new[] {row.Cells["name"], row.Cells["head"], row.Cells["address"]};
	row.ErrorText = "";
	foreach (var cell in cellWithPotentialErrors)
	{
		if(!string.IsNullOrWhiteSpace((string)cell.Value))
		{
			cell.ErrorText = "Ячейка пуста";
			e.Cancel = true;
			row.ErrorText += $"Значение в столбце `{cell.OwningColumn.HeaderText}` пусто\n";
		}
		else
		{
			cell.ErrorText = "";
		}
	}
	if (e.Cancel)
		return;
	using (var sConn = new NpgsqlConnection(sConnStr))
	{
		sConn.Open();
		var sCommand = new NpgsqlCommand
		{
			Connection = sConn
		};
		sCommand.Parameters.AddWithValue("@MuName", row.Cells["name"].Value);
		sCommand.Parameters.AddWithValue("@Muhead", row.Cells["head"].Value);
		sCommand.Parameters.AddWithValue("@Muaddress", row.Cells["address"].Value);
		sCommand.Parameters.AddWithValue("@Mumodified_date", row.Cells["modified_date"].Value);
		var muId = (int?)row.Cells["id"].Value;
		if (muId.HasValue)//true, если что-то лежит, если нулл, то false
		{
			sCommand.CommandText = @"UPDATE municipal_units SET name = @MuName ... WHERE id = @muId";
			sCommand.Parameters.AddWithValue("@muId", muId.Value);
			sCommand.ExecuteNonQuary();
		}
		else
		{
			sCommand.CommandText = @"
			INSERT INTO municipal_units(name, head, address, modified_date) 
			Values (@muName...)
			RETURNING id";
			row.Cells["id"].Value = sCommand.ExecuteScalar();
		}
	}
}

private void user_delitingRow(e)
{
	var muId = (int?)e.row.Cells["id"].Value;
	if (!nuId.HasValue)
		return;
	using (var sConn = new NpgsqlConnection(sConnStr))
	{
		sConn.Open();
		var sCommand = new NpgsqlCommand
		{
			Connection = sConn,
			CommandText = @"
			DELETE FROM municipal_units WHERE id = @muId";
		};
		sCommand.Parameters.AddWithValue("@muId", nuId.Value);
		sCommand.ExecuteNonQuary();
	}
}