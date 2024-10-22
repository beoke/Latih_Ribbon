using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.Helper
{
    public class MesBox
    {
       /* public void MesInfo(string text)
        {
            MessageBox.Show($"{text}","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }*/

        public bool MesKonfirmasi(string text)
        {
            bool cek = false;
            if (MessageBox.Show(text, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) cek = true;
            return cek;
        }
    }
}
 