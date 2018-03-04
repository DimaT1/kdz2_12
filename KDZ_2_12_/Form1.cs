using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDZ_2_12_
{
    /// <summary>
    /// View
    /// </summary>
    public partial class Form1 : Form
    {
        public event EventHandler<string> ViewOpenFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    
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
    }
}
