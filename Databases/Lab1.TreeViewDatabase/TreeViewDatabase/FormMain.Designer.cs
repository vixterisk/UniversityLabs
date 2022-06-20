namespace TreeViewDatabase
{
    partial class FormMain
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
            this.catsTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // catsTree
            // 
            this.catsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catsTree.Location = new System.Drawing.Point(0, 0);
            this.catsTree.Name = "catsTree";
            this.catsTree.Size = new System.Drawing.Size(800, 450);
            this.catsTree.TabIndex = 0;
            this.catsTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.catsTree_AfterCollapse);
            this.catsTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.catsTree_AfterExpand);
            this.catsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.catsTree);
            this.Name = "FormMain";
            this.Text = "Семейство кошачьих";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView catsTree;
    }
}

