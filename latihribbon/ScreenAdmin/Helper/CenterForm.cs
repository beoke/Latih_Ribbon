using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public class CenterForm
    {
        public static LoadingModel TemplateLocation(Form parent, Form child )
        {
            int parentX = parent.Location.X;
            int parentY = parent.Location.Y;

            // Dapatkan ukuran form induk
            int parentWidth = parent.Width;
            int parentHeight = parent.Height;

            // Hitung posisi form anak agar berada di tengah form induk
            int childWidth = child.Width;
            int childHeight = child.Height;
            int centerX = parentX + (parentWidth - childWidth) / 2;
            int centerY = parentY + (parentHeight - childHeight) / 2;

            return new LoadingModel {centerX = centerX, centerY = centerY };
        }
    }
}
public class LoadingModel
{
    public int centerX { get; set; }
    public int centerY { get; set; }
}
