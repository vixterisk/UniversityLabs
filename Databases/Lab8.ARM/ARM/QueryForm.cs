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

namespace ARM
{
    public partial class QueryForm : Form
    {
        public enum FormType
        {
            Intel,
            DDR4
        }
        public QueryForm()
        {
            InitializeComponent();
        }
        public QueryForm(FormType type)
        {
            InitializeComponent();
            queryDGV.ReadOnly = true;
            queryDGV.AllowUserToDeleteRows = false;
            queryDGV.AllowUserToAddRows = false;
            if (type == FormType.Intel) InitializeIntel();
            else InitializeDDR4();
        }

        public void InitializeIntel()
        {
            this.Text = "ПК на базе процессоров Intel";
            queryDGV.Rows.Clear();
            queryDGV.Columns.Clear();
            queryDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "pc_id", Visible = false });
            var cpuColumn = new DataGridViewComboBoxColumn { Name = "cpu_id", HeaderText = @"Процессор" };
            var hddColumn = new DataGridViewComboBoxColumn { Name = "hdd_id", HeaderText = @"Жесткий диск" };
            var caseColumn = new DataGridViewComboBoxColumn { Name = "case_id", HeaderText = @"Корпус" };
            List<int> intelCpu = new List<int>();
            using (var pc = new pcModel())
            {
                foreach (var cpu in FormTables.cpuDict)
                {
                    cpuColumn.Items.Add(cpu.Value);
                    if (cpu.Value.EndsWith("Intel")) intelCpu.Add(cpu.Key);
                }
                foreach (var hdd in FormTables.hddDict.Values)
                {
                    hddColumn.Items.Add(hdd);
                }
                foreach (var casePc in FormTables.caseDict.Values)
                {
                    caseColumn.Items.Add(casePc);
                }
            }
            queryDGV.Columns.Add(cpuColumn);
            queryDGV.Columns.Add(hddColumn);
            queryDGV.Columns.Add(caseColumn);
            using (var pc = new pcModel())
            {
                foreach (var elem in pc.pc_information)
                {
                    if (intelCpu.Contains((int)elem.cpu_id))
                    {
                        var rowId = queryDGV.Rows.Add(elem.pc_id, FormTables.cpuDict[(int)elem.cpu_id], FormTables.hddDict[(int)elem.hdd_id], FormTables.caseDict[(int)elem.case_id]);
                        queryDGV.Rows[rowId].Tag = elem;
                    }
                }
            }
        }
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
        public void InitializeDDR4()
        {
            this.Text = "Модели ОЗУ поколения DDR4";
            queryDGV.Rows.Clear();
            queryDGV.Columns.Clear();
            queryDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "ram_id", Visible = false });
            queryDGV.Columns.Add("ram_model", "Модель");
            var ddrColumn = new DataGridViewComboBoxColumn { Name = "ddr_generation", HeaderText = @"Поколение DDR" };
            using (var pc = new pcModel())
            {
                foreach (var ddr in pc.ddr_generation)
                {
                    ddrColumn.Items.Add(ddr.generation_name);
                }
            }
            queryDGV.Columns.Add(ddrColumn);
            var sizeColumn = new DataGridViewComboBoxColumn { Name = "ram_memory_size", HeaderText = @"Объем ОЗУ" };
            sizeColumn.ValueType = typeof(int);
            foreach (var e in new[] { 1, 2, 4, 8, 16, 32 }) sizeColumn.Items.Add(e);
            queryDGV.Columns.Add(sizeColumn);
            var makerColumn = new DataGridViewComboBoxColumn() { Name = "ram_maker_id", HeaderText = @"Производитель" };
            using (var pc = new pcModel())
            {
                foreach (var maker in pc.maker)
                {
                    makerColumn.Items.Add(maker.maker_name);
                }
            }
            queryDGV.Columns.Add(makerColumn);
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT ram_id, ram_model, ddr_generation, ram_memory_size, ram_maker_id
                                    FROM pcbs.ram_information"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dataDict = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "ram_id", "ram_model", "ddr_generation", "ram_memory_size", "ram_maker_id" })
                    {
                        dataDict[columnsName] = reader[columnsName];
                    }
                    if (FormTables.ddrDict[(int)reader["ddr_generation"]] == "DDR4")
                    {
                        var rowIdx = queryDGV.Rows.Add(reader["ram_id"], reader["ram_model"], FormTables.ddrDict[(int)reader["ddr_generation"]], reader["ram_memory_size"], FormTables.makersDict[(int)reader["ram_maker_id"]]);
                        queryDGV.Rows[rowIdx].Tag = dataDict;
                    }
                }
            }
        }
    }
}
