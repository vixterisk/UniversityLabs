
namespace GistForms
{
    partial class FormAddChange
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.Filename = new System.Windows.Forms.ColumnHeader();
            this.Content = new System.Windows.Forms.ColumnHeader();
            this.addFileButton = new System.Windows.Forms.Button();
            this.DeleteFileButton = new System.Windows.Forms.Button();
            this.patchFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.okButton);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewFile);
            this.splitContainer1.Panel2.Controls.Add(this.addFileButton);
            this.splitContainer1.Panel2.Controls.Add(this.DeleteFileButton);
            this.splitContainer1.Panel2.Controls.Add(this.patchFileButton);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 393;
            this.splitContainer1.TabIndex = 0;
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 15);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(393, 412);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okButton.Location = new System.Drawing.Point(0, 427);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(393, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Accept";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description";
            // 
            // listViewFile
            // 
            this.listViewFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename,
            this.Content});
            this.listViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFile.FullRowSelect = true;
            this.listViewFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewFile.HideSelection = false;
            this.listViewFile.Location = new System.Drawing.Point(0, 0);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(403, 381);
            this.listViewFile.TabIndex = 2;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.Details;
            // 
            // Filename
            // 
            this.Filename.Text = "Filename";
            this.Filename.Width = 100;
            // 
            // Content
            // 
            this.Content.Text = "Text";
            this.Content.Width = 200;
            // 
            // addFileButton
            // 
            this.addFileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addFileButton.Location = new System.Drawing.Point(0, 381);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(403, 23);
            this.addFileButton.TabIndex = 4;
            this.addFileButton.Text = "Add file";
            this.addFileButton.UseVisualStyleBackColor = true;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // DeleteFileButton
            // 
            this.DeleteFileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DeleteFileButton.Location = new System.Drawing.Point(0, 404);
            this.DeleteFileButton.Name = "DeleteFileButton";
            this.DeleteFileButton.Size = new System.Drawing.Size(403, 23);
            this.DeleteFileButton.TabIndex = 3;
            this.DeleteFileButton.Text = "Delete file";
            this.DeleteFileButton.UseVisualStyleBackColor = true;
            this.DeleteFileButton.Click += new System.EventHandler(this.DeleteFileButton_Click);
            // 
            // patchFileButton
            // 
            this.patchFileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.patchFileButton.Location = new System.Drawing.Point(0, 427);
            this.patchFileButton.Name = "patchFileButton";
            this.patchFileButton.Size = new System.Drawing.Size(403, 23);
            this.patchFileButton.TabIndex = 2;
            this.patchFileButton.Text = "Change file";
            this.patchFileButton.UseVisualStyleBackColor = true;
            this.patchFileButton.Click += new System.EventHandler(this.patchFileButton_Click);
            // 
            // FormAddChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormAddChange";
            this.Text = "Gist properties";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ColumnHeader Filename;
        private System.Windows.Forms.ColumnHeader Content;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.Button DeleteFileButton;
        private System.Windows.Forms.Button patchFileButton;
    }
}