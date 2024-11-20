using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormIndex : Form
    {
        private Form mainForm;
        private WindowsKeyBlocker windowsKey;
        public FormIndex(Form ff)
        {
            InitializeComponent();
            windowsKey = new WindowsKeyBlocker();
            this.mainForm = ff;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
           // this.TopMost = true;
            this.ControlBox = true;
            ControlEvent();
        }

        private void ControlEvent()
        {
            this.Resize += FormIndex_Shown;
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

            pictureBoxClose.Click += PictureBoxClose_Click;

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            windowsKey.StartBlocking(); // Mulai blokir tombol Windows
        }
7
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            windowsKey.StopBlocking(); // Hentikan blokir tombol Windows
            base.OnFormClosing(e);
        } 


        private void PictureBoxClose_Click(object sender, EventArgs e)
        {
            new MesClose("Masukkan Code Untuk Keluar!").ShowDialog(this);
        }

        private void FormIndex_Shown(object sender, EventArgs e)
        {
            int widthForm = this.Width;
            int leftMargin = (widthForm - (ButtonSimResi.Width*2)) / 3;
            ButtonSimResi.Location = new Point(leftMargin, ButtonSimResi.Location.Y);
            ButtonSurvey.Location = new Point(widthForm - (leftMargin + ButtonSurvey.Width), ButtonSurvey.Location.Y);
            ButtonAdmin.Location = new Point(widthForm / 2 - ButtonAdmin.Width / 2, ButtonAdmin.Location.Y);
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
            login log = new login(mainForm,this);
            log.Show();
            this.Opacity = 0;
        }

        private void ButtonSurvey_Click(object sender, EventArgs e)
        {
            SurveyForm survey = new SurveyForm(mainForm, this);
            survey.Show();
            this.Opacity = 0;
        }

        private void ButtonSimResi_Click(object sender, EventArgs e)
        {
            Pemakai pakai = new Pemakai(mainForm,this);
            pakai.Show();
            this.Opacity = 0;
        }
    }
}
