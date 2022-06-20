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
        int?[] rowToAdd = new int?[3];

        private void InitializePcRamDGV()
        {
            pcRamDGV.Rows.Clear();
            pcRamDGV.Columns.Clear();
            pcRamDGV.Columns.Add(new DataGridViewCheckBoxColumn { Name = "isDB", Visible = false });
            var pcColumn = new DataGridViewComboBoxColumn { Name = "pc", HeaderText = @"ПК" };
            foreach (var pc in pcDict.Values)
            {
                pcColumn.Items.Add(pc);
            }
            pcColumn.Items.Add("Добавить ПК");
            pcRamDGV.Columns.Add(pcColumn);
            pcRamDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "ram_slot_number", HeaderText = @"Номер слота ОЗУ" });
            var ramColumn = new DataGridViewComboBoxColumn { Name = "ram", HeaderText = @"Модель ОЗУ" };
            foreach (var ram in ramDict.Values)
            {
                ramColumn.Items.Add(ram);
            }
            ramColumn.Items.Add("Добавить ОЗУ");
            pcRamDGV.Columns.Add(ramColumn);
            using (var pc = new pcModel())
            {
                foreach (var elem in pc.pc_ram)
                {
                    var rowId = pcRamDGV.Rows.Add(true, pcDict[elem.pc_id], elem.ram_slot_number, ramDict[(int)elem.ram_id]);
                    pcRamDGV.Rows[rowId].Tag = elem;
                }
            }
        }

        private void pcRamDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!pcRamDGV.Rows[e.RowIndex].IsNewRow)
            {
                pcRamDGV[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
            }
        }

        private void pcRamDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var curRow = pcRamDGV.Rows[e.RowIndex];
            var curCol = pcRamDGV.Columns[e.ColumnIndex];
            var cell = curRow.Cells[e.ColumnIndex];
            if (e.ColumnIndex == 1)
            {
                if (cell.Value != null && cell.Value.ToString() == "Добавить ПК")
                {
                    var newPC = (DataGridViewComboBoxColumn)curCol;
                    pc_information pc_inf = null;
                    var addPc = new AddPCForm();
                    addPc.ShowDialog();
                    if (addPc.DialogResult == DialogResult.OK)
                    {
                        pc_inf = (pc_information)addPc.Tag;
                    }
                    else return;
                    FillDictionaries();
                    InitializePcInformationDGV();
                    var str = "ЦП: " + cpuDict[(int)pc_inf.cpu_id] + ", ЖД: " + hddDict[(int)pc_inf.hdd_id] + ", Корпус: " + caseDict[(int)pc_inf.case_id];
                    pcDict[pc_inf.pc_id] = str;
                    pcIdsDict[str] = pc_inf.pc_id;
                    newPC.Items.Remove("Добавить ПК");
                    newPC.Items.Add(str);
                    newPC.Items.Add("Добавить ПК");
                    curRow.Cells["pc"].Value = pcDict[pc_inf.pc_id];
                }
            }
            if (e.ColumnIndex == 3)
            {
                if (cell.Value != null && cell.Value.ToString() == "Добавить ОЗУ")
                {
                    ram_information ram_inf = null;
                    var addRam = new AddRamForm();
                    addRam.ShowDialog();
                    if (addRam.DialogResult == DialogResult.OK)
                    {
                        ram_inf = (ram_information)addRam.Tag;
                    }
                    else return;
                    FillDictionaries();
                    InitializeDdrDGV();
                    InitializeRamInformationDGV();
                    var str = ram_inf.ram_model + ", " + ram_inf.ram_memory_size + " GB by " + makersDict[(int)ram_inf.ram_maker_id];
                    ramDict[ram_inf.ram_id] = str;
                    ramIdsDict[str] = ram_inf.ram_id;
                    var newRAM = (DataGridViewComboBoxColumn)curCol;
                    newRAM.Items.Remove("Добавить ОЗУ");
                    newRAM.Items.Add(str);
                    newRAM.Items.Add("Добавить ОЗУ");
                    curRow.Cells["ram"].Value = ramDict[ram_inf.ram_id];
                }
            }
        }

        private void pcRamDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var pcbs = new pcModel())
            {
                try
                {
                    var item = (pc_ram)e.Row.Tag;
                    if (item != null)
                    {
                        pcbs.pc_ram.Attach(item);
                        pcbs.pc_ram.Remove(item);
                        pcbs.SaveChanges();
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Данная строка используется в других таблицах. Необходимо удалить или изменить относящиеся к нему строки в этих таблицах.", "Ошибка удаления");
                    e.Cancel = true;
                }
            }
            InitializeRamInformationDGV();
        }

        private void pcRamDGV_CancelChanges()
        {
            pcRamDGV.CurrentRow.ErrorText = "";
            var item = (pc_ram)pcRamDGV.CurrentRow.Tag;
            pcRamDGV.CurrentRow.Cells["pc"].Value = pcDict[item.pc_id];
            pcRamDGV.CurrentRow.Cells["pc"].ErrorText = string.Empty;
            pcRamDGV.CurrentRow.Cells["ram"].Value = ramDict[(int)item.ram_id];
            pcRamDGV.CurrentRow.Cells["ram"].ErrorText = string.Empty;
            pcRamDGV.CurrentRow.Cells["ram_slot_number"].Value = item.ram_slot_number;
            pcRamDGV.CurrentRow.Cells["ram_slot_number"].ErrorText = string.Empty;
        }

        private void pcRamDGV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && pcRamDGV.IsCurrentRowDirty)
            {
                pcRamDGV.CancelEdit();
                if (pcRamDGV.CurrentRow.Cells["isDB"].Value != null)
                    pcRamDGV_CancelChanges();
                else
                {
                    pcRamDGV.Rows.Remove(pcRamDGV.CurrentRow);
                }
            }
        }

        private void pcRamDGV_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            pcRamDGV.EndEdit();
            var curRow = pcRamDGV.Rows[e.RowIndex];
            curRow.ErrorText = "";
            if (!pcRamDGV.IsCurrentRowDirty) return;
            //Проверка, что все значения строки введены
            var cellsWithPotentialErrors = new[] { curRow.Cells["pc"], curRow.Cells["ram_slot_number"], curRow.Cells["ram"] };
            foreach (var cell in cellsWithPotentialErrors)
            {
                if (cell.Value == null)
                {
                    curRow.ErrorText += $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым\n";
                    e.Cancel = true;
                }
            }
            if (e.Cancel) return;
            int slot_number;
            if (!Int32.TryParse(curRow.Cells["ram_slot_number"].Value.ToString(), out slot_number) || slot_number <= 0)
            {
                curRow.ErrorText += $"Значение в столбце номера слота должно быть целым положительным числом\n";
                e.Cancel = true;
                return;
            };
            using (var pc = new pcModel())
            {
                foreach (var elem in pc.pc_ram)
                {
                    if (NameCheck.Formatted(pcDict[elem.pc_id]) == NameCheck.Formatted(curRow.Cells["pc"].Value.ToString()) &&
                        elem.ram_slot_number == Int32.Parse(curRow.Cells["ram_slot_number"].Value.ToString()) &&
                        curRow.Cells["isDB"].Value == null)
                    {
                        curRow.ErrorText += $"Такая комбинация ПК и слота ОЗУ уже есть в таблице. Для изменения подключенной ОЗУ внесите изменения в уже существующую строку.\n";
                        e.Cancel = true;
                        return;
                    }
                }
            }
            if (curRow.Cells["ram"].Value.ToString() == "Добавить ОЗУ")
            {
                curRow.ErrorText += $"Необходимо выбрать существующую модель ОЗУ либо завершить добавление новой.\n";
                e.Cancel = true;
            }
            if (curRow.Cells["pc"].Value.ToString() == "Добавить ПК")
            {
                curRow.ErrorText += $"Необходимо выбрать существующий ПК либо завершить добавление нового.\n";
                e.Cancel = true;
            }
            if (e.Cancel) return;
            var isDB = curRow.Cells["isDB"].Value;
            if (isDB == null || !(bool)isDB)
            {
                var item = new pc_ram { pc_id = pcIdsDict[curRow.Cells["pc"].Value.ToString()], ram_slot_number = Int32.Parse(curRow.Cells["ram_slot_number"].Value.ToString()), ram_id = ramIdsDict[curRow.Cells["ram"].Value.ToString()] };
                using (var pcbs = new pcModel())
                {
                    pcbs.pc_ram.Add(item);
                    pcbs.SaveChanges();
                    curRow.Tag = item;
                    pcRamDGV.CurrentRow.Cells["isDB"].Value = true;
                }
            }
            else
            {
                using (var pcbs = new pcModel())
                {
                    var item = (pc_ram)curRow.Tag;
                    if (pcDict[item.pc_id] != curRow.Cells["pc"].Value.ToString() || item.ram_slot_number != Int32.Parse(curRow.Cells["ram_slot_number"].Value.ToString()))
                    {
                        curRow.ErrorText += $"Изменение комбинации \"ПК-номер слота\" невозможно. Информация модифицируется добавлением новых строк и удалением существующих.\n";
                        e.Cancel = true;
                        return;
                    }
                    pcbs.pc_ram.Attach(item);
                    item.pc_id = pcIdsDict[curRow.Cells["pc"].Value.ToString()];
                    item.ram_slot_number = Int32.Parse(curRow.Cells["ram_slot_number"].Value.ToString());
                    item.ram_id = ramIdsDict[curRow.Cells["ram"].Value.ToString()];
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
