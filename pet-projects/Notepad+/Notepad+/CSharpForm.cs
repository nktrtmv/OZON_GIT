using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Notepad_
{
    /// <summary>
    /// Форма для основной работы с кодом C#.
    /// </summary>
    public partial class CSharpForm : Form
    {
        private List<string> lines = new List<string>();
        private OpenFileDialog? openFileDialog;
        private SaveFileDialog? saveFileDialog;
        private bool isSaved;
        private string file;

        public static string Compiler;
        /// <summary>
        /// инициализация формы и присваивание значений переменным. Конструктор.
        /// </summary>
        public CSharpForm()
        {
            InitializeComponent();
            file = string.Empty;
        }

        /// <summary>
        /// Обработчик изменения текста в TextBox.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = textBox.Text;
            var lines = text.Split("\n");
            labelCountOfSymbols.Text = "Символов: " + text.Length.ToString();
            labelCountOfStrings.Text = "Строк: " + lines.Length.ToString();
            isSaved = false;
        }

        /// <summary>
        /// Обработчик события - открытия файла.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog = new OpenFileDialog
                {
                    Filter = "C# source file|*.cs"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var sr = new StreamReader(openFileDialog.FileName);
                    textBox.Text = sr.ReadToEnd();
                    sr.Close();
                    file = openFileDialog.FileName;
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик события - сохранения файла.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) => Save();
        
        /// <summary>
        /// Сохранение существующего файла.
        /// </summary>
        private void Save()
        {
            try
            {
                var sw = new StreamWriter(file);
                sw.WriteLine(textBox.Text);
                sw.Close();
                isSaved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик события - Сохранить как.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = ".cs",
                    Filter = "C# source file|*.cs"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var sw = new StreamWriter(saveFileDialog.FileName);
                    sw.WriteLine(textBox.Text);
                    sw.Close();
                    file = saveFileDialog.FileName;
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик события открытия вкладки File, проверка можно ли сохранять файлы и каким образом.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (file == string.Empty)
            {
                saveToolStripMenuItem.Enabled = false;
                openToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
            else if (file != string.Empty && isSaved)
            {
                saveToolStripMenuItem.Enabled = false;
                openToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
            else if (file != string.Empty && !isSaved)
            {
                saveToolStripMenuItem.Enabled = true;
                openToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Обработчик события - форматирование кода.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void formattingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = MainForm.SetFormatForCs(textBox.Text);
        }

        /// <summary>
        /// Обработчик события - компиляция программы (кода).
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (!string.IsNullOrEmpty(file))
                {
                    if (!isSaved)
                        saveToolStripMenuItem_Click(sender, e);
                    CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                    ICodeCompiler icc = codeProvider.CreateCompiler();
                    string Output = "Out.exe";

                    System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();

                    parameters.GenerateExecutable = true;
                    parameters.OutputAssembly = Output;
                    CompilerResults results = icc.CompileAssemblyFromSource(parameters, textBox.Text);

                    if (results.Errors.Count > 0)
                    {
                        textBox.ForeColor = Color.Red;
                        foreach (CompilerError CompErr in results.Errors)
                        {
                            textBox.Text = textBox.Text +
                                        "Line number " + CompErr.Line +
                                        ", Error Number: " + CompErr.ErrorNumber +
                                        ", '" + CompErr.ErrorText + ";" +
                                        Environment.NewLine + Environment.NewLine;
                        }
                    }
                    else
                    {
                        Process.Start(Output);
                    }
                }
                else
                {
                    return;
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private string OpenCmdAndExecuteCommand(string compil, string source)
        {
            string command = $"\"{compil}\" /t:exe \"{source}\"";

            Process cmd = new Process();
            cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");

            cmd.StartInfo.CreateNoWindow = true; 
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            cmd.StartInfo.StandardInputEncoding = Encoding.GetEncoding(866);
            cmd.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.WriteLine("exit");

            StreamReader srIncoming = cmd.StandardOutput;
            string value = srIncoming.ReadToEnd();
            return value;
        }

        private static void PrintCommandExecutionData(string value, string source)
        {
            if (value.Contains("error CS"))
            {
                string info = null;
                string[] datas = value.Split("\n");
                for (int i = 0; i < datas.Length; i++)
                {
                    if (datas[i].Contains("error CS"))
                    {
                        info += $"{datas[i]}\n";
                    }
                }

                MessageBox.Show($"Ошибки:\n{info}", "Ошибка при компиляции!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!File.Exists($@"{Path.GetFileNameWithoutExtension(source)}.exe"))
            {
                MessageBox.Show($"После отправки на компиляцию не было создано выходного файла.\n" +
                                $"Вероятно, вы указали csc.exe не связанный с технологией Microsoft .NET!\n" +
                                $"Или произошла неизвестная ошибка!\n", "Провал!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show($"Программа успешно скомпилирована.\n" +
                                $"Исполняемый файл с именем файла можно найти в корневой папке приложения!\n" +
                                $"Путь: {Path.GetFullPath("./" + Path.GetFileNameWithoutExtension(source) + ".exe")}",
                    "Успех!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool StartCompilationWork()
        {
            if (!string.IsNullOrEmpty(file))
            {
                Save();
                string compil = null;
                var data = File.ReadAllText("Interval and Theme.txt").Split("<?>");
                if (data.Length > 0)
                {
                    compil = data[2];
                    OpenCmdAndExecuteCommand(compil,textBox.Text);
                    //PrintCommandExecutionData(value, textBox.Text);
                }
            }
            return false;
        }


    }
}
