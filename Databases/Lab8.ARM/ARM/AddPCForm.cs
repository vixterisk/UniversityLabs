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
    public partial class AddPCForm : Form
    {
        public AddPCForm()
        {
            InitializeComponent();
            using (var pc = new pcModel())
            {
                foreach (var cpu in FormTables.cpuDict)
                {
                    cpuComboBox.Items.Add(cpu.Value);
                }
                foreach (var hdd in FormTables.hddDict.Values)
                {
                    hddComboBox.Items.Add(hdd);
                }
                foreach (var casePc in FormTables.caseDict.Values)
                {
                    caseComboBox.Items.Add(casePc);
                }
            }
            addPcOkButton.Enabled = false;
        }

        private void addPcOkButton_Click(object sender, EventArgs e)
        {
            var item = new pc_information
            {
                case_id = FormTables.caseIdsDict[caseComboBox.Text],
                cpu_id = FormTables.cpuIdsDict[cpuComboBox.Text],
                hdd_id = FormTables.hddIdsDict[hddComboBox.Text]
            };
            using (var pcM = new pcModel())
            {
                pcM.pc_information.Add(item);
                pcM.SaveChanges();
                Tag = item;
            }
        }

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(caseComboBox.Text) || string.IsNullOrEmpty(cpuComboBox.Text) || string.IsNullOrEmpty(hddComboBox.Text))
                return false;
            else
            {
                using (var pc = new pcModel())
                {
                    foreach (var elem in pc.pc_information)
                    {
                        var caseVal = FormTables.caseDict[(int)elem.case_id];
                        var cpuVal = FormTables.cpuDict[(int)elem.cpu_id];
                        var hddVal = FormTables.hddDict[(int)elem.hdd_id];
                        if (caseVal == caseComboBox.Text && cpuVal == cpuComboBox.Text && hddVal == hddComboBox.Text)
                        {
                            MessageBox.Show("Строка повторяет уже существующую в таблице.\n");
                            return false;
                        }
                    }
                }
                return true;
            }

        }

        private void HandleAddButton()
        {
            if (isInputValid()) addPcOkButton.Enabled = true;
        }

        private void caseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }

        private void cpuComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }

        private void hddComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }
    }
}
