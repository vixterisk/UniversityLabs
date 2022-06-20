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
    public partial class FormAddChangeFile : Form
    {
        public string Filename = "";
        public string Content = "";
        List<GistFile> files;
        string currentFileName = "";
        private void Initialize(List<GistFile> files)
        {
            InitializeComponent();
            okFileButton.Enabled = false;
            this.files = files;
        }
        public FormAddChangeFile(List<GistFile> files)
        {
            Initialize(files);
        }
        public FormAddChangeFile(GistFile file, List<GistFile> files)
        {
            Initialize(files);
            filenameTextBox.Text = currentFileName = Filename = file.filename;
            FileRichTextBox.Text = Content = file.content;
        }
        private void CheckIfChangesAcceptable()
        {
            bool nameNotUsed = true;
            foreach (var file in files)
            {
                if (file.filename == Filename && currentFileName != Filename)
                    nameNotUsed = false;
            }
            okFileButton.Enabled = Content != "" && Filename != "" && nameNotUsed;
        }
        private void filenameTextBox_TextChanged(object sender, EventArgs e)
        {
            Filename = filenameTextBox.Text;
            CheckIfChangesAcceptable();
        }

        private void FileRichTextBox_TextChanged(object sender, EventArgs e)
        {
            Content = FileRichTextBox.Text;
            CheckIfChangesAcceptable();
        }

        private void okFileButton_Click(object sender, EventArgs e)
        {

        }
    }
}
