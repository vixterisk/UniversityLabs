
namespace GistForms
{
    partial class FormAuth
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GistLV = new System.Windows.Forms.ListView();
            this.id = new System.Windows.Forms.ColumnHeader();
            this.Desc = new System.Windows.Forms.ColumnHeader();
            this.logoutButton = new System.Windows.Forms.Button();
            this.patchButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginButton
            // 
            this.loginButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.loginButton.Location = new System.Drawing.Point(0, 0);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(198, 29);
            this.loginButton.TabIndex = 0;
            this.loginButton.Text = "Log in";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.GistLV);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.logoutButton);
            this.splitContainer1.Panel2.Controls.Add(this.patchButton);
            this.splitContainer1.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer1.Panel2.Controls.Add(this.addButton);
            this.splitContainer1.Panel2.Controls.Add(this.loginButton);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Size = new System.Drawing.Size(810, 489);
            this.splitContainer1.SplitterDistance = 608;
            this.splitContainer1.TabIndex = 1;
            // 
            // GistLV
            // 
            this.GistLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.Desc});
            this.GistLV.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.GistLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GistLV.FullRowSelect = true;
            this.GistLV.GridLines = true;
            this.GistLV.HideSelection = false;
            this.GistLV.Location = new System.Drawing.Point(0, 0);
            this.GistLV.MultiSelect = false;
            this.GistLV.Name = "GistLV";
            this.GistLV.Size = new System.Drawing.Size(608, 489);
            this.GistLV.TabIndex = 0;
            this.GistLV.UseCompatibleStateImageBehavior = false;
            this.GistLV.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 200;
            // 
            // Desc
            // 
            this.Desc.Text = "Description";
            this.Desc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Desc.Width = 400;
            // 
            // logoutButton
            // 
            this.logoutButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logoutButton.Location = new System.Drawing.Point(0, 466);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(198, 23);
            this.logoutButton.TabIndex = 3;
            this.logoutButton.Text = "Log out";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // patchButton
            // 
            this.patchButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.patchButton.Location = new System.Drawing.Point(0, 75);
            this.patchButton.Name = "patchButton";
            this.patchButton.Size = new System.Drawing.Size(198, 23);
            this.patchButton.TabIndex = 2;
            this.patchButton.Text = "Change gist";
            this.patchButton.UseVisualStyleBackColor = true;
            this.patchButton.Click += new System.EventHandler(this.PatchButton_ClickAsync);
            // 
            // deleteButton
            // 
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.deleteButton.Location = new System.Drawing.Point(0, 52);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(198, 23);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Delete gist";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.addButton.Location = new System.Drawing.Point(0, 29);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(198, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add gist";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_ClickAsync);
            // 
            // FormAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 489);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormAuth";
            this.Text = "My Gists app";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView GistLV;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button patchButton;
        private System.Windows.Forms.ColumnHeader Desc;
        public System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.Button logoutButton;
    }
}

