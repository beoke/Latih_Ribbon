using System.Windows.Forms;
using System.Drawing;

namespace latihribbon
{
    public class StyleComponent
    {
        public static void StyleGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dgv.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dgv.RowTemplate.Height = 30;
            dgv.ColumnHeadersHeight = 35;
        }
    }
}
