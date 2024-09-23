using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }

        private void FormLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ProgresBarLoading.Value < 100)
            {
                ProgresBarLoading.Value += 3;
                LabelLoading.Text = ProgresBarLoading.Value.ToString() + "%";
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
