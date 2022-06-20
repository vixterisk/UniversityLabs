using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM
{
    public partial class FormAuth : Form
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
        public FormAuth()
        {
            InitializeComponent();
            WarmUpConnection();
        }
        private void WarmUpConnection()
        {
            Task.Run(() =>
            {
                using (var sConn = new Npgsql.NpgsqlConnection(_sConnStr))
                {
                    sConn.Open();
                }
            });
        }
        private void handleButtons(bool isEnabled)
        {
            guestButton.Enabled = true;
            regButton.Enabled = isEnabled;
            authButton.Enabled = isEnabled;
        }

        private void loginTB_TextChanged(object sender, EventArgs e)
        {
            guestButton.Enabled = true;
            var login = NameCheck.WhitespaceFormat(loginTB.Text);
            if (!NameCheck.isLoginValid(login))
            {
                errMain.SetError(loginTB, "Можно использовать символы русско-английской раскладки клавиатуры, а также любые пробельные символы.");
                handleButtons(false);
            }
            else
            {
                errMain.Clear();
                if (passwordTB.Text.Length >= 8)
                    handleButtons(true);
                else
                    if (passwordTB.Text.Length != 0)
                    errMain.SetError(passwordTB, "Пароль должен содержать минимально 8 символов, отличных от пробельных.");
            }
        }

        private void passwordTB_TextChanged(object sender, EventArgs e)
        {
            guestButton.Enabled = true;
            var password = NameCheck.WhitespaceFormat(passwordTB.Text);
            if (Regex.Replace(password, @"\s+", "").Length < 8)
            {
                errMain.SetError(passwordTB, "Пароль должен содержать минимально 8 символов, отличных от пробельных.");
                handleButtons(false);
            }
            else
            {
                errMain.Clear();
                var login = NameCheck.WhitespaceFormat(loginTB.Text);
                if (NameCheck.isLoginValid(login))
                {
                    handleButtons(true);
                }
                else
                    if (login.Length != 0)
                    errMain.SetError(loginTB, "Можно использовать символы русско-английской раскладки клавиатуры, а также любые пробельные символы.");
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

        private void regButton_Click(object sender, EventArgs e)
        {
            using (var sConn = new Npgsql.NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new Npgsql.NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT count(*) FROM users.""user"" WHERE lower(login) = lower(@loginFromTb)"
                };
                sCommand.Parameters.AddWithValue("@loginFromTb", NameCheck.WhitespaceFormat(loginTB.Text));
                if ((long)sCommand.ExecuteScalar() > 0)
                    MessageBox.Show("Логин уже зарегистрирован.");
                else
                {
                    handleButtons(false);
                    var regTime = DateTime.Now;
                    var salt = Guid.NewGuid().ToString();
                    var saltedPassword = NameCheck.WhitespaceFormat(passwordTB.Text) + salt;
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        string hash = GetHashString(saltedPassword);
                        sCommand = new Npgsql.NpgsqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"INSERT INTO users.""user"" (login, password, salt, reg_date, is_admin)
                                       VALUES (@loginFromTb, @hash, @salt, @currentTime, false)"
                        };
                        sCommand.Parameters.AddWithValue("@loginFromTb", NameCheck.WhitespaceFormat(loginTB.Text));
                        sCommand.Parameters.AddWithValue("@hash", hash);
                        sCommand.Parameters.AddWithValue("@salt", salt);
                        sCommand.Parameters.AddWithValue("@currentTime", regTime);
                        sCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            using (var sConn = new Npgsql.NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new Npgsql.NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT login, password, salt, is_admin FROM users.""user"" WHERE lower(login) = lower(@loginFromTb)"
                };
                sCommand.Parameters.AddWithValue("@loginFromTb", NameCheck.WhitespaceFormat(loginTB.Text));
                using (var reader = sCommand.ExecuteReader())
                    if (reader.Read())
                    {
                        var saltedPassword = NameCheck.WhitespaceFormat(passwordTB.Text) + (string)reader["salt"];
                        if (GetHashString(saltedPassword) == (string)reader["password"])
                        {
                            handleButtons(false);
                            guestButton.Enabled = false;
                            FormTables formTables;
                            var is_admin = reader["is_admin"];
                            if ((bool)is_admin)
                                formTables = new FormTables(FormTables.FormType.adminUser);
                            else
                                formTables = new FormTables(FormTables.FormType.operatorUser);
                            formTables.ShowDialog();
                        }
                        else
                            MessageBox.Show("Неверный пароль.");
                    }
                    else
                        MessageBox.Show("Пользователя с таким логином нет.");
            }
        }

        private void guestButton_Click(object sender, EventArgs e)
        {
            handleButtons(false);
            guestButton.Enabled = false;
            var formTables = new FormTables(FormTables.FormType.guestUser);
            formTables.ShowDialog();
        }
    }
}
