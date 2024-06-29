using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmVisualizer
{
    interface ISortEngine
    {
        void DoWork(int[] valuesArray, Graphics g, int maxValue);
    }
}
