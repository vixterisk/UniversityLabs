using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransactAppButNotReally
{
    public partial class MainForm : Form
    {
        private readonly string _sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = 5432,
            Database = "postgres",
            Username = "postgres",
            Password = "1234",
            MaxAutoPrepare = 10,
            AutoPrepareMinUsages = 2
        }.ConnectionString;
        public MainForm()
        {
            InitializeComponent();
            InitializeLvVideocard();
        }

        private void InitializeLvVideocard()
        {
            lvRam.Clear();
            lvRam.Columns.Add("id");
            lvRam.Columns.Add("Model");
            lvRam.Columns.Add("Memory");
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"
				        select ram_id, ram_model, ram_memory_size
                        from pcbs.ram_information;"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var lvi = new ListViewItem(new[] {
                        reader["ram_id"].ToString(),
                        reader["ram_model"].ToString(),
                        reader["ram_memory_size"].ToString()
                    });
                    lvRam.Items.Add(lvi);
                }
                lvRam.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvRam.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            InitializeLvVideocard();
        }

        private void btUpdate_Click_1(object sender, EventArgs e)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"Update pcbs.ram_information set ram_memory_size = ram_memory_size * 2;"
                };
                sCommand.ExecuteNonQuery();
                InitializeLvVideocard();
            }
        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var guid = Guid.NewGuid();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"Insert into pcbs.ram_information(ram_model,ram_memory_size) values (@model,2);"
                };
                sCommand.Parameters.AddWithValue("@model", guid);
                sCommand.ExecuteNonQuery();
                InitializeLvVideocard();
            }
        }
    }
}
