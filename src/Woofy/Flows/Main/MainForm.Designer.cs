namespace Woofy.Flows.Main
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.SplitContainer splitContainer1;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
			this.dgvwTasks = new System.Windows.Forms.DataGridView();
			this.colStatus = new System.Windows.Forms.DataGridViewImageColumn();
			this.colComic = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDownloadedStrips = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colCurrentPage = new System.Windows.Forms.DataGridViewLinkColumn();
			this.txtAppLog = new System.Windows.Forms.RichTextBox();
			this.tsbOpenFolder = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpenTaskFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemNewTask = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPauseTask = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.tsbAddComic = new System.Windows.Forms.ToolStripButton();
			this.tsbRemoveComic = new System.Windows.Forms.ToolStripButton();
			this.tsbPauseComic = new System.Windows.Forms.ToolStripButton();
			this.tsbSettings = new System.Windows.Forms.ToolStripButton();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.hideShowWoofyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.stopAllTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startAllTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvwTasks)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.trayMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 25);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(this.dgvwTasks);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(this.txtAppLog);
			splitContainer1.Size = new System.Drawing.Size(642, 491);
			splitContainer1.SplitterDistance = 335;
			splitContainer1.TabIndex = 10;
			// 
			// dgvwTasks
			// 
			this.dgvwTasks.AllowUserToAddRows = false;
			this.dgvwTasks.AllowUserToDeleteRows = false;
			this.dgvwTasks.AllowUserToResizeRows = false;
			this.dgvwTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatus,
            this.colComic,
            this.colDownloadedStrips,
            this.colCurrentPage});
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvwTasks.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgvwTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvwTasks.Location = new System.Drawing.Point(0, 0);
			this.dgvwTasks.Margin = new System.Windows.Forms.Padding(2);
			this.dgvwTasks.MultiSelect = false;
			this.dgvwTasks.Name = "dgvwTasks";
			this.dgvwTasks.ReadOnly = true;
			this.dgvwTasks.RowHeadersVisible = false;
			this.dgvwTasks.RowTemplate.Height = 24;
			this.dgvwTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvwTasks.Size = new System.Drawing.Size(642, 335);
			this.dgvwTasks.TabIndex = 7;
			this.dgvwTasks.DoubleClick += new System.EventHandler(this.OnToggleComicState);
			this.dgvwTasks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnGridCellFormatting);
			this.dgvwTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridCellContentClick);
			// 
			// colStatus
			// 
			this.colStatus.DataPropertyName = "Status";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.NullValue = null;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Menu;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.MenuText;
			this.colStatus.DefaultCellStyle = dataGridViewCellStyle3;
			this.colStatus.HeaderText = "";
			this.colStatus.MinimumWidth = 20;
			this.colStatus.Name = "colStatus";
			this.colStatus.ReadOnly = true;
			this.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colStatus.Width = 20;
			// 
			// colComic
			// 
			this.colComic.DataPropertyName = "Name";
			this.colComic.FillWeight = 30F;
			this.colComic.HeaderText = "Comic";
			this.colComic.Name = "colComic";
			this.colComic.ReadOnly = true;
			this.colComic.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colComic.Width = 175;
			// 
			// colDownloadedStrips
			// 
			this.colDownloadedStrips.DataPropertyName = "DownloadedStrips";
			this.colDownloadedStrips.HeaderText = "Strips";
			this.colDownloadedStrips.Name = "colDownloadedStrips";
			this.colDownloadedStrips.ReadOnly = true;
			this.colDownloadedStrips.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colDownloadedStrips.Width = 50;
			// 
			// colCurrentPage
			// 
			this.colCurrentPage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colCurrentPage.DataPropertyName = "CurrentPage";
			this.colCurrentPage.FillWeight = 70F;
			this.colCurrentPage.HeaderText = "Current page";
			this.colCurrentPage.LinkColor = System.Drawing.Color.Blue;
			this.colCurrentPage.Name = "colCurrentPage";
			this.colCurrentPage.ReadOnly = true;
			this.colCurrentPage.VisitedLinkColor = System.Drawing.Color.Blue;
			// 
			// txtAppLog
			// 
			this.txtAppLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAppLog.Location = new System.Drawing.Point(0, 0);
			this.txtAppLog.Name = "txtAppLog";
			this.txtAppLog.ReadOnly = true;
			this.txtAppLog.Size = new System.Drawing.Size(642, 152);
			this.txtAppLog.TabIndex = 9;
			this.txtAppLog.Text = "";
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbOpenFolder
			// 
			this.tsbOpenFolder.Image = global::Woofy.Properties.Resources.OpenFolder;
			this.tsbOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenFolder.Name = "tsbOpenFolder";
			this.tsbOpenFolder.Size = new System.Drawing.Size(84, 22);
			this.tsbOpenFolder.Text = "Open folder";
			this.tsbOpenFolder.ToolTipText = "Open the comic\'s download folder";
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenTaskFolder,
            this.toolStripSeparator1,
            this.toolStripMenuItemNewTask,
            this.toolStripMenuItemPauseTask});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenuStrip.Size = new System.Drawing.Size(172, 76);
			// 
			// toolStripMenuItemOpenTaskFolder
			// 
			this.toolStripMenuItemOpenTaskFolder.Image = global::Woofy.Properties.Resources.OpenFolder;
			this.toolStripMenuItemOpenTaskFolder.Name = "toolStripMenuItemOpenTaskFolder";
			this.toolStripMenuItemOpenTaskFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.toolStripMenuItemOpenTaskFolder.Size = new System.Drawing.Size(171, 22);
			this.toolStripMenuItemOpenTaskFolder.Text = "Open folder";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
			// 
			// toolStripMenuItemNewTask
			// 
			this.toolStripMenuItemNewTask.Image = global::Woofy.Properties.Resources.New;
			this.toolStripMenuItemNewTask.Name = "toolStripMenuItemNewTask";
			this.toolStripMenuItemNewTask.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.toolStripMenuItemNewTask.Size = new System.Drawing.Size(171, 22);
			this.toolStripMenuItemNewTask.Text = "New";
			// 
			// toolStripMenuItemPauseTask
			// 
			this.toolStripMenuItemPauseTask.Image = global::Woofy.Properties.Resources.Paused;
			this.toolStripMenuItemPauseTask.Name = "toolStripMenuItemPauseTask";
			this.toolStripMenuItemPauseTask.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.toolStripMenuItemPauseTask.Size = new System.Drawing.Size(171, 22);
			this.toolStripMenuItemPauseTask.Text = "Pause";
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddComic,
            this.tsbRemoveComic,
            this.tsbPauseComic,
            toolStripSeparator2,
            this.tsbOpenFolder,
            this.tsbSettings});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(642, 25);
			this.toolStrip.TabIndex = 8;
			this.toolStrip.Text = "toolStrip";
			// 
			// tsbAddComic
			// 
			this.tsbAddComic.Image = global::Woofy.Properties.Resources.New;
			this.tsbAddComic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAddComic.Name = "tsbAddComic";
			this.tsbAddComic.Size = new System.Drawing.Size(87, 22);
			this.tsbAddComic.Text = "Add comic...";
			this.tsbAddComic.ToolTipText = "Add a new comic (Ctrl+N)";
			// 
			// tsbRemoveComic
			// 
			this.tsbRemoveComic.Image = global::Woofy.Properties.Resources.Delete;
			this.tsbRemoveComic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRemoveComic.Name = "tsbRemoveComic";
			this.tsbRemoveComic.Size = new System.Drawing.Size(95, 22);
			this.tsbRemoveComic.Text = "Remove comic";
			this.tsbRemoveComic.Click += new System.EventHandler(this.OnRemoveComic);
			// 
			// tsbPauseComic
			// 
			this.tsbPauseComic.Image = global::Woofy.Properties.Resources.Paused;
			this.tsbPauseComic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPauseComic.Name = "tsbPauseComic";
			this.tsbPauseComic.Size = new System.Drawing.Size(98, 22);
			this.tsbPauseComic.Text = "Pause/Resume";
			this.tsbPauseComic.ToolTipText = "Pause/resume the selected comic";
			this.tsbPauseComic.Click += new System.EventHandler(this.OnToggleComicState);
			// 
			// tsbSettings
			// 
			this.tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbSettings.Image = global::Woofy.Properties.Resources.Settings;
			this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSettings.Name = "tsbSettings";
			this.tsbSettings.Size = new System.Drawing.Size(66, 22);
			this.tsbSettings.Text = "Settings";
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.trayMenuStrip;
			this.notifyIcon.Visible = true;
			// 
			// trayMenuStrip
			// 
			this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideShowWoofyToolStripMenuItem,
            this.toolStripSeparator3,
            this.stopAllTasksToolStripMenuItem,
            this.startAllTasksToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
			this.trayMenuStrip.Name = "trayMenuStrip";
			this.trayMenuStrip.ShowImageMargin = false;
			this.trayMenuStrip.Size = new System.Drawing.Size(136, 104);
			// 
			// hideShowWoofyToolStripMenuItem
			// 
			this.hideShowWoofyToolStripMenuItem.Name = "hideShowWoofyToolStripMenuItem";
			this.hideShowWoofyToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.hideShowWoofyToolStripMenuItem.Text = "&Hide/Show Woofy";
			this.hideShowWoofyToolStripMenuItem.Click += new System.EventHandler(this.hideShowWoofyToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(132, 6);
			// 
			// stopAllTasksToolStripMenuItem
			// 
			this.stopAllTasksToolStripMenuItem.Name = "stopAllTasksToolStripMenuItem";
			this.stopAllTasksToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.stopAllTasksToolStripMenuItem.Text = "&Pause all tasks";
			// 
			// startAllTasksToolStripMenuItem
			// 
			this.startAllTasksToolStripMenuItem.Name = "startAllTasksToolStripMenuItem";
			this.startAllTasksToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.startAllTasksToolStripMenuItem.Text = "&Resume all tasks";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(132, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(642, 516);
			this.Controls.Add(splitContainer1);
			this.Controls.Add(this.toolStrip);
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "Woofy";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvwTasks)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.trayMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvwTasks;
		private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewTask;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPauseTask;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenTaskFolder;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hideShowWoofyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem stopAllTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.RichTextBox txtAppLog;
		private System.Windows.Forms.DataGridViewImageColumn colStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn colComic;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDownloadedStrips;
		private System.Windows.Forms.DataGridViewLinkColumn colCurrentPage;
		private System.Windows.Forms.ToolStripButton tsbAddComic;
		private System.Windows.Forms.ToolStripButton tsbPauseComic;
		private System.Windows.Forms.ToolStripButton tsbRemoveComic;
		private System.Windows.Forms.ToolStripButton tsbOpenFolder;
    }
}