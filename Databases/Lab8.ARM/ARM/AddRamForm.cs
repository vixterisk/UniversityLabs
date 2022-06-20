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
    public partial class AddRamForm : Form
    {
        public AddRamForm()
        {
            InitializeComponent();
            using (var pc = new pcModel())
            {
                foreach (var ddr in FormTables.ddrDict)
                {
                    ddrComboBox.Items.Add(ddr.Value);
                }
                ddrComboBox.Items.Add("Добавить поколение DDR");
                foreach (var i in new[] { 1, 2, 4, 8, 16, 32 }) sizeComboBox.Items.Add(i);
                foreach (var maker in FormTables.makersDict)
                {
                    makerComboBox.Items.Add(maker.Value);
                }
            }
            addRamOkButton.Enabled = false;
        }

        private void addRamOkButton_Click(object sender, EventArgs e)
        {
            var item = new ram_information
            {
                ram_model = modelTB.Text,
                ddr_generation = FormTables.ddrIdsDict[ddrComboBox.Text],
                ram_memory_size = Int32.Parse(sizeComboBox.Text),
                ram_maker_id = FormTables.makersIdsDict[makerComboBox.Text]
            };
            using (var pcM = new pcModel())
            {
                pcM.ram_information.Add(item);
                pcM.SaveChanges();
                Tag = item;
            }
        }

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(modelTB.Text) || string.IsNullOrEmpty(ddrComboBox.Text) || ddrComboBox.Text == "Добавить поколение DDR" || string.IsNullOrEmpty(sizeComboBox.Text) || string.IsNullOrEmpty(makerComboBox.Text))
                return false;
            else 
            {
                using (var pc = new pcModel())
                {
                    foreach (var elem in pc.ram_information)
                    {
                        var modelVal = NameCheck.Formatted(elem.ram_model);
                        var ddrVal = FormTables.ddrDict[(int)elem.ddr_generation];
                        var sizeVal = (int)elem.ram_memory_size;
                        var makerVal = FormTables.makersDict[(int)elem.ram_maker_id];
                        if (modelVal == modelTB.Text && ddrVal == ddrComboBox.Text && sizeVal == Int32.Parse(sizeComboBox.Text) && makerVal == makerComboBox.Text)
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
            addRamOkButton.Enabled = isInputValid();
        }

        private void modelTB_TextChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }

        private void ddrComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddrComboBox.Text == "Добавить поколение DDR")
            {
                ddr_generation ddr = null;
                var addDDR = new AddDDRForm();
                addDDR.ShowDialog();
                if (addDDR.DialogResult == DialogResult.OK)
                {
                    ddr = (ddr_generation)addDDR.Tag;
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать существующее поколение DDR либо завершить добавление нового.\n");
                    return;
                }
                FormTables.FillDictionaries();
                ddrComboBox.Items.Remove("Добавить поколение DDR");
                ddrComboBox.Items.Add(ddr.generation_name);
                ddrComboBox.Items.Add("Добавить поколение DDR");
                ddrComboBox.Text = ddr.generation_name;
            }
            HandleAddButton();
        }

        private void sizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }

        private void makerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }
    }
}
