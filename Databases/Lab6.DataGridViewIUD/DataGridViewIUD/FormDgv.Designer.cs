namespace DataGridViewIUD
{
    partial class FormDgv
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
            this.dgvHdd = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvRam = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRam)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHdd
            // 
            this.dgvHdd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHdd.Location = new System.Drawing.Point(0, 0);
            this.dgvHdd.Name = "dgvHdd";
            this.dgvHdd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHdd.Size = new System.Drawing.Size(484, 215);
            this.dgvHdd.TabIndex = 0;
            this.dgvHdd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvHdd_CellValueChanged);
            this.dgvHdd.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvHdd_RowValidating);
            this.dgvHdd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DgvHdd_UserDeletingRow);
            this.dgvHdd.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DgvHdd_PreviewKeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvHdd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvRam);
            this.splitContainer1.Size = new System.Drawing.Size(484, 461);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvRam
            // 
            this.dgvRam.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRam.Location = new System.Drawing.Point(0, 0);
            this.dgvRam.Name = "dgvRam";
            this.dgvRam.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRam.Size = new System.Drawing.Size(484, 242);
            this.dgvRam.TabIndex = 0;
            this.dgvRam.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRam_CellValueChanged);
            this.dgvRam.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvRam_RowValidating);
            this.dgvRam.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DgvRam_UserDeletingRow);
            this.dgvRam.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DgvRam_PreviewKeyDown);
            // 
            // FormDgv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormDgv";
            this.Text = "Память";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvHdd)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvRam;
    }
}