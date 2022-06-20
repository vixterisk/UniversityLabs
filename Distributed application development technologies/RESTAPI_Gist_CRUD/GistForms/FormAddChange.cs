using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GistForms
{
    public partial class FormAddChange : Form
    {
        public Gist Gist { get; set; }
        public Gist GistPatched { get; set; }
        public FormAddChange()
        {
            InitializeComponent();
            okButton.Enabled = false;
            Gist = new Gist();
            GistPatched = new Gist();
        }

        public FormAddChange(Gist gist)
        {
            InitializeComponent();
            okButton.Enabled = false;
            Gist = gist;
            GistPatched = gist;
            richTextBox.Text = Gist.description;
            Refill_ListViewAsync();
        }
        private void Refill_ListViewAsync()
        {
            listViewFile.Items.Clear();
            var listViewList = new List<ListViewItem>();
            foreach (var file in GistPatched.files)
            {
                if (file.content != "")
                {
                    ListViewItem item = new ListViewItem(file.filename);
                    item.SubItems.Add(file.content);
                    listViewList.Add(item);
                }
            }
            listViewFile.Items.AddRange(listViewList.ToArray());
        }
        private int GetIndex()
        {
            var index = listViewFile.SelectedIndices[0];
            int i = 0;
            while (listViewFile.Items[index].Text != GistPatched.files[i].filename)
                i++;
            return i;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            GistPatched.description = richTextBox.Text;
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            var form = new FormAddChangeFile(GistPatched.files);
            if (form.ShowDialog() == DialogResult.OK)
            {
                GistPatched.files.Add(new GistFile() { filename = form.Filename, content = form.Content });
                Refill_ListViewAsync();
                okButton.Enabled = true;
            }
            form.Close();
        }

        private void DeleteFileButton_Click(object sender, EventArgs e)
        {
            if (listViewFile.Items.Count == 1)
            { 
                MessageBox.Show("Гист должен содержать хотя бы один файл. ");
                return;
            }
            if (listViewFile.SelectedIndices.Count > 0)
            {
                GistPatched.files[GetIndex()].content = "";
                Refill_ListViewAsync();
                okButton.Enabled = true;
            }
        }

        private void patchFileButton_Click(object sender, EventArgs e)
        {
            if (listViewFile.SelectedIndices.Count > 0)
            {
                var index = GetIndex();
                var form = new FormAddChangeFile(GistPatched.files[index], GistPatched.files);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    GistPatched.files[index].content = "";
                    var newGistFile = new GistFile();
                    newGistFile.filename = form.Filename;
                    newGistFile.content = form.Content;
                    GistPatched.files.Add(newGistFile);
                    okButton.Enabled = true;
                }
                form.Close();
                Refill_ListViewAsync();
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GistPatched.files.Count > 0)
                okButton.Enabled = true;
        }
    }
}
