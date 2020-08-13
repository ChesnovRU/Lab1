using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Sepiy : Filters
    {
        protected float[,] kernel = null;
        public Sepiy() { }
        public Sepiy(float[,] kernel)
        {
            this.kernel = kernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            double Intensity = 1;

            Color sourceColor = sourceImage.GetPixel(x, y);
            Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            Color resultcolor = Color.FromArgb(Clamp((int)Intensity + 2 * 2, 0, 255), Clamp((int)((int)Intensity + 0.5 * 2), 0, 255), Clamp((int)Intensity - 1 * 2, 0, 255));




            return resultcolor;
            
               

        }
    }
}
    

