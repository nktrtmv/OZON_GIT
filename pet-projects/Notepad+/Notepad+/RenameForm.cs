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
    /// Форма для переименования файла.
    /// </summary>
    internal class RenameForm : Form
    {
        private Label label;
        private TextBox nameBox;
        private Button buttonOk;
        private Button buttonCancel;

#pragma warning disable CS8618 
#pragma warning disable CS8618 
#pragma warning disable CS8618 
#pragma warning disable CS8618 
        /// <summary>
        /// Конструктор, инициализирует форму и нужные переменные.
        /// </summary>
        public RenameForm()
#pragma warning restore CS8618 
#pragma warning restore CS8618 
#pragma warning restore CS8618 
#pragma warning restore CS8618 
        {
            InitializeComponent();
            NewName = "";
            BadSymbols = "\\/:*?\"<>|";
            IsOkClicked = false;
        }

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label.Location = new System.Drawing.Point(2, 27);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(69, 15);
            this.label.TabIndex = 0;
            this.label.Text = "New Name:";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(88, 24);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(215, 23);
            this.nameBox.TabIndex = 1;
            // 
            // buttonOk
            // 
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(63, 69);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(20);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(178, 69);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(20);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // RenameForm
            // 
            this.ClientSize = new System.Drawing.Size(315, 104);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label);
            this.MaximumSize = new System.Drawing.Size(331, 143);
            this.MinimumSize = new System.Drawing.Size(331, 143);
            this.Name = "RenameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename Current Tab";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Новое имя файла, которое ввел пользователь.
        /// </summary>
        public string NewName { get; private set; }

        /// <summary>
        /// Переменная показывающая изменено ли имя, т.е. нажал пользователь ок или нет.
        /// </summary>
        public bool IsOkClicked { get; private set; }

        /// <summary>
        /// Список недопустимых символов в названии файла.
        /// </summary>
        public string BadSymbols { get; private set; }

        /// <summary>
        /// Обработчик нажатия на клавишу ок - если имя корректно, оно записывается, иначе запиывается ошибочный символ.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (isValidSymbols(nameBox.Text))
            {
                NewName = nameBox.Text;
            }
            else
            {
                NewName = "?";
            }
            IsOkClicked = true;
            Close();
        }

        /// <summary>
        /// Если пользователь решил не изменять имя файла, форма закрывается.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Проверка все ли символы допустимые для названия файла.
        /// </summary>
        /// <param name="fileName">Имя файла для проверки. </param>
        /// <returns>Если файл корректен - true, иначе - false.</returns>
        private bool isValidSymbols(string fileName)
        {
            bool flag = true;
            foreach (var sym in BadSymbols)
            {
                if (fileName.Contains(sym))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
}
