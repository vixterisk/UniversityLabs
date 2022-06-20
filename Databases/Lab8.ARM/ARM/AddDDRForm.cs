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
    public partial class AddDDRForm : Form
    {
        public AddDDRForm()
        {
            InitializeComponent();
            AddDDRButton.Enabled = false;
        }

        private void AddDDRButton_Click(object sender, EventArgs e)
        {
            var item = new ddr_generation
            {
                generation_name = ddrTB.Text
            };
            using (var pcM = new pcModel())
            {
                pcM.ddr_generation.Add(item);
                pcM.SaveChanges();
                Tag = item;
            }
        }

        private void ddrTB_TextChanged(object sender, EventArgs e)
        {
            HandleAddButton();
        }

        private void HandleAddButton()
        {
            AddDDRButton.Enabled = isInputValid();
        }

        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(ddrTB.Text))
                return false;
            else
            {
                using (var pc = new pcModel())
                {
                    foreach (var elem in pc.ddr_generation)
                    {
                        if (elem.generation_name == ddrTB.Text)
                        {
                            MessageBox.Show("Строка повторяет уже существующую в таблице.\n");
                            return false;
                        }
                    }
                }
                return true;
            }

        }
    }
}
