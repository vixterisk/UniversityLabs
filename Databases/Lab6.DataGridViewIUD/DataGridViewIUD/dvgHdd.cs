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
        private void InitializeDgvHdd()
        {
            dgvHdd.Rows.Clear();
            dgvHdd.Columns.Clear();
            dgvHdd.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "hdd_id",
                Visible = false
            });
            dgvHdd.Columns.Add("hdd_model", "Модель");
            dgvHdd.Columns.Add("hdd_memory_size", "Объем памяти");
            var makerColumn = new DataGridViewComboBoxColumn()
            {
                Name = "hdd_maker_id",
                HeaderText = @"Производитель"
            };
            foreach (var value in makers.Values)
                makerColumn.Items.Add(value);
            dgvHdd.Columns.Add(makerColumn);
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT hdd_id, hdd_model, hdd_memory_size, hdd_maker_id
                                    FROM pcbs.hdd_information"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dataDict = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "hdd_id", "hdd_model", "hdd_memory_size", "hdd_maker_id" })
                    {
                        dataDict[columnsName] = reader[columnsName];
                    }
                    dataDict["hdd_maker_id"] = makers[(int)reader["hdd_maker_id"]];
                    var rowIdx = dgvHdd.Rows.Add(reader["hdd_id"], reader["hdd_model"], reader["hdd_memory_size"],
                        makers[(int)reader["hdd_maker_id"]]);
                    dgvHdd.Rows[rowIdx].Tag = dataDict;
                }
            }
        }

        private void DgvHdd_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = dgvHdd.Rows[e.RowIndex];
            if (dgvHdd.IsCurrentRowDirty)
            {
                //Проверка, что в объеме памяти положительное число
                curRow.ErrorText = "";
                if (!isIntCellCorrect(curRow.Cells["hdd_memory_size"]))
                {
                    curRow.ErrorText += $"Значение в столбце '{curRow.Cells["hdd_memory_size"].OwningColumn.HeaderText}' должно быть положительным числом.\n";
                    e.Cancel = true;
                }
                //Проверка, что все значения строки введены
                var cellsWithPotentialErrors = new[] { curRow.Cells["hdd_model"], curRow.Cells["hdd_maker_id"] };
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
                            CommandText = @"SELECT hdd_id from pcbs.hdd_information
                                            where lower(hdd_model) = @hdd_model and hdd_memory_size = @hdd_memory_size and hdd_maker_id = @hdd_maker_id"
                        };
                        sCommand.Parameters.AddWithValue("@hdd_model", Formatted(curRow.Cells["hdd_model"].Value.ToString()));
                        sCommand.Parameters.AddWithValue("@hdd_memory_size", IntValueInCell(curRow.Cells["hdd_memory_size"]));
                        sCommand.Parameters.AddWithValue("@hdd_maker_id", makersIds[(string)curRow.Cells["hdd_maker_id"].Value]);
                        var res = sCommand.ExecuteScalar();
                        if (!(res is null) && (curRow.Cells["hdd_id"].Value == null || res.ToString() != curRow.Cells["hdd_id"].Value.ToString()))
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
                        sCommand.Parameters.AddWithValue("@hdd_model", WhitespaceFormat(curRow.Cells["hdd_model"].Value.ToString()));
                        sCommand.Parameters.AddWithValue("@hdd_memory_size", IntValueInCell(curRow.Cells["hdd_memory_size"]));
                        sCommand.Parameters.AddWithValue("@hdd_maker_id", makersIds[(string)curRow.Cells["hdd_maker_id"].Value]);
                        var muId = (int?)curRow.Cells["hdd_id"].Value;
                        if (muId.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE pcbs.hdd_information SET hdd_model = @hdd_model, hdd_memory_size = @hdd_memory_size, 
                                                            hdd_maker_id = @hdd_maker_id
                                                    WHERE hdd_id = @MuID";
                            sCommand.Parameters.AddWithValue("@MuID", muId.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO pcbs.hdd_information(hdd_model, hdd_memory_size, hdd_maker_id)
                                                     VALUES (@hdd_model, @hdd_memory_size, @hdd_maker_id)
                                                     RETURNING hdd_id";
                            curRow.Cells["hdd_id"].Value = sCommand.ExecuteScalar();
                        }

                        var dataDict = new Dictionary<string, object>();
                        foreach (var columnsName in new[] { "hdd_model", "hdd_memory_size" })
                        {
                            dataDict[columnsName] = curRow.Cells[columnsName].Value;
                        }
                        dataDict["hdd_maker_id"] = curRow.Cells["hdd_maker_id"].Value;
                        curRow.Tag = dataDict;
                    }

                    foreach (DataGridViewCell cell in curRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                }
            }
        }

        private void DgvHdd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvHdd.Rows[e.RowIndex].IsNewRow)
            {
                dgvHdd[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                if (dgvHdd.Rows[e.RowIndex].Tag != null)
                    dgvHdd[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                  ((Dictionary<string, object>)dgvHdd.Rows[e.RowIndex]
                                                                      .Tag)[dgvHdd.Columns[e.ColumnIndex].Name];
            }
        }

        private void DgvHdd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var muId = (int?)e.Row.Cells["hdd_id"].Value;
            if (muId.HasValue)
            {
                using (var sConn = new NpgsqlConnection(_sConnStr))
                {
                    sConn.Open();
                    var sCommand = new NpgsqlCommand
                    {
                        Connection = sConn,
                        CommandText = "DELETE FROM pcbs.hdd_information WHERE hdd_id = @MuID"
                    };
                    sCommand.Parameters.AddWithValue("@MuID", muId.Value);
                    sCommand.ExecuteNonQuery();
                }
            }
        }

        private void DgvHdd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvHdd.IsCurrentRowDirty)
            {
                dgvHdd.CancelEdit();
                if (dgvHdd.CurrentRow.Cells["hdd_id"].Value != null)
                {
                    dgvHdd.CurrentRow.ErrorText = "";
                    foreach (var kvp in (Dictionary<string, object>)dgvHdd.CurrentRow.Tag)
                    {
                        dgvHdd.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        dgvHdd.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                }
                else
                {
                    dgvHdd.Rows.Remove(dgvHdd.CurrentRow);
                }
            }
        }
    }
}
