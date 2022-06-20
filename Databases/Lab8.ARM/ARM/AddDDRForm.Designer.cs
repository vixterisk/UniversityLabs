namespace ARM
{
    partial class AddDDRForm
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
            this.generationLabel = new System.Windows.Forms.Label();
            this.ddrTB = new System.Windows.Forms.TextBox();
            this.AddDDRButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generationLabel
            // 
            this.generationLabel.AutoSize = true;
            this.generationLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.generationLabel.Location = new System.Drawing.Point(30, 15);
            this.generationLabel.Name = "generationLabel";
            this.generationLabel.Size = new System.Drawing.Size(133, 20);
            this.generationLabel.TabIndex = 0;
            this.generationLabel.Text = "Поколение DDR";
            // 
            // ddrTB
            // 
            this.ddrTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.ddrTB.Location = new System.Drawing.Point(30, 35);
            this.ddrTB.Name = "ddrTB";
            this.ddrTB.Size = new System.Drawing.Size(399, 26);
            this.ddrTB.TabIndex = 1;
            this.ddrTB.TextChanged += new System.EventHandler(this.ddrTB_TextChanged);
            // 
            // AddDDRButton
            // 
            this.AddDDRButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddDDRButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddDDRButton.Location = new System.Drawing.Point(30, 92);
            this.AddDDRButton.Name = "AddDDRButton";
            this.AddDDRButton.Size = new System.Drawing.Size(399, 31);
            this.AddDDRButton.TabIndex = 2;
            this.AddDDRButton.Text = "Добавить";
            this.AddDDRButton.UseVisualStyleBackColor = true;
            this.AddDDRButton.Click += new System.EventHandler(this.AddDDRButton_Click);
            // 
            // AddDDRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 138);
            this.Controls.Add(this.AddDDRButton);
            this.Controls.Add(this.ddrTB);
            this.Controls.Add(this.generationLabel);
            this.Name = "AddDDRForm";
            this.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.Text = "Добавить поколение DDR";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label generationLabel;
        private System.Windows.Forms.TextBox ddrTB;
        private System.Windows.Forms.Button AddDDRButton;
    }
}