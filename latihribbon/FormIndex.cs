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
    public partial class FormIndex : Form
    {
        public FormIndex()
        {
            InitializeComponent();
            ControlEvent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ControlBox = true;
        }

        private void ControlEvent()
        {
            ButtonSimResi.Click += ButtonSimResi_Click;
            ButtonSurvey.Click += ButtonSurvey_Click;
            ButtonAdmin.Click += ButtonAdmin_Click;
        }

        private void ButtonAdmin_Click(object sender, EventArgs e)
        {
            login log = new login(this);
            log.Show();

            this.Hide();
        }

        private void ButtonSurvey_Click(object sender, EventArgs e)
        {
            FormSurvey survey = new FormSurvey(this);
            survey.Show();

            this.Hide();
        }

        private void ButtonSimResi_Click(object sender, EventArgs e)
        {
            Pemakai pakai = new Pemakai(this);
            pakai.Show();

            this.Hide();
        }
    }
}
