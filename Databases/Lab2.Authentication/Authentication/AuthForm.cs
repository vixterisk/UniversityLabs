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

namespace Authentication
{
    public partial class AuthForm : Form
    {
        private readonly Regex loginRegex = new Regex(@"^[~`""'!@№#;$%:^&?*(){}\[\]\-_=+<>,./\|ёа-яa-z0-9\s]+$", RegexOptions.IgnoreCase);
        private readonly string sConnStr = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = 5432,
            Username = "postgres",
            Password = "1234",
            Database = "postgres"
        }.ConnectionString;

        public AuthForm()
        {
            InitializeComponent();
            WarmUpConnection();
        }

        private void WarmUpConnection()
        {
            Task.Run(() =>
            {
                using (var sConn = new Npgsql.NpgsqlConnection(sConnStr))
                {
                    sConn.Open();
                }
            });
        }

        private string WhitespaceFormat(TextBox tb)
        {
            var loginText = tb.Text;
            loginText = Regex.Replace(loginText, @"[\s]+", " ");
            return loginText.Trim(new char[] { ' ' });
        }

        private bool isLoginValid(string login)
        {
            return loginRegex.IsMatch(login);
        }

        private void handleButtons(bool isEnabled)
        {
            regButton.Enabled = isEnabled;
            authButton.Enabled = isEnabled;
        }

        private void loginTB_TextChanged(object sender, EventArgs e)
        {
            var login = WhitespaceFormat(loginTB);
            if (!isLoginValid(login))
            {
                errMain.SetError(loginTB, "Можно использовать символы русско-английской раскладки клавиатуры, а также любые пробельные символы.");
                handleButtons(false);
            }
            else
            {
                if (passwordTB.Text.Length >= 8)
                    handleButtons(true);
                errMain.Clear();
            }
        }

        private void passwordTB_TextChanged(object sender, EventArgs e)
        {
            var password = WhitespaceFormat(passwordTB);
            if (Regex.Replace(password, @"\s+", "").Length < 8)
            {
                errMain.SetError(passwordTB, "Пароль должен содержать минимально 8 символов, отличных от пробельных.");
                handleButtons(false);
            }
            else
            {
                var login = WhitespaceFormat(loginTB);
                if (isLoginValid(login))
                    handleButtons(true);
                errMain.Clear();
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
            using (var sConn = new Npgsql.NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                var sCommand = new Npgsql.NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT count(*) FROM users.""user"" WHERE lower(login) = lower(@loginFromTb)"
                };
                sCommand.Parameters.AddWithValue("@loginFromTb", WhitespaceFormat(loginTB));
                if ((long)sCommand.ExecuteScalar() > 0)
                    errMain.SetError(loginTB, "Логин уже зарегистрирован");
                else
                {
                    handleButtons(false);
                    var regTime = DateTime.Now;
                    var salt = Guid.NewGuid().ToString();
                    var saltedPassword = WhitespaceFormat(passwordTB) + salt;
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        string hash = GetHashString(saltedPassword);
                        sCommand = new Npgsql.NpgsqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"INSERT INTO users.""user"" (login, password, salt, reg_date)
                                       VALUES (@loginFromTb, @hash, @salt, @currentTime)"
                        };
                        sCommand.Parameters.AddWithValue("@loginFromTb", WhitespaceFormat(loginTB));
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
            using (var sConn = new Npgsql.NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                var sCommand = new Npgsql.NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT login, password, salt FROM users.""user"" WHERE lower(login) = lower(@loginFromTb)"
                };
                sCommand.Parameters.AddWithValue("@loginFromTb", WhitespaceFormat(loginTB));
                using (var reader = sCommand.ExecuteReader())
                if (reader.Read())
                {
                        var saltedPassword = WhitespaceFormat(passwordTB) + (string)reader["salt"];
                        if (GetHashString(saltedPassword) == (string)reader["password"])
                            MessageBox.Show("Аутентификация успешна!");
                        else
                            MessageBox.Show("Неверный пароль.");
                }
                else
                    MessageBox.Show("Пользователя с таким логином нет.");
            }
        }
    }
}
