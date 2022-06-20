using System;
using System.Linq;
using System.Windows.Forms;
using Npgsql;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace ListViewIUD
{
    public partial class FormListView : Form
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

        public FormListView()
        {
            InitializeComponent();
            InitializeListViewUsers();
        }

        private void InitializeListViewUsers()
        {
            lvUsers.Columns.Add("Логин");
            lvUsers.Columns.Add("Пароль");
            lvUsers.Columns.Add("Соль");
            lvUsers.Columns.Add("Дата регистрации");
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"
				        select id, login, password, salt, reg_date
                        from users.""user"""
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var lvi = new ListViewItem(new[] {
                        (string)reader["login"],
                        (string)reader["password"],
                        (string)reader["salt"],
                        ((DateTime)reader["reg_date"]).ToLongDateString()
                    });
                    lvi.Tag = Tuple.Create((int)reader["id"], (DateTime)reader["reg_date"]);
                    lvUsers.Items.Add(lvi);
                }
                lvUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formInsert = new FormMuInsertUpdate(FormMuInsertUpdate.FormType.Insert, -1);
            formInsert.Salt = Guid.NewGuid().ToString();
            if (formInsert.ShowDialog() == DialogResult.OK)
            {
                if (!UserTextCheck.LoginCheck(UserTextCheck.WhitespaceFormat(formInsert.Login), -1))
                {
                    MessageBox.Show("Логин недопустим (Проверьте, что он состоит из символов русско-английской раскладки и что пользователей с таким логином в системе не зарегистрировано). Добавление неуспешно.");
                    return;
                }
                if (UserTextCheck.WhitespaceFormat(formInsert.Password).Length < 8)
                {
                    MessageBox.Show("Длина пароля меньше 8 символов. Добавление неуспешно.");
                    return;
                }
                using (var sConn = new NpgsqlConnection(_sConnStr))
                {
                    sConn.Open();
                    var sCommand = new NpgsqlCommand
                    {
                        Connection = sConn,
                        CommandText =
                            @"INSERT INTO ""users"".user(login, password, salt, Reg_date)                                        
                              VALUES(@Login, @Password, @Salt, @Reg_date)
                              RETURNING id"
                    };
                    sCommand.Parameters.AddWithValue("@Login", UserTextCheck.WhitespaceFormat(formInsert.Login));
                    var saltedPassword = UserTextCheck.WhitespaceFormat(formInsert.Password) + formInsert.Salt;
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        string hash = GetHashString(saltedPassword);
                        formInsert.Password = hash;
                    }
                    sCommand.Parameters.AddWithValue("@Password", formInsert.Password);
                    sCommand.Parameters.AddWithValue("@Salt", formInsert.Salt);
                    sCommand.Parameters.AddWithValue("@Reg_date", formInsert.Reg_date);
                    // ReSharper disable once PossibleNullReferenceException
                    var muId = (int) sCommand.ExecuteScalar();
                    var lvi = new ListViewItem(new[]
                    {
                        formInsert.Login,
                        formInsert.Password,
                        formInsert.Salt,
                        formInsert.Reg_date.ToLongDateString()
                    })
                    {
                        Tag = Tuple.Create(muId, formInsert.Reg_date)
                    };
                    lvUsers.Items.Add(lvi);
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"DELETE FROM ""users"".user WHERE id = ANY(@Ids)"
                };
                var Ids =
                    from item
                    in lvUsers.SelectedItems.Cast<ListViewItem>()
                    select ((Tuple<int, DateTime>) item.Tag).Item1;
                sCommand.Parameters.AddWithValue("@Ids", Ids.ToArray());
                sCommand.ExecuteNonQuery();
            }
            foreach (ListViewItem selectedItem in lvUsers.SelectedItems)
            {
                lvUsers.Items.Remove(selectedItem);
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in lvUsers.SelectedItems)
            {
                var selectedItemTagAsTuple = (Tuple<int, DateTime>) selectedItem.Tag;
                var id = selectedItemTagAsTuple.Item1;
                var muModifiedDate = selectedItemTagAsTuple.Item2;
                var formUpdate = new FormMuInsertUpdate(FormMuInsertUpdate.FormType.Update, id)
                {
                    Login = selectedItem.SubItems[0].Text,
                    Password = selectedItem.SubItems[1].Text,
                    Salt = selectedItem.SubItems[2].Text,
                    Reg_date = muModifiedDate
                };
                
                if (formUpdate.ShowDialog() == DialogResult.OK)
                {
                    using (var sConn = new NpgsqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new NpgsqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"UPDATE ""users"".user
                                            SET login = @Login, password = @Password, 
                                                salt = @Salt, reg_date = @Reg_date
                                            WHERE id = @id"
                        };
                        sCommand.Parameters.AddWithValue("@Login", formUpdate.Login);
                        var saltedPassword = UserTextCheck.WhitespaceFormat(formUpdate.Password) + formUpdate.Salt;
                        using (SHA256 sha256Hash = SHA256.Create())
                        {
                            string hash = GetHashString(saltedPassword);
                            formUpdate.Password = hash;
                        }
                        sCommand.Parameters.AddWithValue("@Password", formUpdate.Password);
                        sCommand.Parameters.AddWithValue("@Salt", formUpdate.Salt);
                        sCommand.Parameters.AddWithValue("@Reg_date", formUpdate.Reg_date);
                        sCommand.Parameters.AddWithValue("@id", id);
                        sCommand.ExecuteNonQuery();
                        selectedItem.SubItems[0].Text = formUpdate.Login;
                        selectedItem.SubItems[1].Text = formUpdate.Password;
                        selectedItem.SubItems[2].Text = formUpdate.Salt;
                        selectedItem.SubItems[3].Text = formUpdate.Reg_date.ToLongDateString();
                        selectedItem.Tag = Tuple.Create(id, formUpdate.Reg_date);
                    }
                }
            }
        }
    }
}
