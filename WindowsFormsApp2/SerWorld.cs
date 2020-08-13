using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Data;

namespace WindowsFormsApp2
{
    class SerWorld
    {
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
        public Bitmap serw(Bitmap sourceImage)
        {

            Bitmap b = sourceImage;
            int Rall = 0; int Gall = 0; int Ball = 0;
            int width = b.Width;
            int height = b.Height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Rall = b.GetPixel(i, j).R + Rall;
                    Gall = b.GetPixel(i, j).G + Gall;
                    Ball = b.GetPixel(i, j).B + Ball;
                }
            }
            double Rl = (1.0 / (width * height)) * (Rall);
            double Gl = (1.0 / (width * height)) * (Gall);
            double Bl = (1.0 / (width * height)) * (Ball);
            double Avg = (Rl + Gl + Bl) / 3;
            int rfin; int gfin; int bfin;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    rfin = (int)(b.GetPixel(i, j).R * (Avg / Rl));
                    gfin = (int)(b.GetPixel(i, j).G * (Avg / Gl));
                    bfin = (int)(b.GetPixel(i, j).B * (Avg / Bl));
                    b.SetPixel(i, j, Color.FromArgb(Clamp(rfin, 0, 255), Clamp(gfin, 0, 255), Clamp(bfin, 0, 255)));
                   
                }
            }

            return b;
        }
    }
}
