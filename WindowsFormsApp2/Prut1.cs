﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Prut1 : MatrixFilter
    {
        public Prut1()
        {
            int sizeX = 3; int sizeY = 3; kernel = new float[sizeX, sizeY];
            kernel[0, 0] = -1; kernel[0, 1] = 0; kernel[0, 2] = 1;
            kernel[1, 0] = -1; kernel[1, 1] = 0; kernel[1, 2] = -1;
            kernel[2, 0] = -1; kernel[2, 1] = 0; kernel[2, 2] = 1;
        }
    }
}
