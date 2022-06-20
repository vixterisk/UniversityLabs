namespace TransactionApp
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvRam = new System.Windows.Forms.ListView();
            this.btHelp = new System.Windows.Forms.Button();
            this.btAggregate = new System.Windows.Forms.Button();
            this.btCommit = new System.Windows.Forms.Button();
            this.btRollebackTo = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btInsert = new System.Windows.Forms.Button();
            this.btRollback = new System.Windows.Forms.Button();
            this.btSavepoint = new System.Windows.Forms.Button();
            this.btTransStart = new System.Windows.Forms.Button();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.isolationLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btFST = new System.Windows.Forms.Button();
            this.btSST = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvRam);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btSST);
            this.splitContainer1.Panel2.Controls.Add(this.btFST);
            this.splitContainer1.Panel2.Controls.Add(this.btHelp);
            this.splitContainer1.Panel2.Controls.Add(this.btAggregate);
            this.splitContainer1.Panel2.Controls.Add(this.btCommit);
            this.splitContainer1.Panel2.Controls.Add(this.btRollebackTo);
            this.splitContainer1.Panel2.Controls.Add(this.btUpdate);
            this.splitContainer1.Panel2.Controls.Add(this.btInsert);
            this.splitContainer1.Panel2.Controls.Add(this.btRollback);
            this.splitContainer1.Panel2.Controls.Add(this.btSavepoint);
            this.splitContainer1.Panel2.Controls.Add(this.btTransStart);
            this.splitContainer1.Panel2.Controls.Add(this.cbMode);
            this.splitContainer1.Panel2.Controls.Add(this.isolationLabel);
            this.splitContainer1.Size = new System.Drawing.Size(800, 506);
            this.splitContainer1.SplitterDistance = 527;
            this.splitContainer1.TabIndex = 0;
            // 
            // lvRam
            // 
            this.lvRam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRam.HideSelection = false;
            this.lvRam.Location = new System.Drawing.Point(0, 0);
            this.lvRam.Name = "lvRam";
            this.lvRam.Size = new System.Drawing.Size(527, 506);
            this.lvRam.TabIndex = 0;
            this.lvRam.UseCompatibleStateImageBehavior = false;
            this.lvRam.View = System.Windows.Forms.View.Details;
            // 
            // btHelp
            // 
            this.btHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btHelp.Location = new System.Drawing.Point(6, 471);
            this.btHelp.Name = "btHelp";
            this.btHelp.Size = new System.Drawing.Size(260, 23);
            this.btHelp.TabIndex = 10;
            this.btHelp.Text = "Help";
            this.btHelp.UseVisualStyleBackColor = true;
            this.btHelp.Click += new System.EventHandler(this.btHelp_Click);
            // 
            // btAggregate
            // 
            this.btAggregate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btAggregate.Location = new System.Drawing.Point(6, 226);
            this.btAggregate.Name = "btAggregate";
            this.btAggregate.Size = new System.Drawing.Size(260, 23);
            this.btAggregate.TabIndex = 9;
            this.btAggregate.Text = "Aggregate Function";
            this.btAggregate.UseVisualStyleBackColor = true;
            this.btAggregate.Click += new System.EventHandler(this.btAggregate_Click);
            // 
            // btCommit
            // 
            this.btCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btCommit.Location = new System.Drawing.Point(6, 139);
            this.btCommit.Name = "btCommit";
            this.btCommit.Size = new System.Drawing.Size(260, 23);
            this.btCommit.TabIndex = 8;
            this.btCommit.Text = "Commit";
            this.btCommit.UseVisualStyleBackColor = true;
            this.btCommit.Click += new System.EventHandler(this.btCommit_Click);
            // 
            // btRollebackTo
            // 
            this.btRollebackTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btRollebackTo.Location = new System.Drawing.Point(6, 110);
            this.btRollebackTo.Name = "btRollebackTo";
            this.btRollebackTo.Size = new System.Drawing.Size(260, 23);
            this.btRollebackTo.TabIndex = 7;
            this.btRollebackTo.Text = "Rollback To SavePoint";
            this.btRollebackTo.UseVisualStyleBackColor = true;
            this.btRollebackTo.Click += new System.EventHandler(this.btRollebackTo_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Location = new System.Drawing.Point(6, 313);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(260, 23);
            this.btUpdate.TabIndex = 6;
            this.btUpdate.Text = "\"Select Table\"";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btInsert
            // 
            this.btInsert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btInsert.Location = new System.Drawing.Point(6, 197);
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(260, 23);
            this.btInsert.TabIndex = 5;
            this.btInsert.Text = "Insert";
            this.btInsert.UseVisualStyleBackColor = true;
            this.btInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // btRollback
            // 
            this.btRollback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btRollback.Location = new System.Drawing.Point(6, 168);
            this.btRollback.Name = "btRollback";
            this.btRollback.Size = new System.Drawing.Size(260, 23);
            this.btRollback.TabIndex = 4;
            this.btRollback.Text = "Rollback";
            this.btRollback.UseVisualStyleBackColor = true;
            this.btRollback.Click += new System.EventHandler(this.btRollback_Click);
            // 
            // btSavepoint
            // 
            this.btSavepoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSavepoint.Location = new System.Drawing.Point(6, 81);
            this.btSavepoint.Name = "btSavepoint";
            this.btSavepoint.Size = new System.Drawing.Size(260, 23);
            this.btSavepoint.TabIndex = 3;
            this.btSavepoint.Text = "SavePoint";
            this.btSavepoint.UseVisualStyleBackColor = true;
            this.btSavepoint.Click += new System.EventHandler(this.btSavepoint_Click);
            // 
            // btTransStart
            // 
            this.btTransStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btTransStart.Location = new System.Drawing.Point(6, 52);
            this.btTransStart.Name = "btTransStart";
            this.btTransStart.Size = new System.Drawing.Size(260, 23);
            this.btTransStart.TabIndex = 2;
            this.btTransStart.Text = "Start Transaction";
            this.btTransStart.UseVisualStyleBackColor = true;
            this.btTransStart.Click += new System.EventHandler(this.btTransStart_Click);
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Read committed",
            "Repeatable read",
            "Serializable"});
            this.cbMode.Location = new System.Drawing.Point(6, 25);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(260, 21);
            this.cbMode.TabIndex = 1;
            // 
            // isolationLabel
            // 
            this.isolationLabel.AutoSize = true;
            this.isolationLabel.Location = new System.Drawing.Point(3, 9);
            this.isolationLabel.Name = "isolationLabel";
            this.isolationLabel.Size = new System.Drawing.Size(71, 13);
            this.isolationLabel.TabIndex = 0;
            this.isolationLabel.Text = "Isolation level";
            // 
            // btFST
            // 
            this.btFST.Location = new System.Drawing.Point(6, 255);
            this.btFST.Name = "btFST";
            this.btFST.Size = new System.Drawing.Size(260, 23);
            this.btFST.TabIndex = 11;
            this.btFST.Text = "First Serializable transaction";
            this.btFST.UseVisualStyleBackColor = true;
            this.btFST.Click += new System.EventHandler(this.btFST_Click);
            // 
            // btSST
            // 
            this.btSST.Location = new System.Drawing.Point(6, 284);
            this.btSST.Name = "btSST";
            this.btSST.Size = new System.Drawing.Size(260, 23);
            this.btSST.TabIndex = 12;
            this.btSST.Text = "Second Serializable transaction";
            this.btSST.UseVisualStyleBackColor = true;
            this.btSST.Click += new System.EventHandler(this.btSST_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Тут происходят транзакции";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvRam;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Label isolationLabel;
        private System.Windows.Forms.Button btRollback;
        private System.Windows.Forms.Button btSavepoint;
        private System.Windows.Forms.Button btTransStart;
        private System.Windows.Forms.Button btInsert;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btRollebackTo;
        private System.Windows.Forms.Button btCommit;
        private System.Windows.Forms.Button btAggregate;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btHelp;
        private System.Windows.Forms.Button btSST;
        private System.Windows.Forms.Button btFST;
    }
}

