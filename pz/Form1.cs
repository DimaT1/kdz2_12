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

        public static ViewJarvisMessageEvent<RowChangedArgs> JarvisRowChangedEvent = new ViewJarvisMessageEvent<RowChangedArgs>();

        private void OnJarvisRowChangedEvent(object sender, ViewJarvisMessageEventArgs<RowChangedArgs> messageEventArgs)
        {
            RowChangedArgs args = messageEventArgs.Content;
            Jarvis.viewCellChangedEvent.Plugged = false;
            for (int i = 0; i < args.Row.Count; i++)
            {
                dataGridView1.Rows[args.RowIndex].Cells[i].Value = args.Row[i];
            }
            Jarvis.viewCellChangedEvent.Plugged = true;
            dataGridView1.Refresh();
        }

        public static ViewJarvisMessageEvent<List<string>> JarvisMaxDepthUpdatedEvent = new ViewJarvisMessageEvent<List<string>>();

        private void OnJarvisMaxDepthUpdatedEvent(object sender, ViewJarvisMessageEventArgs<List<string>> messageEventArgs)
        {
            List<string> args = messageEventArgs.Content;
            label23.Text = args[0];
            label3.Text = args[1];
            label5.Text = args[2];
            label7.Text = args[3];
            label9.Text = args[4];
            label11.Text = args[5];
        }

        public static ViewJarvisMessageEvent<List<string>> JarvisMinDepthUpdatedEvent = new ViewJarvisMessageEvent<List<string>>();

        private void OnJarvisMinDepthUpdatedEvent(object sender, ViewJarvisMessageEventArgs<List<string>> messageEventArgs)
        {
            List<string> args = messageEventArgs.Content;
            label24.Text = args[0];
            label20.Text = args[1];
            label18.Text = args[2];
            label16.Text = args[3];
            label14.Text = args[4];
            label12.Text = args[5];
        }

        public Form1()
        {
            InitializeComponent();
            JarvisMessageEvent.ViewJarvisMessageEvnt += OnJarvisMessageEvent;
            JarvisListMessageEvent.ViewJarvisMessageEvnt += OnJarvisListMessageEvent;
            JarvisRowChangedEvent.ViewJarvisMessageEvnt += OnJarvisRowChangedEvent;
            JarvisSetTitleEvent.ViewJarvisMessageEvnt += OnJarvisSetTitleEvent;
            JarvisMaxDepthUpdatedEvent.ViewJarvisMessageEvnt += OnJarvisMaxDepthUpdatedEvent;
            JarvisMinDepthUpdatedEvent.ViewJarvisMessageEvnt += OnJarvisMinDepthUpdatedEvent;

            label23.Text = "NA";
            label3.Text = "NA";
            label5.Text = "NA";
            label7.Text = "NA";
            label9.Text = "NA";
            label11.Text = "NA";
            label24.Text = "NA";
            label20.Text = "NA";
            label18.Text = "NA";
            label16.Text = "NA";
            label14.Text = "NA";
            label12.Text = "NA";
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Jarvis.FileOpened)
            {
                DialogResult dialogResult = MessageBox.Show("", "", MessageBoxButtons.YesNoCancel);
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

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("", "", MessageBoxButtons.YesNoCancel);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Jarvis.viewCellChangedEvent.OnViewJarvisMessage(new CellEventArgs(e.ColumnIndex, e.RowIndex, dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()));
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("", "",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.ExitThread();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("NA", "NA", "NA", "NA", "NA", "NA");
            List<string> list = new List<string>
            {
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text
            };

            int rowIndex = dataGridView1.RowCount - 2;

            for (int i = 0; i < 6; i++)
            {
                Jarvis.viewCellChangedEvent.OnViewJarvisMessage(new CellEventArgs(i, rowIndex, list[i]));
            }

            Jarvis.viewCellChangedEvent.Plugged = false;
            dataGridView1.Refresh();
            //dataGridView1.Rows.Add();
            //dataGridView1.Refresh();
            Jarvis.viewCellChangedEvent.Plugged = true;
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("",
                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void idToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Jarvis.viewModListEvent.OnViewJarvisMessage(new ModifyListClass(sortId: true));

        private void stationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Jarvis.viewModListEvent.OnViewJarvisMessage(new ModifyListClass(sortSt: true));
        }
        
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double val;
            if (double.TryParse(textBox7.Text, out val) && val >= 1 && val <= 9.5)
            {
                if (MessageBox.Show($" {val}", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Jarvis.viewModListEvent.OnViewJarvisMessage(new ModifyListClass(filt: true, mag: val));
                }
            }
            else
            {
                MessageBox.Show("", MessageBoxButtons.OK);
            }
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dataGridView1.RowCount < 1) || (dataGridView1.ColumnCount < 1))
                {
                    throw new ArgumentException("Нет строк для удаления!");
                }
                else
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        Jarvis.quakeInfo.Quakes.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        JarvisMinDepthUpdatedEvent.OnViewJarvisMessage(Jarvis.quakeInfo.MinDepthQuake);
                        JarvisMaxDepthUpdatedEvent.OnViewJarvisMessage(Jarvis.quakeInfo.MaxDepthQuake);
                    }
                    else
                    {
                        throw new ArgumentException("Вы не выбрали строку для удаления!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сообщение");
            }
        }

        private void appendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Jarvis.FileOpened)
            {
                Jarvis.viewSaveFileEvent.OnViewJarvisMessage(new SaveFileArgs(append: true));
            }
            else
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Jarvis.viewSaveFileEvent.OnViewJarvisMessage(new SaveFileArgs(append: true, str: saveFileDialog.FileName));

                    }
                }
            }
        }

        private void appendAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Jarvis.viewSaveFileEvent.OnViewJarvisMessage(new SaveFileArgs(append: true, str: saveFileDialog.FileName));
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
                    Jarvis.viewSaveFileEvent.OnViewJarvisMessage(new SaveFileArgs(append: false, str: saveFileDialog.FileName));
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Jarvis.FileOpened)
            {
                Jarvis.viewSaveFileEvent.OnViewJarvisMessage(new SaveFileArgs(append: false));
            }
            else
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        Jarvis.viewSaveFileEvent.OnViewJarvisMessage(new SaveFileArgs(append: false, str: saveFileDialog.FileName));
                    }
                }
            }
        }

        public ViewJarvisMessageEvent<object> ViewJarvisMessageEvent
        {
            get => default(ViewJarvisMessageEvent<object>);
            set
            {
            }
        }

        public RowChangedArgs RowChangedArgs
        {
            get => default(RowChangedArgs);
            set
            {
            }
        }

        public ViewJarvisMessageEventArgs<object> ViewJarvisMessageEventArgs
        {
            get => default(ViewJarvisMessageEventArgs<object>);
            set
            {
            }
        }

        public SaveFileArgs SaveFileArgs
        {
            get => default(SaveFileArgs);
            set
            {
            }
        }

        public ModifyListClass ModifyListClass
        {
            get => default(ModifyListClass);
            set
            {
            }
        }

        public ViewJarvisEventDelegate ViewJarvisEventDelegate
        {
            get => default(ViewJarvisEventDelegate);
            set
            {
            }
        }

        public ViewJarvisNoMessageEvent ViewJarvisNoMessageEvent
        {
            get => default(ViewJarvisNoMessageEvent);
            set
            {
            }
        }

        public CellEventArgs CellEventArgs
        {
            get => default(CellEventArgs);
            set
            {
            }
        }
    }
}
