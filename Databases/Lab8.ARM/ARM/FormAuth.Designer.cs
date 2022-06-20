namespace ARM
{
    partial class FormAuth
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loginGB = new System.Windows.Forms.GroupBox();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.passwordGB = new System.Windows.Forms.GroupBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.guestButton = new System.Windows.Forms.Button();
            this.regButton = new System.Windows.Forms.Button();
            this.authButton = new System.Windows.Forms.Button();
            this.errMain = new System.Windows.Forms.ErrorProvider(this.components);
            this.loginGB.SuspendLayout();
            this.passwordGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errMain)).BeginInit();
            this.SuspendLayout();
            // 
            // loginGB
            // 
            this.loginGB.Controls.Add(this.loginTB);
            this.loginGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.loginGB.Location = new System.Drawing.Point(0, 0);
            this.loginGB.Name = "loginGB";
            this.loginGB.Padding = new System.Windows.Forms.Padding(10, 3, 20, 3);
            this.loginGB.Size = new System.Drawing.Size(382, 74);
            this.loginGB.TabIndex = 2;
            this.loginGB.TabStop = false;
            this.loginGB.Text = "Логин";
            // 
            // loginTB
            // 
            this.loginTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.loginTB.Location = new System.Drawing.Point(10, 16);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(352, 20);
            this.loginTB.TabIndex = 0;
            this.loginTB.TextChanged += new System.EventHandler(this.loginTB_TextChanged);
            // 
            // passwordGB
            // 
            this.passwordGB.Controls.Add(this.passwordTB);
            this.passwordGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordGB.Location = new System.Drawing.Point(0, 74);
            this.passwordGB.Name = "passwordGB";
            this.passwordGB.Padding = new System.Windows.Forms.Padding(10, 3, 20, 3);
            this.passwordGB.Size = new System.Drawing.Size(382, 74);
            this.passwordGB.TabIndex = 3;
            this.passwordGB.TabStop = false;
            this.passwordGB.Text = "Пароль";
            // 
            // passwordTB
            // 
            this.passwordTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordTB.Location = new System.Drawing.Point(10, 16);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(352, 20);
            this.passwordTB.TabIndex = 0;
            this.passwordTB.TextChanged += new System.EventHandler(this.passwordTB_TextChanged);
            // 
            // guestButton
            // 
            this.guestButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guestButton.Location = new System.Drawing.Point(0, 198);
            this.guestButton.Name = "guestButton";
            this.guestButton.Size = new System.Drawing.Size(382, 23);
            this.guestButton.TabIndex = 5;
            this.guestButton.Text = "Гостевой вход";
            this.guestButton.UseVisualStyleBackColor = true;
            this.guestButton.Click += new System.EventHandler(this.guestButton_Click);
            // 
            // regButton
            // 
            this.regButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.regButton.Enabled = false;
            this.regButton.Location = new System.Drawing.Point(0, 175);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(382, 23);
            this.regButton.TabIndex = 6;
            this.regButton.Text = "Зарегистрироваться";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // authButton
            // 
            this.authButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.authButton.Enabled = false;
            this.authButton.Location = new System.Drawing.Point(0, 152);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(382, 23);
            this.authButton.TabIndex = 7;
            this.authButton.Text = "Авторизоваться";
            this.authButton.UseVisualStyleBackColor = true;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // errMain
            // 
            this.errMain.ContainerControl = this;
            // 
            // FormAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 221);
            this.Controls.Add(this.authButton);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.guestButton);
            this.Controls.Add(this.passwordGB);
            this.Controls.Add(this.loginGB);
            this.Name = "FormAuth";
            this.Text = "Добро пожаловать";
            this.loginGB.ResumeLayout(false);
            this.loginGB.PerformLayout();
            this.passwordGB.ResumeLayout(false);
            this.passwordGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox loginGB;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.GroupBox passwordGB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Button guestButton;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.Button authButton;
        private System.Windows.Forms.ErrorProvider errMain;
    }
}

