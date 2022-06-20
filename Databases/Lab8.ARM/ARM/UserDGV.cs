using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM
{
    public partial class FormTables : Form
    {
        DateTimePicker dtp = new DateTimePicker();
        Rectangle rect;
        void InitializeUsersTable()
        {
            userDGV.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.TextChanged += new EventHandler(dtp_TextChange);
            dtp.KeyDown += new KeyEventHandler(dateTimePicker1_KeyDown);
            using (var users = new userModel())
            {
                foreach(var curUser in users.user)
                {
                    var rowId =userDGV.Rows.Add(curUser.id, curUser.login, curUser.password, curUser.salt, curUser.reg_date.ToShortDateString(), curUser.is_admin);
                    userDGV.Rows[rowId].Tag = curUser;
                }
            }
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        private void userDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1)
                switch (userDGV.Columns[e.ColumnIndex].Name)
                {
                    case "reg_date":
                        if (e.RowIndex >= 0)
                        {
                            rect = userDGV.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                            var item = userDGV.Rows[e.RowIndex].Tag;
                            if (item != null) dtp.Value = ((user)item).reg_date;
                            dtp.Size = new Size(rect.Width, rect.Height);
                            dtp.Location = new Point(rect.X, rect.Y);
                            dtp.Visible = true;
                        }
                        break;
                    default:
                        dtp.Visible = false;
                        break;
                }
        }
        private void dtp_TextChange(object sender, EventArgs e)
        {
            userDGV.CurrentCell.Value = dtp.Text.ToString();
        }
        private void userDGV_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
        }
        private void userDGV_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }
        private void userDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var user = new userModel())
            {
                var item = (user)e.Row.Tag;    
                if (item != null)
                {
                    user.user.Attach(item);
                    user.user.Remove(item);
                    user.SaveChanges();
                }
            }
        }
        private void userDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                if (!userDGV.Rows[e.RowIndex].IsNewRow)
                {
                    userDGV[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                }
        }
        private bool RowValidation(DataGridViewRow curRow, int? id)
        {
            var result = true;
            foreach (var column in new string[] { "login", "password", "reg_date" })
                if (curRow.Cells[column].Value == null || string.IsNullOrWhiteSpace((string)curRow.Cells[column].Value.ToString()))
                {
                    curRow.ErrorText += $"Значение в столбце '{curRow.Cells[column].OwningColumn.HeaderText}' не может быть пустым\n";
                    result = false;
                }
            if (!result) return false;
            if (curRow.Cells["password"].Value.ToString().Length < 8)
            {
                curRow.ErrorText += $"Пароль должен быть длиннее 8 символов\n";
                result = false;
            }
            if (id == null && !NameCheck.isLoginValid(curRow.Cells["login"].Value.ToString()))
            {
                curRow.ErrorText += $"В логине допустимы символы русско-английской раскладки клавиатуры\n";
                result = false;
            }
            if (id != null && !NameCheck.LoginChangeCheck(curRow.Cells["login"].Value.ToString(), (int)id) || id == null && !NameCheck.NewLoginCheck(curRow.Cells["login"].Value.ToString()))
            {
                curRow.ErrorText += $"Нельзя повторять существующий логин. \n";
                result = false;
            }
            return result;
        }
        private void userDGV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && userDGV.IsCurrentRowDirty)
            {
                userDGV.CancelEdit();
                if (userDGV.CurrentRow.Cells["id"].Value != null)
                {
                    userDGV.CurrentRow.ErrorText = "";
                    var item = (user)userDGV.CurrentRow.Tag;
                    userDGV.CurrentRow.Cells["login"].Value = item.login;
                    userDGV.CurrentRow.Cells["password"].Value = item.password;
                    userDGV.CurrentRow.Cells["reg_date"].Value = item.reg_date;
                    userDGV.CurrentRow.Cells["salt"].Value = item.salt;
                    userDGV.CurrentRow.Cells["is_admin"].Value = item.is_admin;
                    userDGV.CurrentRow.Cells["login"].ErrorText = "";
                    userDGV.CurrentRow.Cells["password"].ErrorText = "";
                    userDGV.CurrentRow.Cells["reg_date"].ErrorText = "";
                    userDGV.CurrentRow.Cells["salt"].ErrorText = "";
                    userDGV.CurrentRow.Cells["is_admin"].ErrorText = "";
                }
                else
                {
                    userDGV.Rows.Remove(userDGV.CurrentRow);
                }
            }
        }
        private void userDGV_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var curRow = userDGV.Rows[e.RowIndex];
            var id = (int?)curRow.Cells["id"].Value;
            curRow.ErrorText = "";
            var dateHasChanged = id != null && Convert.ToDateTime(curRow.Cells["reg_date"].Value.ToString()) != ((user)curRow.Tag).reg_date;
            if (userDGV.IsCurrentRowDirty || dateHasChanged)
            {
                if (!RowValidation(curRow, id))
                {
                    e.Cancel = true;
                    return;
                }
                if (id == null)
                {
                    var curSalt = Guid.NewGuid().ToString();
                    var hash = NameCheck.GetPasswordHash(curRow.Cells["password"].Value.ToString(), curSalt);
                    var item = new user
                    {
                        login = NameCheck.WhitespaceFormat(curRow.Cells["login"].Value.ToString()),
                        password = hash,
                        reg_date = Convert.ToDateTime(curRow.Cells["reg_date"].Value.ToString()), 
                        is_admin = curRow.Cells["is_admin"].Value != null && (bool)curRow.Cells["is_admin"].Value, 
                        salt = curSalt
                    };
                    using (var user = new userModel())
                    {
                        user.user.Add(item);
                        user.SaveChanges();
                        curRow.Tag = item;
                        curRow.Cells["id"].Value = item.id;
                        curRow.Cells["salt"].Value = curSalt;
                        curRow.Cells["password"].Value = hash;
                    }
                }
                else
                {
                    using (var user = new userModel())
                    {
                        var item = (user)curRow.Tag;
                        user.user.Attach(item);
                        item.login = NameCheck.WhitespaceFormat(curRow.Cells["login"].Value.ToString());
                        item.salt = curRow.Cells["salt"].Value.ToString();
                        var hashNew = NameCheck.GetPasswordHash(curRow.Cells["password"].Value.ToString(), item.salt);
                        var hashOld = NameCheck.GetPasswordHash(item.password, item.salt);
                        if (hashNew != hashOld)
                            item.password = hashNew;
                        item.reg_date = Convert.ToDateTime(curRow.Cells["reg_date"].Value.ToString());
                        item.is_admin = curRow.Cells["is_admin"].Value != null && (bool)curRow.Cells["is_admin"].Value;
                        user.SaveChanges();
                        curRow.Tag = item;
                        curRow.Cells["password"].Value = item.password;
                    }
                }
                foreach (DataGridViewCell cell in curRow.Cells)
                {
                    cell.ErrorText = "";
                }
            }
        }
    }
}
