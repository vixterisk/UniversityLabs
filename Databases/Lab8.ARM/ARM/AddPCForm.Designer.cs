namespace ARM
{
    partial class AddPCForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.addPcOkButton = new System.Windows.Forms.Button();
            this.cpuComboBox = new System.Windows.Forms.ComboBox();
            this.caseComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hddComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Процессор";
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // addPcOkButton
            // 
            this.addPcOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addPcOkButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addPcOkButton.Location = new System.Drawing.Point(30, 192);
            this.addPcOkButton.Name = "addPcOkButton";
            this.addPcOkButton.Size = new System.Drawing.Size(412, 34);
            this.addPcOkButton.TabIndex = 6;
            this.addPcOkButton.Text = "Добавить";
            this.addPcOkButton.UseVisualStyleBackColor = true;
            this.addPcOkButton.Click += new System.EventHandler(this.addPcOkButton_Click);
            // 
            // cpuComboBox
            // 
            this.cpuComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.cpuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cpuComboBox.FormattingEnabled = true;
            this.cpuComboBox.Location = new System.Drawing.Point(30, 35);
            this.cpuComboBox.Name = "cpuComboBox";
            this.cpuComboBox.Size = new System.Drawing.Size(412, 28);
            this.cpuComboBox.TabIndex = 1;
            this.cpuComboBox.SelectedIndexChanged += new System.EventHandler(this.cpuComboBox_SelectedIndexChanged);
            // 
            // caseComboBox
            // 
            this.caseComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.caseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.caseComboBox.FormattingEnabled = true;
            this.caseComboBox.Location = new System.Drawing.Point(30, 131);
            this.caseComboBox.Name = "caseComboBox";
            this.caseComboBox.Size = new System.Drawing.Size(412, 28);
            this.caseComboBox.TabIndex = 5;
            this.caseComboBox.SelectedIndexChanged += new System.EventHandler(this.caseComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(30, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Корпус";
            // 
            // hddComboBox
            // 
            this.hddComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.hddComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hddComboBox.FormattingEnabled = true;
            this.hddComboBox.Location = new System.Drawing.Point(30, 83);
            this.hddComboBox.Name = "hddComboBox";
            this.hddComboBox.Size = new System.Drawing.Size(412, 28);
            this.hddComboBox.TabIndex = 3;
            this.hddComboBox.SelectedIndexChanged += new System.EventHandler(this.hddComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(30, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Жесткий диск";
            // 
            // AddPCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 241);
            this.Controls.Add(this.addPcOkButton);
            this.Controls.Add(this.caseComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hddComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cpuComboBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AddPCForm";
            this.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.Text = "Добавление ПК";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private System.Windows.Forms.Button addPcOkButton;
        private System.Windows.Forms.ComboBox cpuComboBox;
        private System.Windows.Forms.ComboBox caseComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox hddComboBox;
        private System.Windows.Forms.Label label2;
    }
}