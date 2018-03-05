using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventsLibrary;

namespace KDZ_2_12_
{
    /// <summary>
    /// View
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Событие получения сообщения от класса Jarvis
        /// </summary>
        public static ViewJarvisMessageEvent<string> JarvisMessageEvent = new ViewJarvisMessageEvent<string>();

        /// <summary>
        /// Обработчик осбытия получения сообщения от класса Jarvis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs">Аргументы сообщения</param>
        private void OnJarvisMessageEvent(object sender, ViewJarvisMessageEventArgs<string> messageEventArgs)
        {
            string message = messageEventArgs.Content;
            MessageBox.Show(message, "Cообщение");
        }

        /// <summary>
        /// Событие получения таблицы от класса Jarvis
        /// </summary>
        public static ViewJarvisMessageEvent<List<List<string>>> JarvisListMessageEvent = new ViewJarvisMessageEvent<List<List<string>>>();

        /// <summary>
        /// Обработчик события получения таблицы от класса Jarvis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs">Параметр сообщения</param>
        private void OnJarvisListMessageEvent(object sender, ViewJarvisMessageEventArgs<List<List<string>>> messageEventArgs)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            List<List<string>> list = messageEventArgs.Content;
            foreach (List<string> row in list)
            {
                dataGridView1.Rows.Add(row.ToArray());
            }
        }


        public static ViewJarvisMessageEvent<string> JarvisSetTitleEvent = new ViewJarvisMessageEvent<string>();

        private void OnJarvisSetTitleEvent(object sender, ViewJarvisMessageEventArgs<string> messageEventArgs)
        {
            string title = messageEventArgs.Content;
            Program.ThisForm.Text = $"Редактирование файла {title}";
        }

        private static void SetText(string title)
        {
            //Text = $"Редактирование файла {title}";
        }

        /// <summary>
        /// Конструктор View
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            JarvisMessageEvent.ViewJarvisMessageEvnt += OnJarvisMessageEvent;
            JarvisListMessageEvent.ViewJarvisMessageEvnt += OnJarvisListMessageEvent;
        }

        /// <summary>
        /// Обработчик нажатия кнопки открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Jarvis.FileOpened)
            {
                DialogResult dialogResult = MessageBox.Show("Сохранить файл перед закрытием?", "Сообщение", MessageBoxButtons.YesNoCancel);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Jarvis.viewOpenFileEvent.OnViewJarvisMessage(openFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки сохранения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки сохранения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Jarvis.FileOpened)
            {

            }
            else
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки закрытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Jarvis.FileOpened)
            {
                DialogResult dialogResult = MessageBox.Show("Сохранить файл перед выходом?", "Сообщение", MessageBoxButtons.YesNoCancel);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                        Jarvis.viewCloseFileEvent.OnViewJarvisMessage();
                        break;
                    case DialogResult.No:
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                        Jarvis.viewCloseFileEvent.OnViewJarvisMessage();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
        }
    }
}
