using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Notepad_
{
    /// <summary>
    /// Форма блокнота, основное окно приложения Notepad+.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Создание контекстного меню для RichTextBox.
        /// </summary>
        /// <param name="rtBox">ссылка на RichTextBox, которому создается контекстное меню.</param>
        private void CreateContextMenu(RichTextBox rtBox)
        {
            var contextMenu = new ContextMenuStrip();
            var cutContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            var copyContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            var pasteContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            var selectAllContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            var changeFontContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            var changeColorContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            var formatContextMenu = new System.Windows.Forms.ToolStripMenuItem();

            formatContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            changeColorContextMenu,
            changeFontContextMenu});
            formatContextMenu.ForeColor = System.Drawing.Color.Black;
            formatContextMenu.Size = new System.Drawing.Size(180, 22);
            formatContextMenu.Text = "Format";

            contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            cutContextMenu,
            copyContextMenu,
            pasteContextMenu,
            selectAllContextMenu,
            formatContextMenu});
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new System.Drawing.Size(181, 182);

            if (CurrentTheme == "dark")
            {
                cutContextMenu.ForeColor = Color.FromArgb(170, 180, 190);
                copyContextMenu.ForeColor = Color.FromArgb(170, 180, 190);
                pasteContextMenu.ForeColor = Color.FromArgb(170, 180, 190);
                selectAllContextMenu.ForeColor = Color.FromArgb(170, 180, 190);
                formatContextMenu.ForeColor = Color.FromArgb(170, 180, 190);
                changeColorContextMenu.ForeColor = Color.FromArgb(170, 180, 190);
                changeFontContextMenu.ForeColor = Color.FromArgb(170, 180, 190);

                cutContextMenu.BackColor = Color.FromArgb(41, 43, 53);
                copyContextMenu.BackColor = Color.FromArgb(41, 43, 53);
                pasteContextMenu.BackColor = Color.FromArgb(41, 43, 53);
                selectAllContextMenu.BackColor = Color.FromArgb(41, 43, 53);
                formatContextMenu.BackColor = Color.FromArgb(41, 43, 53);
                changeColorContextMenu.BackColor = Color.FromArgb(41, 43, 53);
                changeFontContextMenu.BackColor = Color.FromArgb(41, 43, 53);
            }
            else
            {
                cutContextMenu.ForeColor = System.Drawing.Color.Black;
                copyContextMenu.ForeColor = System.Drawing.Color.Black;
                pasteContextMenu.ForeColor = System.Drawing.Color.Black;
                selectAllContextMenu.ForeColor = System.Drawing.Color.Black;
                formatContextMenu.ForeColor = System.Drawing.Color.Black;
                changeColorContextMenu.ForeColor = System.Drawing.Color.Black;
                changeFontContextMenu.ForeColor = System.Drawing.Color.Black;

                cutContextMenu.BackColor = Color.White;
                copyContextMenu.BackColor = Color.White;
                pasteContextMenu.BackColor = Color.White;
                selectAllContextMenu.BackColor = Color.White;
                formatContextMenu.BackColor = Color.White;
                changeColorContextMenu.BackColor = Color.White;
                changeFontContextMenu.BackColor = Color.White;
            }

            cutContextMenu.Size = new System.Drawing.Size(180, 22);
            copyContextMenu.Size = new System.Drawing.Size(180, 22);
            pasteContextMenu.Size = new System.Drawing.Size(180, 22);
            selectAllContextMenu.Size = new System.Drawing.Size(180, 22);
            changeColorContextMenu.Size = new System.Drawing.Size(180, 22);
            changeFontContextMenu.Size = new System.Drawing.Size(180, 22);

            cutContextMenu.Text = "Cut";
            copyContextMenu.Text = "Copy";
            pasteContextMenu.Text = "Paste";
            selectAllContextMenu.Text = "Select All";
            changeColorContextMenu.Text = "Color";
            changeFontContextMenu.Text = "Font";

            cutContextMenu.Click += new System.EventHandler(Cut_ContextMenuClick);
            copyContextMenu.Click += new System.EventHandler(Copy_ContextMenuClick);
            pasteContextMenu.Click += new System.EventHandler(Paste_ContextMenuClick);
            selectAllContextMenu.Click += new System.EventHandler(SelectAll_ContextMenuClick);
            changeColorContextMenu.Click += new System.EventHandler(Color_ContextMenuClick);
            changeFontContextMenu.Click += new System.EventHandler(Font_ContextMenuClick);

            rtBox.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Установка цветофого оформления темной темы всем элементам формы.
        /// </summary>
        private void SetDarkMenuStrip()
        {
            menuStrip.BackColor = Color.FromArgb(41, 43, 53);
            menuStrip.ForeColor = Color.FromArgb(170, 180, 190);

            fileMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileMenuItem.ForeColor = Color.FromArgb(170, 180, 190);
            editMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editMenuItem.ForeColor = Color.FromArgb(170, 180, 190);
            formatMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            formatMenuItem.ForeColor = Color.FromArgb(170, 180, 190);
            settingsMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            settingsMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileNewMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileNewMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileNewWindowMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileNewWindowMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileOpenMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileOpenMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileSaveMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileSaveMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileSaveAsMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileSaveAsMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileSaveAllMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileSaveAllMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileRenameMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileRenameMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileCloseMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileCloseMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileCloseAllMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileCloseAllMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileExitMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileExitMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            editUndoMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editUndoMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            editRedoMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editRedoMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            editCutMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editCutMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            editCopyMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editCopyMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            editPasteMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editPasteMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            editSelectAllMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            editSelectAllMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            formatColorMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            formatColorMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            formatFontMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            formatFontMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            formattingCodeMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            formattingCodeMenuItem.ForeColor = Color.FromArgb(170, 180, 190);

            fileNewCSharpWindowMenuItem.BackColor = Color.FromArgb(41, 43, 53);
            fileNewCSharpWindowMenuItem.ForeColor = Color.FromArgb(170, 180, 190);
        }

        /// <summary>
        /// Установка цветофого оформления светлой темы всем элементам формы.
        /// </summary>
        private void SetLightMenuStrip()
        {
            menuStrip.BackColor = Color.White;
            menuStrip.ForeColor = Color.FromArgb(170, 180, 190);

            fileMenuItem.BackColor = Color.White;
            fileMenuItem.ForeColor = Color.Black;
            editMenuItem.BackColor = Color.White;
            editMenuItem.ForeColor = Color.Black;
            formatMenuItem.BackColor = Color.White;
            formatMenuItem.ForeColor = Color.Black;
            settingsMenuItem.BackColor = Color.White;
            settingsMenuItem.ForeColor = Color.Black;

            fileNewMenuItem.BackColor = Color.White;
            fileNewMenuItem.ForeColor = Color.Black;

            fileNewWindowMenuItem.BackColor = Color.White;
            fileNewWindowMenuItem.ForeColor = Color.Black;

            fileOpenMenuItem.BackColor = Color.White;
            fileOpenMenuItem.ForeColor = Color.Black;

            fileSaveMenuItem.BackColor = Color.White;
            fileSaveMenuItem.ForeColor = Color.Black;

            fileSaveAsMenuItem.BackColor = Color.White;
            fileSaveAsMenuItem.ForeColor = Color.Black;

            fileSaveAllMenuItem.BackColor = Color.White;
            fileSaveAllMenuItem.ForeColor = Color.Black;

            fileRenameMenuItem.BackColor = Color.White;
            fileRenameMenuItem.ForeColor = Color.Black;

            fileCloseMenuItem.BackColor = Color.White;
            fileCloseMenuItem.ForeColor = Color.Black;

            fileCloseAllMenuItem.BackColor = Color.White;
            fileCloseAllMenuItem.ForeColor = Color.Black;

            fileExitMenuItem.BackColor = Color.White;
            fileExitMenuItem.ForeColor = Color.Black;

            editUndoMenuItem.BackColor = Color.White;
            editUndoMenuItem.ForeColor = Color.Black;

            editRedoMenuItem.BackColor = Color.White;
            editRedoMenuItem.ForeColor = Color.Black;

            editCutMenuItem.BackColor = Color.White;
            editCutMenuItem.ForeColor = Color.Black;

            editCopyMenuItem.BackColor = Color.White;
            editCopyMenuItem.ForeColor = Color.Black;

            editPasteMenuItem.BackColor = Color.White;
            editPasteMenuItem.ForeColor = Color.Black;

            editSelectAllMenuItem.BackColor = Color.White;
            editSelectAllMenuItem.ForeColor = Color.Black;

            formatColorMenuItem.BackColor = Color.White;
            formatColorMenuItem.ForeColor = Color.Black;

            formatFontMenuItem.BackColor = Color.White;
            formatFontMenuItem.ForeColor = Color.Black;

            formattingCodeMenuItem.BackColor = Color.White;
            formattingCodeMenuItem.ForeColor = Color.Black;

            fileNewCSharpWindowMenuItem.BackColor = Color.White;
            fileNewCSharpWindowMenuItem.ForeColor = Color.Black;
        }
    }
}
