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
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;


namespace Notepad_
{
    /// <summary>
    /// Форма блокнота, основное окно приложения Notepad+.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Статический конструктор, устанавливает тему и интервал автосохранения при первом открытии.
        /// </summary>
        static MainForm()
        {
            if (string.IsNullOrEmpty(CurrentTheme))
                CurrentTheme = "dark";
            IntervalForSave = 60000;
            _isFirstOpening = true;
        }

        /// <summary>
        /// Конструктор. Инициализирует форму и важные элементы класса
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            fontDialog = new FontDialog();
            colorDialog = new ColorDialog();
            tabPages = new List<TabPage>();
            richTextBoxes = new List<RichTextBox>();
            currentFile = new List<string>();
            isSaved = new List<bool>();
            count = 1;
            Program.Forms.Add(this);

            tabControl = new TabControl
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Location = new Point(-1, 27),
                Margin = new Padding(0),
                Name = "tabControl",
                SelectedIndex = 0,
                Size = new Size(797, 460),
                TabIndex = 0
            };
            tabControl.SuspendLayout();
            tabControl.ResumeLayout(false);
            Controls.Add(tabControl);
            if (_isFirstOpening) 
            {
                IntervalForSave = 60000;
                OpenStartFiles();
                SetStartThemeAndInterval();
                UpdateAllForNewTheme();
                _isFirstOpening=false;
            }
            if (tabPages.Count == 0)
                CreateNewTab();
            SetTheme(tabControl.SelectedIndex);
            timerAutoSave.Interval = IntervalForSave;
        }

        // Dialogs
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private FontDialog fontDialog;
        private ColorDialog colorDialog;

        // Lists
        private List<TabPage> tabPages;
        private List<RichTextBox> richTextBoxes;
        private List<string> currentFile;
        private List<bool> isSaved;

        // Other
        private int count;
        /// <summary>
        /// Компонент отвечающий за вкладки.
        /// </summary>
        internal TabControl tabControl;
        private static bool _isFirstOpening;

        // Properties
        /// <summary>
        /// Текущая страница tabComtrol.
        /// </summary>
        private TabPage tabPage
        {
            get {  return tabControl.SelectedTab; }
        }
        /// <summary>
        /// Текущая тема.
        /// </summary>
        public static string CurrentTheme { get; set; }
        /// <summary>
        /// Интервал автосохранения.
        /// </summary>
        public static int IntervalForSave { get; set; }



        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //      Event handlers methods 
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //      Event handlers methods for menu item "File"
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Обработчик события - создание новой вкладки.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileNewMenuItem_Click(object sender, EventArgs e) => CreateNewTab();

        /// <summary>
        /// Обработчик события - создание нового окна.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileNewWindowMenuItem_Click(object sender, EventArgs e) => new MainForm().Show();

        /// <summary>
        /// Обработчик события - открытие файла.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ResetLists(tabControl.TabCount == 0);
                if (!(tabPages.Count == 1 && tabPages[0].Text.Contains("Untitled")))
                    CreateNewTab();
                var pageInd = tabControl.SelectedIndex;
                var rtBox = richTextBoxes[pageInd];
                var regRTF = new Regex(@"^.*\.rtf$");
                openFileDialog = new OpenFileDialog
                {
                    Filter = "Text file/C# source file|*.txt;*.rtf;*.cs|All files|*.*"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (regRTF.IsMatch(openFileDialog.FileName))
                    {
                        rtBox.LoadFile(openFileDialog.FileName);
                        tabPage.Text = new FileInfo(openFileDialog.FileName).Name;
                    }
                    else
                    {
                        var sr = new StreamReader(openFileDialog.FileName);
                        rtBox.Text = sr.ReadToEnd();
                        sr.Close();
                        tabPage.Text = new FileInfo(openFileDialog.FileName).Name;
                    }
                    currentFile[pageInd] = openFileDialog.FileName;
                    isSaved[pageInd] = true;
                    SetTheme(pageInd);
                }
                else
                {
                    if (tabControl.TabCount > 1)
                        ClosePage(isNeedSave: false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик события - сохранение файла
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileSaveMenuItem_Click(object sender, EventArgs e) => Save();

        /// <summary>
        /// Обработчик события - сохранение файла в проводнике (Сохранить как).
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileSaveAsMenuItem_Click(object sender, EventArgs e) => SaveAs();

        /// <summary>
        /// Обработчик события - Сохранение всех открытых файлов.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileSaveAllMenuItem_Click(object sender, EventArgs e)
        {
            ResetLists(tabControl.TabCount == 0);
            foreach (var page in tabPages)
            {
                tabControl.SelectedTab = page;
                if (IsEmptyTab())
                {
                    DialogResult dg = MessageBox.Show("Do you want to create and save empty file? ",
                        "Save Empty File " + tabPages[tabControl.SelectedIndex].Text + "?", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.Yes)
                    {
                        Save();
                    }
                    else
                    {
                        continue;
                    }
                }
                Save();
            }
        }

        /// <summary>
        /// Обработчик события - переименование файла.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileRenameMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
                var reg = new Regex(@"^Untitled\d*$");
                var pageInd = tabControl.SelectedIndex;
                if (reg.IsMatch(tabPage.Text) && isSaved[pageInd] == false && string.IsNullOrEmpty(currentFile[pageInd]))
                {
                    MessageBox.Show("You need to save file before renaming.");
                }
                else
                {
                    var renameForm = new RenameForm();
                    renameForm.ShowDialog();
                    if (renameForm.IsOkClicked == true)
                    {
                        var name = renameForm.NewName;
                        if (name.Contains("?"))
                        {
                            MessageBox.Show("File name can't contains next symbols: \\ / : * ? \" < > |");
                            Show();
                            return;
                        }
                        var ext = Path.GetExtension(currentFile[pageInd]);
                        var lastInd = currentFile[pageInd].LastIndexOf("\\") + 1;
                        var newFile = currentFile[pageInd][..lastInd] + name + ext;
                        name += ext;
                        tabPage.Text = name;
                        Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(currentFile[pageInd], name);
                        currentFile[pageInd] = newFile;

                    }
                }
                Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Show();
            }
        }

        /// <summary>
        /// Обработчик события - закрытие вкладки.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileCloseMenuItem_Click(object sender, EventArgs e) => ClosePage();

        /// <summary>
        /// Обработчик события - закрытие всех вкладок.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileCloseAllMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.Visible = false;
            var limit = tabPages.Count;
            for (var i = 0; i < limit; i++)
            {
                ClosePage();
            }
        }

        /// <summary>
        /// Обработчик события - выход и закрытие программы.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Do you want to exit Notepad+?",
                "Exit Notepad+?", MessageBoxButtons.YesNo);
            if (dg == DialogResult.Yes)
            {
                var sw = new StreamWriter("files on close.txt");
                foreach (var file in currentFile)
                {
                    if (!string.IsNullOrEmpty(file))
                        sw.WriteLine(file);
                }
                sw.Close();
                sw = new StreamWriter("Interval and Theme.txt");
                sw.WriteLine(IntervalForSave.ToString() + "<?>" + CurrentTheme);
                sw.Close();

                FileCloseAllMenuItem_Click(sender, e);
                Close();
            }
        }

        /// <summary>
        /// Обработчик события - создание формы для работы с кодом C#.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileNewCSharpWindowMenuItem_Click(object sender, EventArgs e) => new CSharpForm().Show();

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //      Event handlers methods for menu item "Edit"
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Обработчик события - функция отмены действия.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditUndoMenuItem_Click(object sender, EventArgs e) => richTextBoxes[tabControl.SelectedIndex].Undo();

        /// <summary>
        /// Обработчик события - возврат отмененного действия.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditRedoMenuItem_Click(object sender, EventArgs e) => richTextBoxes[tabControl.SelectedIndex].Redo();

        /// <summary>
        /// Обработчик события - вырезать текст.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditCutMenuItem_Click(object sender, EventArgs e) => richTextBoxes[tabControl.SelectedIndex].Cut();

        /// <summary>
        /// Обработчик события - копировать текст.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditCopyMenuItem_Click(object sender, EventArgs e) => richTextBoxes[tabControl.SelectedIndex].Copy();

        /// <summary>
        /// Обработчик события - вставить текст.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditPasteMenuItem_Click(object sender, EventArgs e) => richTextBoxes[tabControl.SelectedIndex].Paste();

        /// <summary>
        /// Обработчик события - выбрать весь текст.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditSelectAllMenuItem_Click(object sender, EventArgs e) => richTextBoxes[tabControl.SelectedIndex].SelectAll();

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //      Event handlers methods for menu items "Format" and "Settings"
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Обработчик события - изменение шрифта.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormatFontMenuItem_Click(object sender, EventArgs e)
        {
            var rtBox = richTextBoxes[tabControl.SelectedIndex];
            fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                rtBox.Font = fontDialog.Font;
            }
        }

        /// <summary>
        /// Обработчик события - измнение цвета текста.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormatColorMenuItem_Click(object sender, EventArgs e)
        {
            var rtBox = richTextBoxes[tabControl.SelectedIndex];
            colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                rtBox.ForeColor = colorDialog.Color;
            }
        }

        /// <summary>
        /// Обработчик события - форматирование кода c#.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormattingCodeMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.TabCount > 0 && currentFile[tabControl.SelectedIndex] != String.Empty && Path.GetExtension(currentFile[tabControl.SelectedIndex]) == ".cs")
            {
                var rtBox = richTextBoxes[tabControl.SelectedIndex];
                rtBox.Text = SetFormatForCs(rtBox.Text);
            }
        }

        /// <summary>
        /// Обработчик события - открытие настроек.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new SettingsForm();
            settings.ShowDialog(); 
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //      Event handlers for context menu in RichTextBox
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Обработчик события - вырезать выделенный текст.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void Cut_ContextMenuClick(object sender, EventArgs e) => EditCutMenuItem_Click(sender, e);

        /// <summary>
        /// Обработчик события - копирование текста.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void Copy_ContextMenuClick(object sender, EventArgs e) => EditCopyMenuItem_Click(sender, e);

        /// <summary>
        /// Обработчик события - вставка текста.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void Paste_ContextMenuClick(object sender, EventArgs e) => EditPasteMenuItem_Click(sender, e);

        /// <summary>
        /// Обработчик события - выбрать весь текст.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SelectAll_ContextMenuClick(object sender, EventArgs e) => EditSelectAllMenuItem_Click(sender, e);

        /// <summary>
        /// Обработчик события - изменить шрифт выделенного текста.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void Font_ContextMenuClick(object sender, EventArgs e)
        {
            var rtBox = richTextBoxes[tabControl.SelectedIndex];
            fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                rtBox.SelectionFont = fontDialog.Font;
            }
        }

        /// <summary>
        /// Обработчик события - выбрать цвет выделенного текста.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void Color_ContextMenuClick(object sender, EventArgs e)
        {
            var rtBox = richTextBoxes[tabControl.SelectedIndex];
            colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                rtBox.SelectionColor = colorDialog.Color;
            }
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //      Other event handlers methods 
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Обработчик события - ограничение на элементы fileMenuStrip.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (!IsSavedFilesExists() && tabControl.TabCount > 0)
            {
                fileNewMenuItem.Enabled = true;
                fileOpenMenuItem.Enabled = true;
                fileSaveMenuItem.Enabled = false;
                fileSaveAsMenuItem.Enabled = true;
                fileSaveAllMenuItem.Enabled = false;
                fileRenameMenuItem.Enabled = false;
                fileCloseMenuItem.Enabled = true;
                fileCloseAllMenuItem.Enabled = true;
                fileExitMenuItem.Enabled = true;
                return;
            }
            else if (tabControl.TabCount > 0)
            {
                fileNewMenuItem.Enabled = true;
                fileOpenMenuItem.Enabled = true;
                fileSaveMenuItem.Enabled = true;
                fileSaveAsMenuItem.Enabled = true;
                fileSaveAllMenuItem.Enabled = true;
                fileRenameMenuItem.Enabled = true;
                fileCloseMenuItem.Enabled = true;
                fileCloseAllMenuItem.Enabled = true;
                fileExitMenuItem.Enabled = true;
            }
            else
            {
                fileNewMenuItem.Enabled = true;
                fileOpenMenuItem.Enabled = true;
                fileSaveMenuItem.Enabled = false;
                fileSaveAsMenuItem.Enabled = false;
                fileSaveAllMenuItem.Enabled = false;
                fileRenameMenuItem.Enabled = false;
                fileCloseMenuItem.Enabled = false;
                fileCloseAllMenuItem.Enabled = false;
                fileExitMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Обработчик события - ограничение на элементы editMenuStrip.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EditMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (tabControl.TabCount > 0)
            {
                editUndoMenuItem.Enabled = true;
                editRedoMenuItem.Enabled = true;
                editCutMenuItem.Enabled = true;
                editCopyMenuItem.Enabled = true;
                editPasteMenuItem.Enabled = true;
                editSelectAllMenuItem.Enabled = true;
            }
            else
            {
                editUndoMenuItem.Enabled = false;
                editRedoMenuItem.Enabled = false;
                editCutMenuItem.Enabled = false;
                editCopyMenuItem.Enabled = false;
                editPasteMenuItem.Enabled = false;
                editSelectAllMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Обработчик события  ограничение на элементы formatMenuStrip.-
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormatMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (tabControl.TabCount > 0)
            {
                formatColorMenuItem.Enabled = true;
                formatFontMenuItem.Enabled = true;
                if (currentFile[tabControl.SelectedIndex] != String.Empty && Path.GetExtension(currentFile[tabControl.SelectedIndex]) == ".cs")
                {
                    formattingCodeMenuItem.Enabled = true;
                }
                else
                {
                    formattingCodeMenuItem.Enabled = false;
                }
            }
            else
            {
                formatColorMenuItem.Enabled = false;
                formatFontMenuItem.Enabled = false;
                formattingCodeMenuItem.Enabled= false;
            }
        }

        /// <summary>
        /// Обработчик события - таймер, сохраняющий открытые файлы.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TimerAutoSave_Tick(object sender, EventArgs e)
        {
            foreach (var page in tabPages)
            {
                if (!string.IsNullOrEmpty(currentFile[tabPages.IndexOf(page)]))
                {
                    try
                    {
                        ResetLists(tabControl.TabCount == 0);
                        var reg = new Regex(@"^Untitled\d*$");
                        var pageInd = tabPages.IndexOf(page);
                        var rtBox = richTextBoxes[pageInd];

                        var sw = new StreamWriter(currentFile[pageInd]);
                        if (Path.GetExtension(currentFile[pageInd]) == ".rtf")
                            sw.WriteLine(rtBox.Rtf);
                        else
                            sw.WriteLine(rtBox.Text);
                        sw.Close();
                        isSaved[pageInd] = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик события - закрытие формы
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sw = new StreamWriter("files on close.txt");
            foreach (var file in currentFile)
            {
                 sw.WriteLine(file);
            }
            sw.Close();
            sw = new StreamWriter("Interval and Theme.txt");
            sw.WriteLine(IntervalForSave.ToString() + "<?>" + CurrentTheme + "<?>" + CSharpForm.Compiler);
            sw.Close();

            FileCloseAllMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Обработчик события - изменения текста RichTextBox.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void Text_Changed(object sender, EventArgs e)
        {
            isSaved[tabControl.SelectedIndex] = false;
        }

// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//      Auxiliary methods and methods with different logic
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Сбросить списки класса, если все вкладки закрыты.
        /// </summary>
        /// <param name="isReset"></param>
        private void ResetLists(bool isReset)
        {
            if (isReset)
            {
                tabPages = new List<TabPage>();
                richTextBoxes = new List<RichTextBox>();
                currentFile = new List<string>();
                isSaved = new List<bool>();
                count = 1;
            }
        }

        /// <summary>
        /// Создание новой вкладки/страницы tabControl.
        /// </summary>
        private void CreateNewTab()
        {
            ResetLists(tabControl.TabCount == 0);
            var newTabPage = new TabPage
            {
                Text = $"Untitled{count}",
                Location = new System.Drawing.Point(4, 24),
                Padding = new System.Windows.Forms.Padding(0),
                Size = new System.Drawing.Size(789, 432),
                TabIndex = count - 1,
                UseVisualStyleBackColor = true,
                Dock = System.Windows.Forms.DockStyle.Top
            };
            newTabPage.SuspendLayout();
            newTabPage.ResumeLayout(false);

            var rtBox = new RichTextBox
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new System.Drawing.Point(3, 3),
                Size = new System.Drawing.Size(783, 426),
                TabIndex = count - 1,
                Text = ""
            };

            rtBox.TextChanged += Text_Changed;
            rtBox.AcceptsTab = true;
            CreateContextMenu(rtBox);
            newTabPage.Controls.Add(rtBox);
            richTextBoxes.Add(rtBox);
            tabPages.Add(newTabPage);
            count++;
            currentFile.Add(String.Empty);
            isSaved.Add(false);
            tabControl.TabPages.Add(tabPages[^1]);
            if (tabControl.TabCount == 1)
                tabControl.Visible = true;
            tabControl.SelectTab(newTabPage);
            FileMenuItem_DropDownOpening(this, new EventArgs());
            SetTheme(tabControl.SelectedIndex);
            if (CurrentTheme == "dark")
                rtBox.ForeColor = Color.FromArgb(170, 180, 190);
        }

        /// <summary>
        /// Открытие файлов, которые были открыты в предыдущую сессию.
        /// </summary>
        private void OpenStartFiles()
        {
            try
            {
                var files = File.ReadAllLines("files on close.txt");
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        if (File.Exists(file) && file.Length > 2)
                        {
                            CreateNewTab();
                            var pageInd = tabControl.SelectedIndex;
                            var rtBox = richTextBoxes[pageInd];
                            var regRTF = new Regex(@"^.*\.rtf$");

                            if (regRTF.IsMatch(file))
                            {
                                rtBox.LoadFile(file);
                                tabPage.Text = new FileInfo(file).Name;
                            }
                            else
                            {
                                var sr = new StreamReader(file);
                                rtBox.Text = sr.ReadToEnd();
                                sr.Close();
                                tabPage.Text = new FileInfo(file).Name;
                            }
                            currentFile[pageInd] = file;
                            isSaved[pageInd] = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Установка темы и интервала автосозранения предыдущей сессии.
        /// </summary>
        private void SetStartThemeAndInterval()
        {
            if (File.Exists("Interval and Theme.txt"))
            {
                var sr = new StreamReader("Interval and Theme.txt");
                var intervalAndTheme = sr.ReadLine().Split("<?>");
                IntervalForSave = int.Parse(intervalAndTheme[0]);
                CurrentTheme = intervalAndTheme[1];
                sr.Close();
            }
        }

        /// <summary>
        /// Сохранение файла.
        /// </summary>
        private void Save()
        {
            try
            {
                ResetLists(tabControl.TabCount == 0);
                var reg = new Regex(@"^Untitled\d*$");
                var pageInd = tabControl.SelectedIndex;
                var rtBox = richTextBoxes[pageInd];

                if (IsEmptyTab())
                {
                    
                }
                else
                {
                    var sw = new StreamWriter(currentFile[pageInd]);
                    if (Path.GetExtension(currentFile[pageInd]) == ".rtf")
                        sw.WriteLine(rtBox.Rtf);
                    else
                        sw.WriteLine(rtBox.Text);
                    sw.Close();
                    isSaved[pageInd] = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// "Сохранить файл Как".
        /// </summary>
        private void SaveAs()
        {
            try
            {
                ResetLists(tabControl.TabCount == 0);
                var pageInd = tabControl.SelectedIndex;
                var rtBox = richTextBoxes[pageInd];
                saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = ".txt",
                    Filter = "Text file|*.txt|RTF file|*.rtf|C# source file|*.cs|All files|*.*"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var sw = new StreamWriter(saveFileDialog.FileName);
                    if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                        sw.WriteLine(rtBox.Rtf);
                    else
                        sw.WriteLine(rtBox.Text);
                    sw.Close();
                    tabPage.Text = new FileInfo(saveFileDialog.FileName).Name;
                    currentFile[pageInd] = saveFileDialog.FileName;
                    isSaved[pageInd] = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Закрытие страницы tabpage.
        /// </summary>
        /// <param name="isCurrentClose"></param>
        /// <param name="isNeedSave"></param>
        private void ClosePage(bool isCurrentClose = true, bool isNeedSave = true)
        {
            var pageInd = tabControl.SelectedIndex;
            if (!isCurrentClose)
                pageInd = 0;

            if (IsEmptyTab()) { }
            else if (isSaved[pageInd] == false && isNeedSave == true)
            {
                DialogResult dg = MessageBox.Show("Do you want to save " + tabPages[pageInd].Text + " file before close?",
                "Save before Close ?", MessageBoxButtons.YesNo);
                if (dg == DialogResult.Yes)
                {
                    Save();
                }
            }
            tabPages[pageInd].Dispose();
            currentFile.RemoveAt(pageInd);
            richTextBoxes.RemoveAt(pageInd);
            tabPages.RemoveAt(pageInd);
            isSaved.RemoveAt(pageInd);
            FileMenuItem_DropDownOpening(this, new EventArgs());
            EditMenuItem_DropDownOpening(this,new EventArgs());
            if (tabPages.Count > 0)
                SetTheme(tabControl.SelectedIndex);
            if (tabControl.TabCount == 0)
                tabControl.Visible = false;
        }

        /// <summary>
        /// Проверка существуют ли файлы для сохранения.
        /// </summary>
        /// <returns></returns>
        private bool IsSavedFilesExists()
        {
            foreach (var file in currentFile)
            {
                if (file.Length > 3)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка пустая ли вкладка (только созданная или нет).
        /// </summary>
        /// <returns></returns>
        private bool IsEmptyTab()
        {
            var rtBox = richTextBoxes[tabControl.SelectedIndex];
            var reg = new Regex(@"^Untitled\d*$");
            return string.IsNullOrEmpty(rtBox.Text) && reg.IsMatch(tabPage.Text)
                && string.IsNullOrEmpty(currentFile[tabControl.SelectedIndex]);
        }

        /// <summary>
        /// Изменить тему во всех вкладках.
        /// </summary>
        public void UpdateAllForNewTheme()
        {
            for (var i = 0; i < tabPages.Count; i++)
            {
                SetTheme(i);
            }
        }

        /// <summary>
        /// Изменить тему вкладки.
        /// </summary>
        /// <param name="ind">Индекс вкладки в списке вкладок.</param>
        private void SetTheme(int ind)
        {
            switch (CurrentTheme)
            {
                case "dark":
                    SetDarkTheme(ind);
                    break;
                case "light":
                    SetLightTheme(ind);
                    break;
                case "hse":
                    break;
            }
        }

        /// <summary>
        /// Установка вкладке темной темы.
        /// </summary>
        /// <param name="pageInd">Индекс вкладки в списке вкладок.</param>
        private void SetDarkTheme(int pageInd)
        {
            var rtBox = richTextBoxes[pageInd];
            var tabPage = tabPages[pageInd];
            var contextMenu = rtBox.ContextMenuStrip;

            rtBox.BackColor = Color.FromArgb(41, 43, 53);
            rtBox.ForeColor = Color.FromArgb(170, 180, 190);
            tabPage.BackColor = Color.FromArgb(41, 43, 53);
            tabPage.ForeColor = Color.FromArgb(170, 180, 190);

            contextMenu.BackColor = Color.FromArgb(41, 43, 53);
            contextMenu.ForeColor = Color.FromArgb(170, 180, 190);
            contextMenu.Items.Clear();
            CreateContextMenu(rtBox);

            SetDarkMenuStrip();

            this.BackColor = Color.FromArgb(44, 46, 55);
            this.ForeColor = Color.FromArgb(170, 180, 190);
        }

        /// <summary>
        /// Установка вкладке светлой теме.
        /// </summary>
        /// <param name="pageInd">Индекс вкладки в списке вкладок.</param>
        private void SetLightTheme(int pageInd)
        {
            var rtBox = richTextBoxes[pageInd];
            var tabPage = tabPages[pageInd];
            var contextMenu = rtBox.ContextMenuStrip;

            rtBox.BackColor = Color.White;
            rtBox.ForeColor= Color.Black;
            tabPage.BackColor = Color.White;
            tabPage.ForeColor = Color.Black;

            contextMenu.BackColor = Color.White;
            contextMenu.ForeColor = Color.Black;
            contextMenu.Items.Clear();
            CreateContextMenu(rtBox);

            SetLightMenuStrip();

            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
        }

        /// <summary>
        /// Форматирование кода c#.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string SetFormatForCs(string code) => CSharpSyntaxTree.ParseText(code).GetRoot().NormalizeWhitespace().ToFullString();
    }
}