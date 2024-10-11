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
        private bool TopMost = true;
        public FormIndex()
        {
            InitializeComponent();
            ControlEvent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ControlBox = true;
            this.KeyPreview = true;


            this.KeyDown += FormIndex_KeyDown;
        }

        private void FormIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                Application.Exit();
            }
        }

        private void ControlEvent()
        {
            ButtonSimResi.Click += ButtonSimResi_Click;
            ButtonSurvey.Click += ButtonSurvey_Click;
            ButtonAdmin.Click += ButtonAdmin_Click;

            ButtonSimResi.MouseEnter += ButtonSimResi_MouseEnter;
            ButtonSimResi.Enter += ButtonSimResi_MouseEnter;
            ButtonSimResi.MouseLeave += ButtonSimResi_MouseLeave;
            ButtonSimResi.Leave += ButtonSimResi_MouseLeave;

            ButtonSurvey.MouseEnter += ButtonSurvey_Enter;
            ButtonSurvey.Enter += ButtonSurvey_Enter;
            ButtonSurvey.MouseLeave += ButtonSurvey_Leave;
            ButtonSurvey.Leave += ButtonSurvey_Leave;

            ButtonAdmin.MouseEnter += ButtonAdmin_Enter;
            ButtonAdmin.Enter += ButtonAdmin_Enter;
            ButtonAdmin.MouseLeave += ButtonAdmin_Leave;
            ButtonAdmin.Leave += ButtonAdmin_Leave;

        }
        private void ButtonSimResi_MouseEnter(object sender, EventArgs e)
        {
            ButtonSimResi.BackColor = Color.FromArgb(146, 166, 192);
        }
        private void ButtonSimResi_MouseLeave(object sender, EventArgs e)
        {
            ButtonSimResi.BackColor = Color.FromArgb(176, 196, 222);
        }

        private void ButtonSurvey_Enter(object sender,EventArgs e)
        {
            ButtonSurvey.BackColor = Color.FromArgb(149,149,149);
        }
        private void ButtonSurvey_Leave(object sender, EventArgs e)
        {
            ButtonSurvey.BackColor = Color.FromArgb(169, 169, 169);
        }

        private void ButtonAdmin_Enter(object sender, EventArgs e)
        {
            ButtonAdmin.BackColor = Color.FromArgb(149, 149, 149);
        }
        private void ButtonAdmin_Leave(object sender,EventArgs e)
        {
            ButtonAdmin.BackColor = Color.FromArgb(169, 169, 169);
        }

        private void ButtonAdmin_Click(object sender, EventArgs e)
        {
            login log = new login(this);
            log.Show();
            this.Opacity = 0;
        }

        private void ButtonSurvey_Click(object sender, EventArgs e)
        {
            SurveyForm survey = new SurveyForm(this);
            survey.Show();
            this.Opacity = 0;

        }

        private void ButtonSimResi_Click(object sender, EventArgs e)
        {
            Pemakai pakai = new Pemakai(this);
            pakai.Show();

            this.Opacity = 0;
        }
    }
}
