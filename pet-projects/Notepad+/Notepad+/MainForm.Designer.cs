namespace Notepad_
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileRenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileCloseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editUndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatFontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formattingCodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
            this.fileNewCSharpWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.formatMenuItem,
            this.settingsMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(794, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenuItem,
            this.fileNewWindowMenuItem,
            this.fileOpenMenuItem,
            this.fileSaveMenuItem,
            this.fileSaveAsMenuItem,
            this.fileSaveAllMenuItem,
            this.fileRenameMenuItem,
            this.fileCloseMenuItem,
            this.fileCloseAllMenuItem,
            this.fileExitMenuItem,
            this.fileNewCSharpWindowMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            this.fileMenuItem.DropDownOpening += new System.EventHandler(this.FileMenuItem_DropDownOpening);
            // 
            // fileNewMenuItem
            // 
            this.fileNewMenuItem.Name = "fileNewMenuItem";
            this.fileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileNewMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileNewMenuItem.Text = "New";
            this.fileNewMenuItem.Click += new System.EventHandler(this.FileNewMenuItem_Click);
            // 
            // fileNewWindowMenuItem
            // 
            this.fileNewWindowMenuItem.Name = "fileNewWindowMenuItem";
            this.fileNewWindowMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.fileNewWindowMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileNewWindowMenuItem.Text = "New Window";
            this.fileNewWindowMenuItem.Click += new System.EventHandler(this.FileNewWindowMenuItem_Click);
            // 
            // fileOpenMenuItem
            // 
            this.fileOpenMenuItem.Name = "fileOpenMenuItem";
            this.fileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpenMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileOpenMenuItem.Text = "Open...";
            this.fileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // fileSaveMenuItem
            // 
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSaveMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileSaveMenuItem.Text = "Save";
            this.fileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // fileSaveAsMenuItem
            // 
            this.fileSaveAsMenuItem.Name = "fileSaveAsMenuItem";
            this.fileSaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.fileSaveAsMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileSaveAsMenuItem.Text = "Save As...";
            this.fileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // fileSaveAllMenuItem
            // 
            this.fileSaveAllMenuItem.Name = "fileSaveAllMenuItem";
            this.fileSaveAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.fileSaveAllMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileSaveAllMenuItem.Text = "Save All";
            this.fileSaveAllMenuItem.Click += new System.EventHandler(this.FileSaveAllMenuItem_Click);
            // 
            // fileRenameMenuItem
            // 
            this.fileRenameMenuItem.Name = "fileRenameMenuItem";
            this.fileRenameMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileRenameMenuItem.Text = "Rename...";
            this.fileRenameMenuItem.Click += new System.EventHandler(this.FileRenameMenuItem_Click);
            // 
            // fileCloseMenuItem
            // 
            this.fileCloseMenuItem.Name = "fileCloseMenuItem";
            this.fileCloseMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.fileCloseMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileCloseMenuItem.Text = "Close";
            this.fileCloseMenuItem.Click += new System.EventHandler(this.FileCloseMenuItem_Click);
            // 
            // fileCloseAllMenuItem
            // 
            this.fileCloseAllMenuItem.Name = "fileCloseAllMenuItem";
            this.fileCloseAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.fileCloseAllMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileCloseAllMenuItem.Text = "Close All";
            this.fileCloseAllMenuItem.Click += new System.EventHandler(this.FileCloseAllMenuItem_Click);
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.fileExitMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileExitMenuItem.Text = "Exit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editUndoMenuItem,
            this.editRedoMenuItem,
            this.editCutMenuItem,
            this.editCopyMenuItem,
            this.editPasteMenuItem,
            this.editSelectAllMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "Edit";
            this.editMenuItem.DropDownOpening += new System.EventHandler(this.EditMenuItem_DropDownOpening);
            // 
            // editUndoMenuItem
            // 
            this.editUndoMenuItem.Name = "editUndoMenuItem";
            this.editUndoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.editUndoMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editUndoMenuItem.Text = "Undo";
            this.editUndoMenuItem.Click += new System.EventHandler(this.EditUndoMenuItem_Click);
            // 
            // editRedoMenuItem
            // 
            this.editRedoMenuItem.Name = "editRedoMenuItem";
            this.editRedoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.editRedoMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editRedoMenuItem.Text = "Redo";
            this.editRedoMenuItem.Click += new System.EventHandler(this.EditRedoMenuItem_Click);
            // 
            // editCutMenuItem
            // 
            this.editCutMenuItem.Name = "editCutMenuItem";
            this.editCutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.editCutMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editCutMenuItem.Text = "Cut";
            this.editCutMenuItem.Click += new System.EventHandler(this.EditCutMenuItem_Click);
            // 
            // editCopyMenuItem
            // 
            this.editCopyMenuItem.Name = "editCopyMenuItem";
            this.editCopyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.editCopyMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editCopyMenuItem.Text = "Copy";
            this.editCopyMenuItem.Click += new System.EventHandler(this.EditCopyMenuItem_Click);
            // 
            // editPasteMenuItem
            // 
            this.editPasteMenuItem.Name = "editPasteMenuItem";
            this.editPasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.editPasteMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editPasteMenuItem.Text = "Paste";
            this.editPasteMenuItem.Click += new System.EventHandler(this.EditPasteMenuItem_Click);
            // 
            // editSelectAllMenuItem
            // 
            this.editSelectAllMenuItem.Name = "editSelectAllMenuItem";
            this.editSelectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.editSelectAllMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editSelectAllMenuItem.Text = "Select All";
            this.editSelectAllMenuItem.Click += new System.EventHandler(this.EditSelectAllMenuItem_Click);
            // 
            // formatMenuItem
            // 
            this.formatMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatFontMenuItem,
            this.formatColorMenuItem,
            this.formattingCodeMenuItem});
            this.formatMenuItem.Name = "formatMenuItem";
            this.formatMenuItem.Size = new System.Drawing.Size(57, 20);
            this.formatMenuItem.Text = "Format";
            this.formatMenuItem.DropDownOpening += new System.EventHandler(this.FormatMenuItem_DropDownOpening);
            // 
            // formatFontMenuItem
            // 
            this.formatFontMenuItem.Name = "formatFontMenuItem";
            this.formatFontMenuItem.Size = new System.Drawing.Size(203, 22);
            this.formatFontMenuItem.Text = "Font";
            this.formatFontMenuItem.Click += new System.EventHandler(this.FormatFontMenuItem_Click);
            // 
            // formatColorMenuItem
            // 
            this.formatColorMenuItem.Name = "formatColorMenuItem";
            this.formatColorMenuItem.Size = new System.Drawing.Size(203, 22);
            this.formatColorMenuItem.Text = "Color";
            this.formatColorMenuItem.Click += new System.EventHandler(this.FormatColorMenuItem_Click);
            // 
            // formattingCodeMenuItem
            // 
            this.formattingCodeMenuItem.Name = "formattingCodeMenuItem";
            this.formattingCodeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.formattingCodeMenuItem.Size = new System.Drawing.Size(203, 22);
            this.formattingCodeMenuItem.Text = "Formatting code";
            this.formattingCodeMenuItem.Click += new System.EventHandler(this.FormattingCodeMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
            // 
            // timerAutoSave
            // 
            this.timerAutoSave.Enabled = true;
            this.timerAutoSave.Tick += new System.EventHandler(this.TimerAutoSave_Tick);
            // 
            // fileNewCSharpWindowMenuItem
            // 
            this.fileNewCSharpWindowMenuItem.Name = "fileNewCSharpWindowMenuItem";
            this.fileNewCSharpWindowMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileNewCSharpWindowMenuItem.Text = "New Window C# Mode";
            this.fileNewCSharpWindowMenuItem.Click += new System.EventHandler(this.FileNewCSharpWindowMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(794, 485);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "MainForm";
            this.Text = "Notepad+";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileRenameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileCloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileCloseAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editUndoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editRedoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPasteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSelectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatFontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewWindowMenuItem;
        private System.Windows.Forms.Timer timerAutoSave;
        private System.Windows.Forms.ToolStripMenuItem formattingCodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewCSharpWindowMenuItem;
    }
}
