namespace ARM
{
    partial class FormTables
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
            this.tablesTab = new System.Windows.Forms.TabControl();
            this.pcInformationPage = new System.Windows.Forms.TabPage();
            this.pcInformationDGV = new System.Windows.Forms.DataGridView();
            this.pcRamPage = new System.Windows.Forms.TabPage();
            this.pcRamDGV = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ramInformationPage = new System.Windows.Forms.TabPage();
            this.ramInformationDGV = new System.Windows.Forms.DataGridView();
            this.ddrPage = new System.Windows.Forms.TabPage();
            this.ddrDGV = new System.Windows.Forms.DataGridView();
            this.userPage = new System.Windows.Forms.TabPage();
            this.userDGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reg_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_admin = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.IntelPcQueryButton = new System.Windows.Forms.Button();
            this.DDR4RamButton = new System.Windows.Forms.Button();
            this.tablesTab.SuspendLayout();
            this.pcInformationPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcInformationDGV)).BeginInit();
            this.pcRamPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcRamDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.ramInformationPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramInformationDGV)).BeginInit();
            this.ddrPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddrDGV)).BeginInit();
            this.userPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablesTab
            // 
            this.tablesTab.Controls.Add(this.pcInformationPage);
            this.tablesTab.Controls.Add(this.pcRamPage);
            this.tablesTab.Controls.Add(this.ramInformationPage);
            this.tablesTab.Controls.Add(this.ddrPage);
            this.tablesTab.Controls.Add(this.userPage);
            this.tablesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesTab.Location = new System.Drawing.Point(0, 0);
            this.tablesTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tablesTab.Name = "tablesTab";
            this.tablesTab.SelectedIndex = 0;
            this.tablesTab.Size = new System.Drawing.Size(925, 694);
            this.tablesTab.TabIndex = 0;
            // 
            // pcInformationPage
            // 
            this.pcInformationPage.Controls.Add(this.pcInformationDGV);
            this.pcInformationPage.Location = new System.Drawing.Point(4, 29);
            this.pcInformationPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcInformationPage.Name = "pcInformationPage";
            this.pcInformationPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcInformationPage.Size = new System.Drawing.Size(917, 661);
            this.pcInformationPage.TabIndex = 0;
            this.pcInformationPage.Text = "Информация о ПК";
            this.pcInformationPage.UseVisualStyleBackColor = true;
            // 
            // pcInformationDGV
            // 
            this.pcInformationDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pcInformationDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pcInformationDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcInformationDGV.Location = new System.Drawing.Point(4, 5);
            this.pcInformationDGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcInformationDGV.Name = "pcInformationDGV";
            this.pcInformationDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pcInformationDGV.Size = new System.Drawing.Size(909, 651);
            this.pcInformationDGV.TabIndex = 0;
            this.pcInformationDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.pcInformationDGV_CellValueChanged);
            this.pcInformationDGV.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.pcInformationDGV_RowValidating);
            this.pcInformationDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.pcInformationDGV_UserDeletingRow);
            this.pcInformationDGV.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pcInformationDGV_PreviewKeyDown);
            // 
            // pcRamPage
            // 
            this.pcRamPage.Controls.Add(this.pcRamDGV);
            this.pcRamPage.Controls.Add(this.dataGridView2);
            this.pcRamPage.Location = new System.Drawing.Point(4, 29);
            this.pcRamPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcRamPage.Name = "pcRamPage";
            this.pcRamPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcRamPage.Size = new System.Drawing.Size(917, 661);
            this.pcRamPage.TabIndex = 1;
            this.pcRamPage.Text = "ОЗУ, подключенные в ПК";
            this.pcRamPage.UseVisualStyleBackColor = true;
            // 
            // pcRamDGV
            // 
            this.pcRamDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pcRamDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pcRamDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcRamDGV.Location = new System.Drawing.Point(4, 5);
            this.pcRamDGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcRamDGV.Name = "pcRamDGV";
            this.pcRamDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pcRamDGV.Size = new System.Drawing.Size(909, 651);
            this.pcRamDGV.TabIndex = 1;
            this.pcRamDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.pcRamDGV_CellEndEdit);
            this.pcRamDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.pcRamDGV_CellValueChanged);
            this.pcRamDGV.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.pcRamDGV_RowValidating);
            this.pcRamDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.pcRamDGV_UserDeletingRow);
            this.pcRamDGV.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pcRamDGV_PreviewKeyDown);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(4, 5);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(909, 651);
            this.dataGridView2.TabIndex = 0;
            // 
            // ramInformationPage
            // 
            this.ramInformationPage.Controls.Add(this.ramInformationDGV);
            this.ramInformationPage.Location = new System.Drawing.Point(4, 29);
            this.ramInformationPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ramInformationPage.Name = "ramInformationPage";
            this.ramInformationPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ramInformationPage.Size = new System.Drawing.Size(917, 661);
            this.ramInformationPage.TabIndex = 2;
            this.ramInformationPage.Text = "Информация об ОЗУ";
            this.ramInformationPage.UseVisualStyleBackColor = true;
            // 
            // ramInformationDGV
            // 
            this.ramInformationDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ramInformationDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ramInformationDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ramInformationDGV.Location = new System.Drawing.Point(4, 5);
            this.ramInformationDGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ramInformationDGV.Name = "ramInformationDGV";
            this.ramInformationDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ramInformationDGV.Size = new System.Drawing.Size(909, 651);
            this.ramInformationDGV.TabIndex = 0;
            this.ramInformationDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ramInformationDGV_CellEndEdit);
            this.ramInformationDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ramInformationDGV_CellValueChanged);
            this.ramInformationDGV.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ramInformationDGV_RowValidating);
            this.ramInformationDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ramInformationDGV_UserDeletingRow);
            this.ramInformationDGV.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ramInformationDGV_PreviewKeyDown);
            // 
            // ddrPage
            // 
            this.ddrPage.Controls.Add(this.ddrDGV);
            this.ddrPage.Location = new System.Drawing.Point(4, 29);
            this.ddrPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ddrPage.Name = "ddrPage";
            this.ddrPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ddrPage.Size = new System.Drawing.Size(917, 663);
            this.ddrPage.TabIndex = 3;
            this.ddrPage.Text = "Поколения DDR";
            this.ddrPage.UseVisualStyleBackColor = true;
            // 
            // ddrDGV
            // 
            this.ddrDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ddrDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddrDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddrDGV.Location = new System.Drawing.Point(4, 5);
            this.ddrDGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ddrDGV.Name = "ddrDGV";
            this.ddrDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ddrDGV.Size = new System.Drawing.Size(909, 653);
            this.ddrDGV.TabIndex = 0;
            this.ddrDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddrDGV_CellValueChanged);
            this.ddrDGV.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ddrDGV_RowValidating);
            this.ddrDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ddrDGV_UserDeletingRow);
            this.ddrDGV.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ddrDGV_PreviewKeyDown);
            // 
            // userPage
            // 
            this.userPage.Controls.Add(this.userDGV);
            this.userPage.Location = new System.Drawing.Point(4, 29);
            this.userPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userPage.Name = "userPage";
            this.userPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userPage.Size = new System.Drawing.Size(917, 663);
            this.userPage.TabIndex = 4;
            this.userPage.Text = "Пользователи";
            this.userPage.UseVisualStyleBackColor = true;
            // 
            // userDGV
            // 
            this.userDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.userDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.login,
            this.password,
            this.salt,
            this.reg_date,
            this.is_admin});
            this.userDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userDGV.Location = new System.Drawing.Point(4, 5);
            this.userDGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userDGV.Name = "userDGV";
            this.userDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userDGV.Size = new System.Drawing.Size(909, 653);
            this.userDGV.TabIndex = 0;
            this.userDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDGV_CellClick);
            this.userDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDGV_CellValueChanged);
            this.userDGV.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.userDGV_ColumnWidthChanged);
            this.userDGV.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.userDGV_RowValidating);
            this.userDGV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.userDGV_Scroll);
            this.userDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.userDGV_UserDeletingRow);
            this.userDGV.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.userDGV_PreviewKeyDown);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // login
            // 
            this.login.HeaderText = "Логин";
            this.login.Name = "login";
            // 
            // password
            // 
            this.password.HeaderText = "Пароль";
            this.password.Name = "password";
            // 
            // salt
            // 
            this.salt.HeaderText = "Соль";
            this.salt.Name = "salt";
            // 
            // reg_date
            // 
            this.reg_date.HeaderText = "Дата регистрации";
            this.reg_date.Name = "reg_date";
            // 
            // is_admin
            // 
            this.is_admin.HeaderText = "Администратор АРМ?";
            this.is_admin.Name = "is_admin";
            this.is_admin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(10, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tablesTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(925, 765);
            this.splitContainer1.SplitterDistance = 694;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.IntelPcQueryButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.DDR4RamButton);
            this.splitContainer2.Size = new System.Drawing.Size(925, 65);
            this.splitContainer2.SplitterDistance = 462;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // IntelPcQueryButton
            // 
            this.IntelPcQueryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IntelPcQueryButton.Location = new System.Drawing.Point(0, 0);
            this.IntelPcQueryButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IntelPcQueryButton.Name = "IntelPcQueryButton";
            this.IntelPcQueryButton.Size = new System.Drawing.Size(462, 65);
            this.IntelPcQueryButton.TabIndex = 0;
            this.IntelPcQueryButton.Text = "ПК на базе процессоров от Intel";
            this.IntelPcQueryButton.UseVisualStyleBackColor = true;
            this.IntelPcQueryButton.Click += new System.EventHandler(this.IntelPcQueryButton_Click);
            // 
            // DDR4RamButton
            // 
            this.DDR4RamButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DDR4RamButton.Location = new System.Drawing.Point(0, 0);
            this.DDR4RamButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DDR4RamButton.Name = "DDR4RamButton";
            this.DDR4RamButton.Size = new System.Drawing.Size(461, 65);
            this.DDR4RamButton.TabIndex = 0;
            this.DDR4RamButton.Text = "ОЗУ с поколением памяти DDR4";
            this.DDR4RamButton.UseVisualStyleBackColor = true;
            this.DDR4RamButton.Click += new System.EventHandler(this.DDR4RamButton_Click);
            // 
            // FormTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 785);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormTables";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Компоненты ПК";
            this.tablesTab.ResumeLayout(false);
            this.pcInformationPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcInformationDGV)).EndInit();
            this.pcRamPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcRamDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ramInformationPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ramInformationDGV)).EndInit();
            this.ddrPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ddrDGV)).EndInit();
            this.userPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userDGV)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tablesTab;
        private System.Windows.Forms.TabPage pcInformationPage;
        private System.Windows.Forms.TabPage pcRamPage;
        private System.Windows.Forms.TabPage ramInformationPage;
        private System.Windows.Forms.TabPage ddrPage;
        private System.Windows.Forms.TabPage userPage;
        private System.Windows.Forms.DataGridView userDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn salt;
        private System.Windows.Forms.DataGridViewTextBoxColumn reg_date;
        private System.Windows.Forms.DataGridViewCheckBoxColumn is_admin;
        private System.Windows.Forms.DataGridView pcInformationDGV;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView ramInformationDGV;
        private System.Windows.Forms.DataGridView ddrDGV;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button IntelPcQueryButton;
        private System.Windows.Forms.Button DDR4RamButton;
        private System.Windows.Forms.DataGridView pcRamDGV;
    }
}