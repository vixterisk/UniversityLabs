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
        private void InitializePcInformationDGV()
        {
            pcInformationDGV.Rows.Clear();
            pcInformationDGV.Columns.Clear();
            pcInformationDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "pc_id", Visible = false });
            var cpuColumn = new DataGridViewComboBoxColumn { Name = "cpu_id", HeaderText = @"Процессор" };
            var hddColumn = new DataGridViewComboBoxColumn { Name = "hdd_id", HeaderText = @"Жесткий диск" };
            var caseColumn = new DataGridViewComboBoxColumn { Name = "case_id", HeaderText = @"Корпус" };
            using (var pc = new pcModel())
            {
                foreach (var cpu in cpuDict.Values)
                {
                    cpuColumn.Items.Add(cpu);
                }
                foreach (var hdd in hddDict.Values)
                {
                    hddColumn.Items.Add(hdd);
                }
                foreach (var casePc in caseDict.Values)
                {
                    caseColumn.Items.Add(casePc);
                }
            }
            pcInformationDGV.Columns.Add(cpuColumn);
            pcInformationDGV.Columns.Add(hddColumn);
            pcInformationDGV.Columns.Add(caseColumn);
            using (var pc = new pcModel())
            {
                foreach (var elem in pc.pc_information)
                {
                    var rowId = pcInformationDGV.Rows.Add(elem.pc_id, cpuDict[(int)elem.cpu_id], hddDict[(int)elem.hdd_id], caseDict[(int)elem.case_id]);
                    pcInformationDGV.Rows[rowId].Tag = elem;
                }
            }
        }
        private void pcInformationDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!pcInformationDGV.Rows[e.RowIndex].IsNewRow)
            {
                pcInformationDGV[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
            }
        }

        private void pcInformationDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var pcbs = new pcModel())
            {
                try
                {
                    var item = (pc_information)e.Row.Tag;
                    if (item != null)
                    {
                        pcbs.pc_information.Attach(item);
                        pcbs.pc_information.Remove(item);
                        pcbs.SaveChanges();
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Данный ПК используется в других таблицах. Необходимо удалить или изменить относящиеся к нему строки в этих таблицах.", "Ошибка удаления");
                    e.Cancel = true;
                }
            }
            FillDictionaries();
            InitializePcRamDGV();
        }

        private void pcInformationDGV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && pcInformationDGV.IsCurrentRowDirty)
            {
                pcInformationDGV.CancelEdit();
                if (pcInformationDGV.CurrentRow.Cells["pc_id"].Value != null)
                {
                    pcInformationDGV.CurrentRow.ErrorText = "";
                    var item = (pc_information)pcInformationDGV.CurrentRow.Tag;
                    pcInformationDGV.CurrentRow.Cells["cpu_id"].Value = cpuDict[(int)item.cpu_id];
                    pcInformationDGV.CurrentRow.Cells["hdd_id"].Value = hddDict[(int)item.hdd_id];
                    pcInformationDGV.CurrentRow.Cells["case_id"].Value = caseDict[(int)item.case_id];
                    pcInformationDGV.CurrentRow.Cells["cpu_id"].ErrorText = "";
                    pcInformationDGV.CurrentRow.Cells["hdd_id"].ErrorText = "";
                    pcInformationDGV.CurrentRow.Cells["case_id"].ErrorText = "";
                }
                else
                {
                    pcInformationDGV.Rows.Remove(pcInformationDGV.CurrentRow);
                }
            }
        }

        private void pcInformationDGV_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = pcInformationDGV.Rows[e.RowIndex];
            if (pcInformationDGV.IsCurrentRowDirty)
            {
                curRow.ErrorText = "";
                //Проверка, что все значения строки введены
                var cellsWithPotentialErrors = new[] { curRow.Cells["cpu_id"], curRow.Cells["hdd_id"], curRow.Cells["case_id"] };
                foreach (var cell in cellsWithPotentialErrors)
                {
                    if (cell.Value == null)
                    {
                        curRow.ErrorText += $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым\n";
                        e.Cancel = true;
                    }
                }
                if (e.Cancel) return;
                //Проверка, что мы не создаем дубликат существующей строки
                using (var pc = new pcModel())
                {
                    foreach (var elem in pc.pc_information)
                    {
                        var caseVal = caseDict[(int)elem.case_id];
                        var cpuVal = cpuDict[(int)elem.cpu_id];
                        var hddVal = hddDict[(int)elem.hdd_id];
                        if (caseVal == curRow.Cells["case_id"].Value.ToString() && 
                            cpuVal == curRow.Cells["cpu_id"].Value.ToString() && 
                            hddVal == curRow.Cells["hdd_id"].Value.ToString()
                            && (curRow.Tag == null || ((pc_information)curRow.Tag).pc_id != elem.pc_id))
                        {
                            curRow.ErrorText += $"Строка повторяет уже существующую в таблице.\n";
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                var id = (int?)curRow.Cells["pc_id"].Value;
                if (id == null)
                {
                    var item = new pc_information
                    {
                        case_id = caseIdsDict[curRow.Cells["case_id"].Value.ToString()],
                        cpu_id = cpuIdsDict[curRow.Cells["cpu_id"].Value.ToString()],
                        hdd_id = hddIdsDict[curRow.Cells["hdd_id"].Value.ToString()]
                    };
                    using (var pcM = new pcModel())
                    {
                        pcM.pc_information.Add(item);
                        pcM.SaveChanges();
                        curRow.Tag = item;
                        curRow.Cells["pc_id"].Value = item.pc_id;
                    }
                }
                else
                {
                    using (var pcM = new pcModel())
                    {
                        var item = (pc_information)curRow.Tag;
                        pcM.pc_information.Attach(item);
                        item.case_id = caseIdsDict[curRow.Cells["case_id"].Value.ToString()];
                        item.cpu_id = cpuIdsDict[curRow.Cells["cpu_id"].Value.ToString()];
                        item.hdd_id = hddIdsDict[curRow.Cells["hdd_id"].Value.ToString()];
                        pcM.SaveChanges();
                        curRow.Tag = item;
                    }
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
