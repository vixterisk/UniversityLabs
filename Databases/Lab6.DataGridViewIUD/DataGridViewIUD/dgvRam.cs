using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewIUD
{
    public partial class FormDgv : Form
    {
        private void InitializeDgvRam()
        {
            dgvRam.Rows.Clear();
            dgvRam.Columns.Clear();
            dgvRam.Columns.Add(new DataGridViewTextBoxColumn { Name = "ram_id", Visible = false });
            dgvRam.Columns.Add("ram_model", "Модель");
            var ddrColumn = new DataGridViewComboBoxColumn { Name = "ddr_generation", HeaderText = @"Поколение DDR" };
            foreach (var value in ddrGeneration.Values) ddrColumn.Items.Add(value);
            dgvRam.Columns.Add(ddrColumn);
            var sizeColumn = new DataGridViewComboBoxColumn  { Name = "ram_memory_size",HeaderText = @"Объем ОЗУ"};
            sizeColumn.ValueType = typeof(int);
            foreach (var e in new[] { 1, 2, 4, 8, 16, 32 }) sizeColumn.Items.Add(e);
            dgvRam.Columns.Add(sizeColumn);
            var makerColumn = new DataGridViewComboBoxColumn() { Name = "ram_maker_id", HeaderText = @"Производитель" };
            foreach (var value in makers.Values) makerColumn.Items.Add(value);
            dgvRam.Columns.Add(makerColumn);
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
                    dataDict["ddr_generation"] = ddrGeneration[(int)reader["ddr_generation"]];
                    dataDict["ram_maker_id"] = makers[(int)reader["ram_maker_id"]];
                    var rowIdx = dgvRam.Rows.Add(reader["ram_id"], reader["ram_model"], ddrGeneration[(int)reader["ddr_generation"]], reader["ram_memory_size"], makers[(int)reader["ram_maker_id"]]);
                    dgvRam.Rows[rowIdx].Tag = dataDict;
                }
            }
        }
        
        private void DgvRam_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = dgvRam.Rows[e.RowIndex];
            if (dgvRam.IsCurrentRowDirty)
            {
                curRow.ErrorText = "";
                if (!isIntCellCorrect(curRow.Cells["ram_memory_size"]))
                {
                    curRow.ErrorText += $"Значение в столбце '{curRow.Cells["ram_memory_size"].OwningColumn.HeaderText}' должно быть положительным числом.\n";
                    e.Cancel = true;
                }
                //Проверка, что все значения строки введены
                var cellsWithPotentialErrors = new[] { curRow.Cells["Ram_model"], curRow.Cells["ddr_generation"],curRow.Cells["ram_maker_id"] };
                foreach (var cell in cellsWithPotentialErrors)
                {
                    if (string.IsNullOrWhiteSpace((string)cell.Value))
                    {
                        curRow.ErrorText += $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым\n";
                        e.Cancel = true;
                    }
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
                        sCommand.Parameters.AddWithValue("@ram_model", Formatted(curRow.Cells["ram_model"].Value.ToString()));
                        sCommand.Parameters.AddWithValue("@ddr_generation", ddrIds[(string)curRow.Cells["ddr_generation"].Value]);
                        sCommand.Parameters.AddWithValue("@ram_memory_size", IntValueInCell(curRow.Cells["ram_memory_size"]));
                        sCommand.Parameters.AddWithValue("@ram_maker_id", makersIds[(string)curRow.Cells["ram_maker_id"].Value]);
                        var res = sCommand.ExecuteScalar();
                        if (!(res is null) && (curRow.Cells["ram_id"].Value == null || res.ToString() != curRow.Cells["ram_id"].Value.ToString()))
                        {
                            curRow.ErrorText += $"Введенная строка уже повторяет другую существющую в базе данных.\n";
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
                        sCommand.Parameters.AddWithValue("@ram_model", Formatted(curRow.Cells["ram_model"].Value.ToString()));
                        sCommand.Parameters.AddWithValue("@ddr_generation", ddrIds[(string)curRow.Cells["ddr_generation"].Value]);
                        sCommand.Parameters.AddWithValue("@ram_memory_size", IntValueInCell(curRow.Cells["ram_memory_size"]));
                        sCommand.Parameters.AddWithValue("@ram_maker_id", makersIds[(string)curRow.Cells["ram_maker_id"].Value]);
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
                        dataDict["ddr_generation"] = curRow.Cells["ddr_generation"].Value;
                        dataDict["ram_maker_id"] = curRow.Cells["ram_maker_id"].Value;
                        curRow.Tag = dataDict;
                    }

                    foreach (DataGridViewCell cell in curRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                }
            }
        }

        private void DgvRam_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvRam.Rows[e.RowIndex].IsNewRow)
            {
                dgvRam[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                if (dgvRam.Rows[e.RowIndex].Tag != null)
                    dgvRam[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                  ((Dictionary<string, object>)dgvRam.Rows[e.RowIndex]
                                                                      .Tag)[dgvRam.Columns[e.ColumnIndex].Name];
            }
        }

        private void DgvRam_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var muId = (int?)e.Row.Cells["ram_id"].Value;
            if (muId.HasValue)
            {
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

        private void DgvRam_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvRam.IsCurrentRowDirty)
            {
                dgvRam.CancelEdit();
                if (dgvRam.CurrentRow.Cells["ram_id"].Value != null)
                {
                    dgvRam.CurrentRow.ErrorText = "";
                    foreach (var kvp in (Dictionary<string, object>)dgvRam.CurrentRow.Tag)
                    {
                        dgvRam.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        dgvRam.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                }
                else
                {
                    dgvRam.Rows.Remove(dgvRam.CurrentRow);
                }
            }
        }
    }
}
