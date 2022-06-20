
namespace GistForms
{
    partial class FormAddChangeFile
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
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FileRichTextBox = new System.Windows.Forms.RichTextBox();
            this.okFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.filenameTextBox.Location = new System.Drawing.Point(0, 15);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(800, 23);
            this.filenameTextBox.TabIndex = 0;
            this.filenameTextBox.TextChanged += new System.EventHandler(this.filenameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filename (Unique within the gist)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Text (Cannot be empty)";
            // 
            // FileRichTextBox
            // 
            this.FileRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileRichTextBox.Location = new System.Drawing.Point(0, 53);
            this.FileRichTextBox.Name = "FileRichTextBox";
            this.FileRichTextBox.Size = new System.Drawing.Size(800, 374);
            this.FileRichTextBox.TabIndex = 3;
            this.FileRichTextBox.Text = "";
            this.FileRichTextBox.TextChanged += new System.EventHandler(this.FileRichTextBox_TextChanged);
            // 
            // okFileButton
            // 
            this.okFileButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okFileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okFileButton.Location = new System.Drawing.Point(0, 427);
            this.okFileButton.Name = "okFileButton";
            this.okFileButton.Size = new System.Drawing.Size(800, 23);
            this.okFileButton.TabIndex = 4;
            this.okFileButton.Text = "Save";
            this.okFileButton.UseVisualStyleBackColor = true;
            this.okFileButton.Click += new System.EventHandler(this.okFileButton_Click);
            // 
            // FormAddChangeFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FileRichTextBox);
            this.Controls.Add(this.okFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filenameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "FormAddChangeFile";
            this.Text = "Gist Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox FileRichTextBox;
        private System.Windows.Forms.Button okFileButton;
    }
}