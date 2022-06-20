using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORM2
{
    public partial class MainForm
    {
        //Инициализация таблицы форм-факторов
        private void InitializeDgvCaseFormFactor()
        {
            dgvCaseFormFactor.Rows.Clear();
            dgvCaseFormFactor.Columns.Clear();
            dgvCaseFormFactor.Columns.Add(new DataGridViewTextBoxColumn { Name = "case_form_factor_id", Visible = false });
            dgvCaseFormFactor.Columns.Add("form_factor", "Форм-фактор");
            using (var pcbs = new PcbsModel())
            {
                foreach (var elem in pcbs.case_form_factor)
                {
                    var rowId = dgvCaseFormFactor.Rows.Add(elem.case_form_factor_id, elem.form_factor);
                    dgvCaseFormFactor.Rows[rowId].Tag = elem;
                }
            }
        }
        //Проверка строки (И дальнейшая вставка/изменение)
        private void dgvCaseFormFactor_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = dgvCaseFormFactor.Rows[e.RowIndex]; 
            curRow.ErrorText = "";
            if (dgvCaseFormFactor.IsCurrentRowDirty)
            {
                //Проверка на пустоту ячейки
                if (string.IsNullOrWhiteSpace((string)curRow.Cells["form_factor"].Value))
                {
                    curRow.ErrorText += $"Значение в столбце '{curRow.Cells["form_factor"].OwningColumn.HeaderText}' не может быть пустым\n";
                    e.Cancel = true;
                }
                //Проверка на повторение значений
                if (!e.Cancel)
                    using (var pcbs = new PcbsModel())
                    {
                        foreach (var elem in pcbs.case_form_factor)
                        {
                            if (Formatted(elem.form_factor) == Formatted(curRow.Cells["form_factor"].Value.ToString()) &&
                                (curRow.Cells["case_form_factor_id"].Value == null || elem.case_form_factor_id != (int)curRow.Cells["case_form_factor_id"].Value))
                            {
                                curRow.ErrorText += $"Строка повторяет уже существующую в таблице\n";
                                e.Cancel = true;
                            }
                        }
                    }
                if (!e.Cancel)
                {
                    var muId = (int?)curRow.Cells["case_form_factor_id"].Value;
                    if (muId == null)
                    {
                        var item = new case_form_factor { form_factor = WhitespaceFormat(curRow.Cells["form_factor"].Value.ToString()) };
                        using (var pcbs = new PcbsModel())
                        {
                            pcbs.case_form_factor.Add(item);
                            pcbs.SaveChanges();
                            curRow.Tag = item;
                            dgvCaseFormFactor.CurrentRow.Cells["case_form_factor_id"].Value = item.case_form_factor_id;
                        }
                    }
                    else
                    {
                        using (var pcbs = new PcbsModel())
                        {
                            var item = (case_form_factor)curRow.Tag;
                            pcbs.case_form_factor.Attach(item);
                            item.form_factor = WhitespaceFormat(curRow.Cells["form_factor"].Value.ToString());
                            pcbs.SaveChanges();
                            curRow.Tag = item;
                        }
                    }
                    InitializeDgvCaseInformation();
                    foreach (DataGridViewCell cell in curRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                }
            }
        }
        private void dgvCaseFormFactor_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvCaseFormFactor.Rows[e.RowIndex].IsNewRow)
            {
                dgvCaseFormFactor[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
            }
        }
        //Отмена введенных значений, если они не приняты
        private void dgvCaseFormFactor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvCaseFormFactor.IsCurrentRowDirty)
            {
                dgvCaseFormFactor.CancelEdit();
                if (dgvCaseFormFactor.CurrentRow.Cells["case_form_factor_id"].Value != null)
                {
                    dgvCaseFormFactor.CurrentRow.ErrorText = "";
                    var item = (case_form_factor)dgvCaseFormFactor.CurrentRow.Tag;
                    dgvCaseFormFactor.CurrentRow.Cells["form_factor"].Value = item.form_factor;
                    dgvCaseFormFactor.CurrentRow.Cells["form_factor"].ErrorText = string.Empty;
                }
                else
                {
                    dgvCaseFormFactor.Rows.Remove(dgvCaseFormFactor.CurrentRow);
                }
            }
        }
        //Удаление строки
        private void dgvCaseFormFactor_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var pcbs = new PcbsModel())
            {
                try
                {
                    var item = (case_form_factor)e.Row.Tag;
                    if (item != null)
                    {
                        pcbs.case_form_factor.Attach(item);
                        pcbs.case_form_factor.Remove(item);
                        pcbs.SaveChanges();
                        InitializeDgvCaseInformation();
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException) 
                {
                    MessageBox.Show("Данный форм-фактор используется в других таблицах. Необходимо удалить или изменить относящиеся к нему строки в этих таблицах.", "Ошибка удаления");
                    e.Cancel = true;
                }
            }
        }
    }
}
