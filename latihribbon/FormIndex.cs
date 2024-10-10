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

            PictureRESI.Click += PictureRESI_Click;
            PictureSurvey.Click += PictureSurvey_Click;
        }

        private void PictureSurvey_Click(object sender, EventArgs e)
        {
            SurveyForm survey = new SurveyForm(this);
            survey.Show();
            this.Opacity = 0;
        }

        private void PictureRESI_Click(object sender, EventArgs e)
        {
            Pemakai pakai = new Pemakai(this);
            pakai.Show();

            this.Opacity = 0;
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
