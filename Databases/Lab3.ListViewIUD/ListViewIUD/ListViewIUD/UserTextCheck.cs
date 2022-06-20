using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ListViewIUD
{
    class UserTextCheck
    {
        public static readonly Regex loginRegex = new Regex(@"^[~`""'!@№#;$%:^&?*(){}\[\]\-_=+<>,./\|ёа-яa-z0-9\s]+$", RegexOptions.IgnoreCase);
        private static readonly string _sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = Database.Default.Host,
            Port = Database.Default.Port,
            Database = Database.Default.Name,
            Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
            Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD"),
            MaxAutoPrepare = 10,
            AutoPrepareMinUsages = 2
        }.ConnectionString;

        public static string WhitespaceFormat(string text)
        {
            text = Regex.Replace(text, @"[\s]+", " ");
            return text.Trim(new char[] { ' ' });
        }

        public static bool LoginCheck(string login, int id)
        {
                using (var sConn = new Npgsql.NpgsqlConnection(_sConnStr))
                {
                    sConn.Open();
                    var sCommand = new Npgsql.NpgsqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"SELECT count(*) FROM users.""user"" WHERE lower(login) = lower(@loginFromTb) and id <> @id"
                    };
                    sCommand.Parameters.AddWithValue("@loginFromTb", login);
                    sCommand.Parameters.AddWithValue("@id", id);
                    return ((long)sCommand.ExecuteScalar() <= 0) && loginRegex.IsMatch(login);
                }
        }
    }
}
