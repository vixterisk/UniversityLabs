using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM
{
    public partial class FormTables : Form
    {
        private readonly string _sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = Database.Default.Host,
            Port = Database.Default.Port,
            Database = Database.Default.Name,
            Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
            Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD"),
            MaxAutoPrepare = 10,
            AutoPrepareMinUsages = 2
        }.ConnectionString; 

        private void InitializeRamInformationDGV()
        {
            ramInformationDGV.Rows.Clear();
            ramInformationDGV.Columns.Clear();
            ramInformationDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "ram_id", Visible = false });
            ramInformationDGV.Columns.Add("ram_model", "Модель");
            var ddrColumn = new DataGridViewComboBoxColumn { Name = "ddr_generation", HeaderText = @"Поколение DDR" };
            using (var pc = new pcModel())
            {
                foreach (var ddr in pc.ddr_generation)
                {
                    ddrColumn.Items.Add(ddr.generation_name);
                }
            }
            ddrColumn.Items.Add("Добавить поколение DDR");
            ramInformationDGV.Columns.Add(ddrColumn);
            var sizeColumn = new DataGridViewComboBoxColumn { Name = "ram_memory_size", HeaderText = @"Объем ОЗУ" };
            sizeColumn.ValueType = typeof(int);
            foreach (var e in new[] { 1, 2, 4, 8, 16, 32 }) sizeColumn.Items.Add(e);
            ramInformationDGV.Columns.Add(sizeColumn);
            var makerColumn = new DataGridViewComboBoxColumn() { Name = "ram_maker_id", HeaderText = @"Производитель" };
            using (var pc = new pcModel())
            {
                foreach (var maker in pc.maker)
                {
                    makerColumn.Items.Add(maker.maker_name);
                }
            }
            ramInformationDGV.Columns.Add(makerColumn);
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT ram_id, ram_model, ddr_generation, ram_memory_size, ram_maker_id
                                    FROM pcbs.ram_information"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dataDict = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "ram_id", "ram_model", "ddr_generation", "ram_memory_size", "ram_maker_id" })
                    {
                        dataDict[columnsName] = reader[columnsName];
                    }
                    var rowIdx = ramInformationDGV.Rows.Add(reader["ram_id"], reader["ram_model"], ddrDict[(int)reader["ddr_generation"]], reader["ram_memory_size"], makersDict[(int)reader["ram_maker_id"]]);
                    ramInformationDGV.Rows[rowIdx].Tag = dataDict;
                }
            }
        }

        private void ramInformationDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var curRow = ramInformationDGV.Rows[e.RowIndex];
            var curCol = ramInformationDGV.Columns[e.ColumnIndex];
            var cell = curRow.Cells[e.ColumnIndex];
            if (e.ColumnIndex == 2)
            {
                if (cell.Value != null && cell.Value.ToString() == "Добавить поколение DDR")
                {
                    ddr_generation ddr = null;
                    var addDDR = new AddDDRForm();
                    addDDR.ShowDialog();
                    if (addDDR.DialogResult == DialogResult.OK)
                    {
                        ddr = (ddr_generation)addDDR.Tag;
                    }
                    else
                    {
                        return;
                    }
                    FillDictionaries();
                    InitializeDdrDGV();
                    var newDDR = (DataGridViewComboBoxColumn)curCol;
                    newDDR.Items.Remove("Добавить поколение DDR");
                    newDDR.Items.Add(ddr.generation_name);
                    newDDR.Items.Add("Добавить поколение DDR");
                    curRow.Cells["ddr_generation"].Value = ddr.generation_name;
                }
            }
        }

        private void ramInformationDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                if (!ramInformationDGV.Rows[e.RowIndex].IsNewRow)
                {
                    ramInformationDGV[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                }
        }
        
        private void ramInformationDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                var muId = (int?)e.Row.Cells["ram_id"].Value;
                if (muId.HasValue)
                {
                    using (var pcbs = new pcModel())
                    {
                        foreach (var pcRam in pcbs.pc_ram)
                        {
                            if (pcRam.ram_id == muId)
                            {
                                MessageBox.Show("Данная модель ОЗУ используется в других таблицах. Необходимо удалить или изменить относящиеся к нему строки в этих таблицах.", "Ошибка удаления");
                                e.Cancel = true;
                                return;
                            }
                        }
                    }
                    using (var sConn = new NpgsqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new NpgsqlCommand
                        {
                            Connection = sConn,
                            CommandText = "DELETE FROM pcbs.ram_information WHERE ram_id = @MuID"
                        };
                        sCommand.Parameters.AddWithValue("@MuID", muId.Value);
                        sCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Данная модель ОЗУ используется в других таблицах. Необходимо удалить или изменить относящиеся к нему строки в этих таблицах.", "Ошибка удаления");
                e.Cancel = true;
                return;
            }
            FillDictionaries();
            InitializePcRamDGV();
        }

        private void ramInformationDGV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && ramInformationDGV.IsCurrentRowDirty)
            {
                ramInformationDGV.CancelEdit();
                if (ramInformationDGV.CurrentRow.Cells["ram_id"].Value != null)
                {
                    ramInformationDGV.CurrentRow.ErrorText = "";
                    foreach (var kvp in (Dictionary<string, object>)ramInformationDGV.CurrentRow.Tag)
                    {
                        if (kvp.Key == "ddr_generation")
                            ramInformationDGV.CurrentRow.Cells[kvp.Key].Value = ddrDict[(int)kvp.Value];
                        else if (kvp.Key == "ram_maker_id")
                                ramInformationDGV.CurrentRow.Cells[kvp.Key].Value = makersDict[(int)kvp.Value];
                        else
                        ramInformationDGV.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        ramInformationDGV.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                }
                else
                {
                    ramInformationDGV.Rows.Remove(ramInformationDGV.CurrentRow);
                }
            }
        }

        int IntValueInCell(DataGridViewCell cell)
        {
            return Int32.Parse(cell.Value.ToString());
        }

        private void ramInformationDGV_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            ramInformationDGV.EndEdit();
            var curRow = ramInformationDGV.Rows[e.RowIndex];
            if (ramInformationDGV.IsCurrentRowDirty)
            {
                curRow.ErrorText = ""; 
                //Проверка, что все значения строки введены
                var cellsWithPotentialErrors = new[] { curRow.Cells["Ram_model"], curRow.Cells["ram_memory_size"], curRow.Cells["ddr_generation"], curRow.Cells["ram_maker_id"] };
                foreach (var cell in cellsWithPotentialErrors)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace((string)cell.Value))
                        {
                            curRow.ErrorText += $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым\n";
                            e.Cancel = true;
                        }
                    }
                    catch
                    {
                        if (cell.Value == null)
                        {
                            curRow.ErrorText += $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым\n";
                            e.Cancel = true;
                        }
                    }
                }
                if (curRow.Cells["ddr_generation"].Value.ToString() == "Добавить поколение DDR")
                {
                    curRow.ErrorText += $"Необходимо выбрать существующее поколение DDR либо завершить добавление нового.\n";
                    e.Cancel = true;
                }
                //Проверка, что мы не создаем дубликат существующей строки
                if (!e.Cancel)
                {
                    using (var sConn = new NpgsqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new NpgsqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"SELECT ram_id from pcbs.ram_information
                                            where lower(ram_model) = @ram_model and ddr_generation = @ddr_generation and ram_memory_size = @ram_memory_size and ram_maker_id = @ram_maker_id"
                        };
                        sCommand.Parameters.AddWithValue("@ram_model", NameCheck.Formatted(curRow.Cells["ram_model"].Value.ToString()));
                        sCommand.Parameters.AddWithValue("@ddr_generation", ddrIdsDict[(string)curRow.Cells["ddr_generation"].Value]);
                        sCommand.Parameters.AddWithValue("@ram_memory_size", IntValueInCell(curRow.Cells["ram_memory_size"]));
                        sCommand.Parameters.AddWithValue("@ram_maker_id", makersIdsDict[(string)curRow.Cells["ram_maker_id"].Value]);
                        var res = sCommand.ExecuteScalar();
                        if (!(res is null) && (curRow.Cells["ram_id"].Value == null || res.ToString() != curRow.Cells["ram_id"].Value.ToString()))
                        {
                            curRow.ErrorText += $"Введенная строка уже повторяет другую существующую в базе данных.\n";
                            e.Cancel = true;
                        }
                    }
                }
                if (!e.Cancel)
                {
                    using (var sConn = new NpgsqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new NpgsqlCommand
                        {
                            Connection = sConn
                        };
                        sCommand.Parameters.AddWithValue("@ram_model", NameCheck.WhitespaceFormat(curRow.Cells["ram_model"].Value.ToString()));
                        sCommand.Parameters.AddWithValue("@ddr_generation", ddrIdsDict[(string)curRow.Cells["ddr_generation"].Value]);
                        sCommand.Parameters.AddWithValue("@ram_memory_size", IntValueInCell(curRow.Cells["ram_memory_size"]));
                        sCommand.Parameters.AddWithValue("@ram_maker_id", makersIdsDict[(string)curRow.Cells["ram_maker_id"].Value]);
                        var muId = (int?)curRow.Cells["ram_id"].Value;
                        if (muId.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE pcbs.ram_information SET ram_model = @ram_model, ddr_generation = @ddr_generation, ram_memory_size = @ram_memory_size, 
                                                            ram_maker_id = @ram_maker_id
                                                    WHERE ram_id = @MuID";
                            sCommand.Parameters.AddWithValue("@MuID", muId.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO pcbs.ram_information(ram_model,ddr_generation, ram_memory_size, ram_maker_id)
                                                     VALUES (@ram_model, @ddr_generation, @ram_memory_size, @ram_maker_id)
                                                     RETURNING ram_id";
                            curRow.Cells["ram_id"].Value = sCommand.ExecuteScalar();
                        }

                        var dataDict = new Dictionary<string, object>();
                        foreach (var columnsName in new[] { "ram_model", "ram_memory_size" })
                        {
                            dataDict[columnsName] = curRow.Cells[columnsName].Value;
                        }
                        dataDict["ddr_generation"] = ddrIdsDict[curRow.Cells["ddr_generation"].Value.ToString()];
                        dataDict["ram_maker_id"] = makersIdsDict[curRow.Cells["ram_maker_id"].Value.ToString()];
                        curRow.Tag = dataDict;
                    }
                    foreach (DataGridViewCell cell in curRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                    FillDictionaries();
                    InitializePcRamDGV();
                }
            }
        }
    }
}
