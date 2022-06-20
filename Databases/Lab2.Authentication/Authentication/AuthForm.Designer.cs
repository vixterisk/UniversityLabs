namespace Authentication
{
    partial class AuthForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.passwordGB = new System.Windows.Forms.GroupBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.regButton = new System.Windows.Forms.Button();
            this.errMain = new System.Windows.Forms.ErrorProvider(this.components);
            this.authButton = new System.Windows.Forms.Button();
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
            this.loginGB.Size = new System.Drawing.Size(355, 74);
            this.loginGB.TabIndex = 1;
            this.loginGB.TabStop = false;
            this.loginGB.Text = "Логин";
            // 
            // loginTB
            // 
            this.loginTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.loginTB.Location = new System.Drawing.Point(10, 16);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(325, 20);
            this.loginTB.TabIndex = 0;
            this.loginTB.TextChanged += new System.EventHandler(this.loginTB_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(10, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(775, 20);
            this.textBox1.TabIndex = 0;
            // 
            // passwordGB
            // 
            this.passwordGB.Controls.Add(this.passwordTB);
            this.passwordGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordGB.Location = new System.Drawing.Point(0, 74);
            this.passwordGB.Name = "passwordGB";
            this.passwordGB.Padding = new System.Windows.Forms.Padding(10, 3, 20, 3);
            this.passwordGB.Size = new System.Drawing.Size(355, 74);
            this.passwordGB.TabIndex = 2;
            this.passwordGB.TabStop = false;
            this.passwordGB.Text = "Пароль";
            // 
            // passwordTB
            // 
            this.passwordTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordTB.Location = new System.Drawing.Point(10, 16);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(325, 20);
            this.passwordTB.TabIndex = 0;
            this.passwordTB.TextChanged += new System.EventHandler(this.passwordTB_TextChanged);
            // 
            // regButton
            // 
            this.regButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.regButton.Enabled = false;
            this.regButton.Location = new System.Drawing.Point(0, 173);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(355, 23);
            this.regButton.TabIndex = 3;
            this.regButton.Text = "Зарегистрироваться";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // errMain
            // 
            this.errMain.ContainerControl = this;
            // 
            // authButton
            // 
            this.authButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.authButton.Enabled = false;
            this.authButton.Location = new System.Drawing.Point(0, 150);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(355, 23);
            this.authButton.TabIndex = 4;
            this.authButton.Text = "Аутентифицироваться";
            this.authButton.UseVisualStyleBackColor = true;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 196);
            this.Controls.Add(this.authButton);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.passwordGB);
            this.Controls.Add(this.loginGB);
            this.MinimumSize = new System.Drawing.Size(371, 209);
            this.Name = "AuthForm";
            this.Text = "Аутентификация / Регистрация";
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox passwordGB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.ErrorProvider errMain;
        private System.Windows.Forms.Button authButton;
    }
}

