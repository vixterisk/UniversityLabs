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
        private void InitializeDdrDGV()
        {
            ddrDGV.Rows.Clear();
            ddrDGV.Columns.Clear();
            ddrDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "generation_id", Visible = false });
            ddrDGV.Columns.Add("generation_name", "Поколение DDR");
            using (var pc = new pcModel())
            {
                foreach (var elem in pc.ddr_generation)
                {
                    var rowId = ddrDGV.Rows.Add(elem.generation_id, elem.generation_name);
                    ddrDGV.Rows[rowId].Tag = elem;
                }
            }
        }
        private void ddrDGV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && ddrDGV.IsCurrentRowDirty)
            {
                ddrDGV.CancelEdit();
                if (ddrDGV.CurrentRow.Cells["generation_id"].Value != null)
                {
                    ddrDGV.CurrentRow.ErrorText = "";
                    var item = (ddr_generation)ddrDGV.CurrentRow.Tag;
                    ddrDGV.CurrentRow.Cells["generation_name"].Value = item.generation_name;
                    ddrDGV.CurrentRow.Cells["generation_name"].ErrorText = string.Empty;
                }
                else
                {
                    ddrDGV.Rows.Remove(ddrDGV.CurrentRow);
                }
            }
        }
        private void ddrDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!ddrDGV.Rows[e.RowIndex].IsNewRow)
            {
                ddrDGV[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
            }
        }
        private void ddrDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var pcbs = new pcModel())
            {
                try
                {
                    var item = (ddr_generation)e.Row.Tag;
                    if (item != null)
                    {
                        pcbs.ddr_generation.Attach(item);
                        pcbs.ddr_generation.Remove(item);
                        pcbs.SaveChanges();
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Данное поколение DDR используется в других таблицах. Необходимо удалить или изменить относящиеся к нему строки в этих таблицах.", "Ошибка удаления");
                    e.Cancel = true;
                }
            }
            FillDictionaries();
            InitializeRamInformationDGV();
        }
        private void ddrDGV_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = ddrDGV.Rows[e.RowIndex];
            curRow.ErrorText = "";
            if (!ddrDGV.IsCurrentRowDirty) return;
            if (string.IsNullOrWhiteSpace((string)curRow.Cells["generation_name"].Value))
            {
                curRow.ErrorText += $"Значение в столбце '{curRow.Cells["generation_name"].OwningColumn.HeaderText}' не может быть пустым\n";
                e.Cancel = true;
                return;
            }
            using (var pc = new pcModel())
            {
                foreach (var elem in pc.ddr_generation)
                {
                    if (NameCheck.Formatted(elem.generation_name) == NameCheck.Formatted(curRow.Cells["generation_name"].Value.ToString()) &&
                        (curRow.Cells["generation_id"].Value == null || elem.generation_id != (int)curRow.Cells["generation_id"].Value))
                    {
                        curRow.ErrorText += $"Строка повторяет уже существующую в таблице\n";
                        e.Cancel = true;
                        return;
                    }
                }
            }
            var muId = (int?)curRow.Cells["generation_id"].Value;
            if (muId == null)
            {
                var item = new ddr_generation { generation_name = NameCheck.WhitespaceFormat(curRow.Cells["generation_name"].Value.ToString()) };
                using (var pcbs = new pcModel())
                {
                    pcbs.ddr_generation.Add(item);
                    pcbs.SaveChanges();
                    curRow.Tag = item;
                    ddrDGV.CurrentRow.Cells["generation_id"].Value = item.generation_id;
                }
            }
            else
            {
                using (var pcbs = new pcModel())
                {
                    var item = (ddr_generation)curRow.Tag;
                    pcbs.ddr_generation.Attach(item);
                    item.generation_name = NameCheck.WhitespaceFormat(curRow.Cells["generation_name"].Value.ToString());
                    pcbs.SaveChanges();
                    curRow.Tag = item;
                }
            }
            foreach (DataGridViewCell cell in curRow.Cells)
            {
                cell.ErrorText = "";
            }
            FillDictionaries();
            InitializeRamInformationDGV();
        }
    }
}
