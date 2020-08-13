using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int n1 = 0; 
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|ALL files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            { image = new Bitmap(dialog.FileName); }
            pictureBox1.Image = image;
        
            pictureBox1.Refresh();
            if (n1 > 0)
            {
                Bitmap old = (Bitmap)pictureBox2.Image;
                int x_2 = old.Width;  //Ширина изображения
                int y_2 = old.Height; //Высота изображения
                for (int i = 0; i < x_2; i++)
                {
                    for (int j = 0; j < y_2; j++)
                    {
                        old.SetPixel(i, j, Color.White);
                    }
                }
                pictureBox2.Image = old;
                if (n1 > 1)
                {

                    Bitmap new1 = (Bitmap)pictureBox3.Image; int i1; int j1;

                    int x_3 = new1.Width;  //Ширина изображения
                    int y_3 = new1.Height; //Высота изображения        

                    for (int i = 0; i < x_3; i++)
                    {
                        for (int j = 0; j < y_3; j++)
                        {
                            new1.SetPixel(i, j, Color.White);
                        }
                    }
                    pictureBox3.Image = new1;
                }
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {

                izm(image);
              

            }
            progressBar2.Value = 0;
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГаусуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void полутонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сэпияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Sepiy();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new yrkost();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Sobel();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Rez();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }


        private void отменаПоследнегоДействияToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = pictureBox2.Image;
           
            pictureBox1.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void отмена2ДействийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox3.Image;
           
            pictureBox1.Refresh();
        }



        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            SerWorld f = new SerWorld();
            
            izm(f.serw((Bitmap)Clipboard.GetImage()));
        }

        private void гистограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] image_matr; // матрица полутонового изображения
            int[] hist; //гистограмма

            double[,] obr_image; // матрица обработанного изображения
            double[] hist_new; //гистограмма

            int fmax, fmin; //максимальное и минимальное значение яркости изображения
            int w_b, h_b; //ширина и высота изображения

            Bitmap ish_bitmap = (Bitmap)pictureBox1.Image;

            w_b = ish_bitmap.Width;  //Ширина изображения
            h_b = ish_bitmap.Height; //Высота изображения

            image_matr = new byte[w_b, h_b];  //матрица изображения 
            obr_image = new double[w_b, h_b];  //матрица изображения 

            hist = new int[256];
            hist_new = new double[256];

            fmin = 1000;
            fmax = -100;
                
            for (int x = 0; x < w_b; x++)
            {
                for (int y = 0; y < h_b; y++)
                {
                    Color c = ish_bitmap.GetPixel(x, y);//получаем цвет указанной точки
                    int r = Convert.ToInt32(c.R);
                    int b = Convert.ToInt32(c.B);
                    int g = Convert.ToInt32(c.G);
                    int brit = Convert.ToInt32(0.299 * r + 0.587 * g + 0.114 * b); //Перевод из RGB в полутон
                    image_matr[x, y] = Convert.ToByte(brit);
                }
            }

            for (int x = 0; x < w_b; x++)
            {
                for (int y = 0; y < h_b; y++)
                {
                    hist[image_matr[x, y]]++;

                    if (image_matr[x, y] > fmax)
                        fmax = image_matr[x, y];
                    if (image_matr[x, y] < fmin)
                        fmin = image_matr[x, y];
                }
            }

            //Отображение гистограммы на компоненте chart
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 256; i++)
            {
                chart1.Series[0].Points.AddXY(i, hist[i]);
            }
            chart1.Update();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            Bitmap old = (Bitmap)Clipboard.GetImage();
            int w_b = old.Width;  //Ширина изображения
            int h_b = old.Height; //Высота изображения
            Bitmap new0 = (Bitmap)Clipboard.GetImage();  int i1; int j1;
            for (int i = 0; i < w_b; i++)
            {
                for (int j = 0; j < h_b; j++)
                {
                    if (i >= 49)
                    {
                        i1 = i - 49;
                        j1 = j;
                        new0.SetPixel(i1, j1, old.GetPixel(i, j));
                    }
                }
            }
            for (int i = w_b - 51; i < w_b; i++)
            {
                for (int j = 0; j < h_b; j++)
                {
                    new0.SetPixel(i, j, Color.Black);
                }
            }

            izm(new0);

            
        }

        private Bitmap rotateImage(Bitmap b, float angle)
        {
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            
            Graphics g = Graphics.FromImage(returnBitmap);
            
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            
            g.RotateTransform(angle);
            
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            
            g.DrawImage(b, new Point(0, 0));

            return returnBitmap;
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            izm(rotateImage((Bitmap)pictureBox1.Image, 10));        
            





        }

        private void СтеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            Bitmap old = (Bitmap)Clipboard.GetImage();
            int w_b = old.Width;  //Ширина изображения
            int h_b = old.Height; //Высота изображения
            Bitmap new1 = (Bitmap)Clipboard.GetImage(); int i1 = -2; int j1 = -2;
            var rand = new Random();
            for (int i = 0; i < w_b; i++)
            {
                for (int j = 0; j < h_b; j++)
                {
                    if (i < 5)
                    {
                        i1 = (i + ((rand.Next(3))));
                    }
                    else
                    {
                        if (i > w_b - 6)
                        {
                            i1 = (i + (rand.Next(3)) * (-1));
                        }
                        else
                        {
                            i1 = (i + (rand.Next(2) * 5 - 2));

                        }
                    }
                    if (j < 5)
                    {
                        j1 = (j + (rand.Next(3)));
                    }
                    else
                    {
                        if (j > h_b - 6)
                        {
                            j1 = (j + (rand.Next(3)) * (-1));
                        }
                        else
                        {
                            j1 = (j + (rand.Next(2) * 5 - 2));

                        }
                    }

                    new1.SetPixel(i, j, old.GetPixel(i1, j1));
                    
                }
            }
            izm(new1); 
        }

        private void щарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new sharra2();

            filter = new sharra1();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void прьюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new sharra1();

            filter = new sharra2();
            backgroundWorker1.RunWorkerAsync(filter);
        }

       

        private void izm (Bitmap b)
        {
            if (n1 > 0)
            {
                Clipboard.SetImage(pictureBox2.Image);
                pictureBox3.Image = Clipboard.GetImage();
            }
            Clipboard.SetImage(pictureBox1.Image);
            
            
            pictureBox1.Image = b;
            pictureBox2.Image = Clipboard.GetImage();
            n1 = n1 + 1;
           
           
        }
        private Bitmap dilat(Bitmap b)
        {
            Clipboard.SetImage(b);
            Bitmap TempBitmap = (Bitmap)Clipboard.GetImage();
            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics = Graphics.FromImage(NewBitmap);
            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), GraphicsUnit.Pixel);
            NewGraphics.Dispose();
            Random TempRandom = new Random();
            int ApetureMin = -(4 / 2);
            int ApetureMax = (4 / 2);
            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    int RValue = 0;
                    int GValue = 0;
                    int BValue = 0;
                    for (int x2 = ApetureMin; x2 < ApetureMax; ++x2)
                    {
                        int TempX = x + x2;
                        if (TempX >= 0 && TempX < NewBitmap.Width)
                        {
                            for (int y2 = ApetureMin; y2 < ApetureMax; ++y2)
                            {
                                int TempY = y + y2;
                                if (TempY >= 0 && TempY < NewBitmap.Height)
                                {
                                    Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                    if (TempColor.R > RValue)
                                        RValue = TempColor.R;
                                    if (TempColor.G > GValue)
                                        GValue = TempColor.G;
                                    if (TempColor.B > BValue)
                                        BValue = TempColor.B;
                                }
                            }
                        }
                    }
                    Color TempPixel = Color.FromArgb(RValue, GValue, BValue);
                    NewBitmap.SetPixel(x, y, TempPixel);
                }
            }
            return NewBitmap;
        }

        private Bitmap erozion(Bitmap b)
        {
            Clipboard.SetImage(b);
            Bitmap TempBitmap = (Bitmap)Clipboard.GetImage();
            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics = Graphics.FromImage(NewBitmap);
            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), GraphicsUnit.Pixel);
            NewGraphics.Dispose();
            Random TempRandom = new Random();
            int ApetureMin = -(4 / 2);
            int ApetureMax = (4 / 2);
            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    int RValue = 255;
                    int GValue = 255;
                    int BValue = 255;
                    for (int x2 = ApetureMin; x2 < ApetureMax; ++x2)
                    {
                        int TempX = x + x2;
                        if (TempX >= 0 && TempX < NewBitmap.Width)
                        {
                            for (int y2 = ApetureMin; y2 < ApetureMax; ++y2)
                            {
                                int TempY = y + y2;
                                if (TempY >= 0 && TempY < NewBitmap.Height)
                                {
                                    Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                    if (TempColor.R < RValue)
                                        RValue = TempColor.R;
                                    if (TempColor.G < GValue)
                                        GValue = TempColor.G;
                                    if (TempColor.B < BValue)
                                        BValue = TempColor.B;
                                }
                            }
                        }
                    }
                    Color TempPixel = Color.FromArgb(RValue, GValue, BValue);
                    NewBitmap.SetPixel(x, y, TempPixel);
                }
            }
            return NewBitmap;


        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            izm(dilat((Bitmap)pictureBox1.Image));
        }

        private void ОткрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            izm(dilat(erozion((Bitmap)pictureBox1.Image)));
        }

        private void эрозияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            izm(erozion((Bitmap)pictureBox1.Image));
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            Bitmap TempBitmap = (Bitmap)Clipboard.GetImage();
            int Size = 8;
            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics =Graphics.FromImage(NewBitmap);
            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), GraphicsUnit.Pixel);
            Random TempRandom = new Random();
            int ApetureMin = -(Size / 2);
            int ApetureMax = (Size / 2);
            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    List<int> RValues = new List<int>();
                    List<int> GValues = new List<int>();
                    List<int> BValues = new List<int>();
                    for (int x2 = ApetureMin; x2 < ApetureMax; ++x2)
                    {
                        int TempX = x + x2;
                        if (TempX >= 0 && TempX < NewBitmap.Width)
                        {
                            for (int y2 = ApetureMin; y2 < ApetureMax; ++y2)
                            {
                                int TempY = y + y2;
                                if (TempY >= 0 && TempY < NewBitmap.Height)
                                {
                                    Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                    RValues.Add(TempColor.R);
                                    GValues.Add(TempColor.G);

                                    BValues.Add(TempColor.B);
                                }
                            }
                        }
                    }
                    RValues.Sort();
                    GValues.Sort();
                    BValues.Sort();
                    Color MedianPixel = Color.FromArgb(RValues[RValues.Count / 2], GValues[GValues.Count / 2], BValues[BValues.Count / 2]);
                    NewBitmap.SetPixel(x, y, MedianPixel);
                }
            }
            izm(NewBitmap);


        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            izm(erozion(dilat((Bitmap)pictureBox1.Image)));
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            izm(erozion(erozion(dilat((Bitmap)pictureBox1.Image))));
        }





        
    }
      

   

}
    
