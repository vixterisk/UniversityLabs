using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Npgsql;

namespace DataGridViewIUD
{
    public partial class FormDgv : Form
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
        Dictionary<int, string> makers = new Dictionary<int, string>();
        Dictionary<string, int> makersIds = new Dictionary<string, int>();
        Dictionary<int, string> ddrGeneration = new Dictionary<int, string>();
        Dictionary<string, int> ddrIds = new Dictionary<string, int>();

        public FormDgv()
        {
            InitializeComponent();
            FillMakersDictionary();
            FillDDRDictionary();
            InitializeDgvHdd();
            InitializeDgvRam();
        }

        private void FillDDRDictionary()
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT generation_id, generation_name from pcbs.ddr_generation"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    ddrIds.Add((string)reader["generation_name"],(int)reader["generation_id"]);
                    ddrGeneration.Add((int)reader["generation_id"], (string)reader["generation_name"]);
                }
            }
        }

        private void FillMakersDictionary()
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT maker_id, maker_name from pcbs.maker"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    makers.Add((int)reader["maker_id"], (string)reader["maker_name"]);
                    makersIds.Add((string)reader["maker_name"], (int)reader["maker_id"]);
                }
            }
        }

        private string WhitespaceFormat(string text)
        {
            text = Regex.Replace(text, @"[\s]+", " ");
            return text.Trim(new char[] { ' ' });
        }

        private string Formatted(string str)
        {
            return WhitespaceFormat(str).ToLower();
        }

        private bool isIntCellCorrect(DataGridViewCell cell)
        {
            int tmp = -1;
            return (cell.Value != null && (Int32.TryParse(cell.Value.ToString(), out tmp) && tmp >= 0));
        }

        int IntValueInCell(DataGridViewCell cell)
        {
            return Int32.Parse(cell.Value.ToString());
        }
    }
}