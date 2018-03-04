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
        public static ViewJarvisMessageEvent<string> JarvisMessageEvent = new ViewJarvisMessageEvent<string>();

        private void OnJarvisMessageEvent(object sender, ViewJarvisMessageEventArgs<string> messageEventArgs)
        {
            string message = messageEventArgs.Content;
            MessageBox.Show(message, "Cообщение");
        }

        public static ViewJarvisMessageEvent<List<List<string>>> JarvisListMessageEvent = new ViewJarvisMessageEvent<List<List<string>>>();

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

        public Form1()
        {
            InitializeComponent();
            JarvisMessageEvent.ViewJarvisMessage += OnJarvisMessageEvent;
            JarvisListMessageEvent.ViewJarvisMessage += OnJarvisListMessageEvent;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Jarvis.viewOpenFileEvent.OnViewJarvisMessage(openFileDialog.FileName);
                }
            }
        }

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
    }
}
