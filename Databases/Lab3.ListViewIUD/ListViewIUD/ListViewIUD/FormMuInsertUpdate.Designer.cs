namespace ListViewIUD
{
    partial class FormMuInsertUpdate
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
            this.components = new System.ComponentModel.Container();
            this.gbName = new System.Windows.Forms.GroupBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gbHead = new System.Windows.Forms.GroupBox();
            this.tbHead = new System.Windows.Forms.TextBox();
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.gbModifiedDate = new System.Windows.Forms.GroupBox();
            this.dtpModifiedDate = new System.Windows.Forms.DateTimePicker();
            this.btOK = new System.Windows.Forms.Button();
            this.errorProviderLogin = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderPassword = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbName.SuspendLayout();
            this.gbHead.SuspendLayout();
            this.gbAddress.SuspendLayout();
            this.gbModifiedDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.tbName);
            this.gbName.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbName.Location = new System.Drawing.Point(0, 0);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(423, 44);
            this.gbName.TabIndex = 0;
            this.gbName.TabStop = false;
            this.gbName.Text = "Логин";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(12, 16);
            this.tbName.MaxLength = 100;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(386, 20);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // gbHead
            // 
            this.gbHead.Controls.Add(this.tbHead);
            this.gbHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbHead.Location = new System.Drawing.Point(0, 44);
            this.gbHead.Name = "gbHead";
            this.gbHead.Size = new System.Drawing.Size(423, 44);
            this.gbHead.TabIndex = 1;
            this.gbHead.TabStop = false;
            this.gbHead.Text = "Пароль";
            // 
            // tbHead
            // 
            this.tbHead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHead.Location = new System.Drawing.Point(12, 16);
            this.tbHead.MaxLength = 500;
            this.tbHead.Name = "tbHead";
            this.tbHead.Size = new System.Drawing.Size(386, 20);
            this.tbHead.TabIndex = 0;
            this.tbHead.TextChanged += new System.EventHandler(this.tbHead_TextChanged);
            // 
            // gbAddress
            // 
            this.gbAddress.Controls.Add(this.tbAddress);
            this.gbAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAddress.Location = new System.Drawing.Point(0, 88);
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.Size = new System.Drawing.Size(423, 44);
            this.gbAddress.TabIndex = 2;
            this.gbAddress.TabStop = false;
            this.gbAddress.Text = "Соль";
            // 
            // tbAddress
            // 
            this.tbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddress.Location = new System.Drawing.Point(12, 16);
            this.tbAddress.MaxLength = 500;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(386, 20);
            this.tbAddress.TabIndex = 0;
            // 
            // gbModifiedDate
            // 
            this.gbModifiedDate.Controls.Add(this.dtpModifiedDate);
            this.gbModifiedDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbModifiedDate.Location = new System.Drawing.Point(0, 132);
            this.gbModifiedDate.Name = "gbModifiedDate";
            this.gbModifiedDate.Size = new System.Drawing.Size(423, 44);
            this.gbModifiedDate.TabIndex = 3;
            this.gbModifiedDate.TabStop = false;
            this.gbModifiedDate.Text = "Дата регистрации";
            // 
            // dtpModifiedDate
            // 
            this.dtpModifiedDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpModifiedDate.Location = new System.Drawing.Point(12, 16);
            this.dtpModifiedDate.Name = "dtpModifiedDate";
            this.dtpModifiedDate.Size = new System.Drawing.Size(386, 20);
            this.dtpModifiedDate.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 176);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(386, 37);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "btOK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // errorProviderLogin
            // 
            this.errorProviderLogin.ContainerControl = this;
            // 
            // errorProviderPassword
            // 
            this.errorProviderPassword.ContainerControl = this;
            // 
            // FormMuInsertUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 223);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gbModifiedDate);
            this.Controls.Add(this.gbAddress);
            this.Controls.Add(this.gbHead);
            this.Controls.Add(this.gbName);
            this.Name = "FormMuInsertUpdate";
            this.Text = "Пользователи";
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.gbHead.ResumeLayout(false);
            this.gbHead.PerformLayout();
            this.gbAddress.ResumeLayout(false);
            this.gbAddress.PerformLayout();
            this.gbModifiedDate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPassword)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.GroupBox gbHead;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbHead;
        private System.Windows.Forms.GroupBox gbAddress;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.GroupBox gbModifiedDate;
        private System.Windows.Forms.DateTimePicker dtpModifiedDate;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ErrorProvider errorProviderLogin;
        private System.Windows.Forms.ErrorProvider errorProviderPassword;
    }
}