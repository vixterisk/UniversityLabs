using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORM2
{
    public partial class MainForm
    {
        Dictionary<string, int> formFactorsDict;
        Dictionary<int, string> formFactorsIdsDict;
        Dictionary<string, int> makersDict;
        Dictionary<int, string> makersIdsDict;
        private void InitializeDgvCaseInformation()
        {
            formFactorsDict = new Dictionary<string, int>();
            formFactorsIdsDict = new Dictionary<int, string>();   dgvCaseInformation.Rows.Clear();
            makersDict = new Dictionary<string, int>();   dgvCaseInformation.Columns.Clear();
            makersIdsDict = new Dictionary<int, string>();   dgvCaseInformation.Columns.Add(new DataGridViewTextBoxColumn { Name = "case_id", Visible = false });
            dgvCaseInformation.Columns.Add("case_model", "Модель");
            var formFactorCombobox = new DataGridViewComboBoxColumn() { Name = "case_form_factor", HeaderText = "Форм-фактор" };
            var makerCombobox = new DataGridViewComboBoxColumn() { Name ="maker", HeaderText = "Производитель" };
            using (var pcbs = new PcbsModel())
            {
                //Заполнение форм-фактора
                foreach (var formFact in pcbs.case_form_factor)
                {
                    formFactorsDict[formFact.form_factor] = formFact.case_form_factor_id;
                    formFactorsIdsDict[formFact.case_form_factor_id] = formFact.form_factor; 
                }
                formFactorCombobox.DataSource = formFactorsDict.Keys.ToList();
                dgvCaseInformation.Columns.Add(formFactorCombobox);
                //Заполнение производителей
                foreach (var maker in pcbs.maker)
                {
                    makersDict[maker.maker_name] = maker.maker_id;
                    makersIdsDict[maker.maker_id] = maker.maker_name;
                }
                makerCombobox.DataSource = makersDict.Keys.ToList();
                dgvCaseInformation.Columns.Add(makerCombobox);
                foreach (var caseInf in pcbs.case_information)
                {
                    var rowId = dgvCaseInformation.Rows.Add(caseInf.case_id, caseInf.case_model, formFactorsIdsDict[caseInf.case_form_factor_id], makersIdsDict[caseInf.case_maker_id]);
                    dgvCaseInformation.Rows[rowId].Tag = caseInf;
                }
            }
        }
        private void dgvCaseInformation_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = dgvCaseInformation.Rows[e.RowIndex];
            curRow.ErrorText = "";
            if (dgvCaseInformation.IsCurrentRowDirty)
            {
                //Проверка на пустоту ячейки
                var columnNames = new string[] { "case_model","case_form_factor","maker" };
                foreach (var colName in columnNames)
                {
                    if (string.IsNullOrWhiteSpace((string)curRow.Cells[colName].Value))
                    {
                        curRow.ErrorText += $"Значение в столбце '{curRow.Cells[colName].OwningColumn.HeaderText}' не может быть пустым\n";
                        e.Cancel = true;
                    }
                }
                //Проверка на повторение значений
                if (!e.Cancel)
                    using (var pcbs = new PcbsModel())
                    {
                        foreach (var elem in pcbs.case_information)
                        {
                            var curId = curRow.Cells["case_id"].Value;
                            var curModel = Formatted(curRow.Cells["case_model"].Value.ToString());
                            var curFormFactor = formFactorsDict[curRow.Cells["case_form_factor"].Value.ToString()];
                            var curMaker = makersDict[curRow.Cells["maker"].Value.ToString()];
                            if (Formatted(elem.case_model) == curModel && elem.case_form_factor_id == curFormFactor && elem.case_maker_id == curMaker &&
                                (curId == null || elem.case_id != (int)curId))
                            {
                                curRow.ErrorText += $"Строка повторяет уже существующую в таблице\n";
                                e.Cancel = true;
                            }
                        }
                    }
                if (!e.Cancel)
                {
                    var muId = (int?)curRow.Cells["case_id"].Value;
                    if (muId == null)
                    {
                        var item = new case_information { 
                            case_model = WhitespaceFormat(curRow.Cells["case_model"].Value.ToString()),
                            case_form_factor_id = formFactorsDict[curRow.Cells["case_form_factor"].Value.ToString()],
                            case_maker_id = makersDict[curRow.Cells["maker"].Value.ToString()]
                        };
                        using (var pcbs = new PcbsModel())
                        {
                            pcbs.case_information.Add(item);
                            pcbs.SaveChanges();
                            curRow.Tag = item;
                        }
                    }
                    else
                    {
                        using (var pcbs = new PcbsModel())
                        {
                            var item = (case_information)curRow.Tag;
                            pcbs.case_information.Attach(item);
                            item.case_model = WhitespaceFormat(curRow.Cells["case_model"].Value.ToString());
                            item.case_form_factor_id = formFactorsDict[curRow.Cells["case_form_factor"].Value.ToString()];
                            item.case_maker_id = makersDict[curRow.Cells["maker"].Value.ToString()];
                            pcbs.SaveChanges();
                            curRow.Tag = item;
                        }
                    }
                    foreach (DataGridViewCell cell in curRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                }
            }
        }
        private void dgvCaseInformation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           if (!dgvCaseInformation.Rows[e.RowIndex].IsNewRow)
           {
                dgvCaseInformation[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
           }
        }
        //Отмена введенных значений, если они не приняты
        private void dgvCaseInformation_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvCaseInformation.IsCurrentRowDirty)
            {
                dgvCaseInformation.CancelEdit();
                if (dgvCaseInformation.CurrentRow.Cells["case_id"].Value != null)
                {
                    dgvCaseInformation.CurrentRow.ErrorText = "";
                    var item = (case_information)dgvCaseInformation.CurrentRow.Tag;
                    dgvCaseInformation.CurrentRow.Cells["case_model"].Value = item.case_model;
                    dgvCaseInformation.CurrentRow.Cells["case_model"].ErrorText = "";
                    dgvCaseInformation.CurrentRow.Cells["case_form_factor"].Value = formFactorsIdsDict[item.case_form_factor_id];
                    dgvCaseInformation.CurrentRow.Cells["case_form_factor"].ErrorText = "";
                    dgvCaseInformation.CurrentRow.Cells["maker"].Value = makersIdsDict[item.case_maker_id];
                    dgvCaseInformation.CurrentRow.Cells["maker"].ErrorText = "";
                }
                else
                {
                    dgvCaseInformation.Rows.Remove(dgvCaseInformation.CurrentRow);
                }
            }
        }
        private void dgvCaseInformation_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var pcbs = new PcbsModel())
            {
                 var item = (case_information)e.Row.Tag;
                if (item != null)
                {
                    pcbs.case_information.Attach(item);
                    pcbs.case_information.Remove(item);
                    pcbs.SaveChanges();
                }
            }
        }
    }
}
