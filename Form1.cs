using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTLearningMySQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Task.Delay(1);
            Information information = new Information();
            information.Show();
        }
    }
}
