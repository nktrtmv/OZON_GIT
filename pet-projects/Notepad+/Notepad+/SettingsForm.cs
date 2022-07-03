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
    /// Класс, отвечающий за винформу с настройками приложения.
    /// </summary>
    internal class SettingsForm : Form
    {
        private RadioButton? lightTheme;
        private RadioButton? darkTheme;
        private Label? label1;
        private Label? label2;
        private TrackBar? trackBar;

        /// <summary>
        /// Конструктор - устанаваливается выбор radioButton на текущую тему, происходит инициализация.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            if (MainForm.CurrentTheme == "dark")
            {
                darkTheme.Checked = true;

            }
            else
            {
                lightTheme.Checked = true;
            }
        }

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        private void InitializeComponent()
        {
            this.lightTheme = new System.Windows.Forms.RadioButton();
            this.darkTheme = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // lightTheme
            // 
            this.lightTheme.AutoSize = true;
            this.lightTheme.Location = new System.Drawing.Point(30, 164);
            this.lightTheme.Name = "lightTheme";
            this.lightTheme.Size = new System.Drawing.Size(89, 19);
            this.lightTheme.TabIndex = 0;
            this.lightTheme.TabStop = true;
            this.lightTheme.Text = "Light theme";
            this.lightTheme.UseVisualStyleBackColor = true;
            this.lightTheme.CheckedChanged += new System.EventHandler(this.lightTheme_CheckedChanged);
            // 
            // darkTheme
            // 
            this.darkTheme.AutoSize = true;
            this.darkTheme.Location = new System.Drawing.Point(30, 200);
            this.darkTheme.Name = "darkTheme";
            this.darkTheme.Size = new System.Drawing.Size(86, 19);
            this.darkTheme.TabIndex = 1;
            this.darkTheme.TabStop = true;
            this.darkTheme.Text = "Dark theme";
            this.darkTheme.UseVisualStyleBackColor = true;
            this.darkTheme.CheckedChanged += new System.EventHandler(this.darkTheme_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Color theme of application:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = $"Save files every: {MainForm.IntervalForSave / 60000} minutes";
            // 
            // trackBar
            // 
            this.trackBar.LargeChange = 1;
            this.trackBar.Location = new System.Drawing.Point(12, 63);
            this.trackBar.Maximum = 29;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(194, 45);
            this.trackBar.TabIndex = 4;
            this.trackBar.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            this.trackBar.Value = MainForm.IntervalForSave / 60000 - 1;
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(464, 261);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.darkTheme);
            this.Controls.Add(this.lightTheme);
            this.MaximumSize = new System.Drawing.Size(480, 300);
            this.MinimumSize = new System.Drawing.Size(480, 300);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        /// <summary>
        /// Изменение темы на светлую. 
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void lightTheme_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.CurrentTheme = "light";
            foreach (var form in Program.Forms)
                form.UpdateAllForNewTheme();
            BackColor = Color.White;
            ForeColor = Color.Black;
        }

        /// <summary>
        /// Изменение темы на темную.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void darkTheme_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.CurrentTheme = "dark";
            foreach (var form in Program.Forms)
                form.UpdateAllForNewTheme();
            BackColor = Color.FromArgb(41, 43, 53);
            ForeColor = Color.FromArgb(170, 180, 190);

        }

        /// <summary>
        /// Изменение интервала автосохранения.
        /// </summary>
        /// <param name="sender">Объект который инициализировал событие. </param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            MainForm.IntervalForSave = 60000 + (60000 * trackBar.Value);
            this.label2.Text = $"Save files every: {MainForm.IntervalForSave / 60000} minutes";
        }
    }
}
