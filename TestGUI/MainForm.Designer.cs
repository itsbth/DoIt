namespace TestGUI
{
    partial class MainForm
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
            this.hitList = new System.Windows.Forms.ListView();
            this.ScoreHeader = new System.Windows.Forms.ColumnHeader();
            this.NameHeader = new System.Windows.Forms.ColumnHeader();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // hitList
            // 
            this.hitList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ScoreHeader,
            this.NameHeader});
            this.hitList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hitList.Location = new System.Drawing.Point(0, 0);
            this.hitList.Name = "hitList";
            this.hitList.Size = new System.Drawing.Size(496, 436);
            this.hitList.TabIndex = 0;
            this.hitList.UseCompatibleStateImageBehavior = false;
            this.hitList.View = System.Windows.Forms.View.Details;
            // 
            // ScoreHeader
            // 
            this.ScoreHeader.Text = "Score";
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 404;
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(472, 20);
            this.searchBox.TabIndex = 1;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.searchBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.hitList);
            this.splitContainer.Size = new System.Drawing.Size(496, 476);
            this.splitContainer.SplitterDistance = 36;
            this.splitContainer.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 476);
            this.Controls.Add(this.splitContainer);
            this.Name = "MainForm";
            this.Text = "DoItTestGUI";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView hitList;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ColumnHeader ScoreHeader;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}

