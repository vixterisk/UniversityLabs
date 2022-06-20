
namespace FormsTrie
{
    partial class FormMain
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
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GenerateNButton = new System.Windows.Forms.Button();
            this.NTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.RemoveAllButton = new System.Windows.Forms.Button();
            this.FindButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(12, 35);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(182, 23);
            this.AddButton.TabIndex = 0;
            this.AddButton.Text = "Add to a map";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(400, 35);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(200, 23);
            this.RemoveButton.TabIndex = 1;
            this.RemoveButton.Text = "Delete from a map";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Location = new System.Drawing.Point(56, 6);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(226, 23);
            this.KeyTextBox.TabIndex = 2;
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(354, 6);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(246, 23);
            this.ValueTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Value";
            // 
            // GenerateNButton
            // 
            this.GenerateNButton.Location = new System.Drawing.Point(606, 6);
            this.GenerateNButton.Name = "GenerateNButton";
            this.GenerateNButton.Size = new System.Drawing.Size(222, 23);
            this.GenerateNButton.TabIndex = 7;
            this.GenerateNButton.Text = "Generate N Pairs <Key, Value>";
            this.GenerateNButton.UseVisualStyleBackColor = true;
            this.GenerateNButton.Click += new System.EventHandler(this.GenerateNButton_Click);
            // 
            // NTextBox
            // 
            this.NTextBox.Location = new System.Drawing.Point(867, 6);
            this.NTextBox.Name = "NTextBox";
            this.NTextBox.Size = new System.Drawing.Size(90, 23);
            this.NTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(834, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "N =";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(12, 68);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(945, 484);
            this.listBox.TabIndex = 10;
            // 
            // RemoveAllButton
            // 
            this.RemoveAllButton.Location = new System.Drawing.Point(606, 35);
            this.RemoveAllButton.Name = "RemoveAllButton";
            this.RemoveAllButton.Size = new System.Drawing.Size(351, 23);
            this.RemoveAllButton.TabIndex = 11;
            this.RemoveAllButton.Text = "Delete all pairs";
            this.RemoveAllButton.UseVisualStyleBackColor = true;
            this.RemoveAllButton.Click += new System.EventHandler(this.RemoveAllButton_Click);
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(200, 35);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(194, 23);
            this.FindButton.TabIndex = 12;
            this.FindButton.Text = "Find an element";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 567);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.RemoveAllButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NTextBox);
            this.Controls.Add(this.GenerateNButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.listBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.Text = "Associative arrays";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GenerateNButton;
        private System.Windows.Forms.TextBox NTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button RemoveAllButton;
        private System.Windows.Forms.Button FindButton;
    }
}

