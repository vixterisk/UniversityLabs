using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsTrie
{
    public partial class FormMain : Form
    {
        Trie<String> Trie;
        public FormMain()
        {
            InitializeComponent();
            Trie = new Trie<string>();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Trie.Insert(KeyTextBox.Text, ValueTextBox.Text);
            FillListBox();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            Trie.Remove(KeyTextBox.Text);
            FillListBox();
        }
        private void GenerateNButton_Click(object sender, EventArgs e)
        {
            int number;
            if (!int.TryParse(NTextBox.Text, out number))
            {
                MessageBox.Show("Incorrect N!");
                return;
            }
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var temp = new Dictionary<string, string>();
            for (int i = 0; i < number; i++)
            {
                var word = new string(Enumerable.Repeat(chars, random.Next(1, 20))
                   .Select(s => s[random.Next(s.Length)]).ToArray());
                var word2 = new string(Enumerable.Repeat(chars, random.Next(1, 20))
                   .Select(s => s[random.Next(s.Length)]).ToArray());
                Trie.Insert(word, word2);
            }
            FillListBox();
        }

        private void FillListBox()
        {
            listBox.Items.Clear();
            listBox.Items.AddRange(Trie.Each().OfType<object>().Select(o => o.ToString()).ToArray());
        }

        private void RemoveAllButton_Click(object sender, EventArgs e)
        {
            Trie = new Trie<string>();
            listBox.Items.Clear();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            string value;
            if (!Trie.TryGetValue(KeyTextBox.Text, out value))
                MessageBox.Show("Not found");
            else MessageBox.Show("Value: \"" + value + "\"");
        }
    }
}
