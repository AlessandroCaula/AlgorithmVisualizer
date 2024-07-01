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
        /// <summary>
        /// DoWork interface method, that will be called for the execution of the sorting algorithm. 
        /// </summary>
        /// <param name="valuesArray"> Array with values to sort </param>
        /// <param name="g"> Graphic for the painting </param>
        /// <param name="maxValue"> Maximum value that can be found inside the array </param>
        /// <param name="rectangleWidth"> Pixels widht of the rectangles. Repainting pourpose </param>
        /// <param name="paddingFromSideMargins"> Padding from left and right margin of the panel. Repainting pourpose </param>
        /// <param name="panelHeight"> Height of the panel. Repainting pourpose </param>
        void DoWork(int[] valuesArray, Graphics g, int maxValue, int rectangleWidth, int paddingFromSideMargins, int panelHeight);
    }
}
