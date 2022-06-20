namespace ARM
{
    partial class AddRamForm
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
            this.modelLabel = new System.Windows.Forms.Label();
            this.modelTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ddrComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sizeComboBox = new System.Windows.Forms.ComboBox();
            this.makerLabel = new System.Windows.Forms.Label();
            this.makerComboBox = new System.Windows.Forms.ComboBox();
            this.addRamOkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modelLabel
            // 
            this.modelLabel.AutoSize = true;
            this.modelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.modelLabel.Location = new System.Drawing.Point(30, 15);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(70, 20);
            this.modelLabel.TabIndex = 0;
            this.modelLabel.Text = "Модель";
            // 
            // modelTB
            // 
            this.modelTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.modelTB.Location = new System.Drawing.Point(30, 35);
            this.modelTB.Name = "modelTB";
            this.modelTB.Size = new System.Drawing.Size(426, 26);
            this.modelTB.TabIndex = 2;
            this.modelTB.TextChanged += new System.EventHandler(this.modelTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(30, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Поколение DDR";
            // 
            // ddrComboBox
            // 
            this.ddrComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ddrComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddrComboBox.FormattingEnabled = true;
            this.ddrComboBox.Location = new System.Drawing.Point(30, 81);
            this.ddrComboBox.Name = "ddrComboBox";
            this.ddrComboBox.Size = new System.Drawing.Size(426, 28);
            this.ddrComboBox.TabIndex = 4;
            this.ddrComboBox.SelectedIndexChanged += new System.EventHandler(this.ddrComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(30, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Объем RAM";
            // 
            // sizeComboBox
            // 
            this.sizeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.sizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeComboBox.FormattingEnabled = true;
            this.sizeComboBox.Location = new System.Drawing.Point(30, 129);
            this.sizeComboBox.Name = "sizeComboBox";
            this.sizeComboBox.Size = new System.Drawing.Size(426, 28);
            this.sizeComboBox.TabIndex = 6;
            this.sizeComboBox.SelectedIndexChanged += new System.EventHandler(this.sizeComboBox_SelectedIndexChanged);
            // 
            // makerLabel
            // 
            this.makerLabel.AutoSize = true;
            this.makerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.makerLabel.Location = new System.Drawing.Point(30, 157);
            this.makerLabel.Name = "makerLabel";
            this.makerLabel.Size = new System.Drawing.Size(131, 20);
            this.makerLabel.TabIndex = 7;
            this.makerLabel.Text = "Производитель";
            // 
            // makerComboBox
            // 
            this.makerComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.makerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.makerComboBox.FormattingEnabled = true;
            this.makerComboBox.Location = new System.Drawing.Point(30, 177);
            this.makerComboBox.Name = "makerComboBox";
            this.makerComboBox.Size = new System.Drawing.Size(426, 28);
            this.makerComboBox.TabIndex = 8;
            this.makerComboBox.SelectedIndexChanged += new System.EventHandler(this.makerComboBox_SelectedIndexChanged);
            // 
            // addRamOkButton
            // 
            this.addRamOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addRamOkButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addRamOkButton.Location = new System.Drawing.Point(30, 249);
            this.addRamOkButton.Name = "addRamOkButton";
            this.addRamOkButton.Size = new System.Drawing.Size(426, 34);
            this.addRamOkButton.TabIndex = 9;
            this.addRamOkButton.Text = "Добавить";
            this.addRamOkButton.UseVisualStyleBackColor = true;
            this.addRamOkButton.Click += new System.EventHandler(this.addRamOkButton_Click);
            // 
            // AddRamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 298);
            this.Controls.Add(this.addRamOkButton);
            this.Controls.Add(this.makerComboBox);
            this.Controls.Add(this.makerLabel);
            this.Controls.Add(this.sizeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddrComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modelTB);
            this.Controls.Add(this.modelLabel);
            this.Name = "AddRamForm";
            this.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.Text = "Добавить ОЗУ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.TextBox modelTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddrComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sizeComboBox;
        private System.Windows.Forms.Label makerLabel;
        private System.Windows.Forms.ComboBox makerComboBox;
        private System.Windows.Forms.Button addRamOkButton;
    }
}