using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmVisualizer
{
    internal class DiscardedCode
    {
        //// Draw a rectangle every random value.
        //private void DrawRectangle(int[] arrayOfNumbers)
        //{
        //    g = panelGraphic.CreateGraphics();

        //    panelGraphic.Invalidate();
        //    panelGraphic.Update();

        //    // Compute the width dimension of each rectangle.
        //    int rectangleWidth = (int)(Math.Round(panelGraphic.Width / (double)numEntries));
        //    // Compute the residual, not used pixels in the panel.
        //    int residualPixels = panelGraphic.Width - (numEntries * rectangleWidth);
        //    // Find to how many rectangle I can increase the dimension.
        //    int nIncreasedDimRectangle = (int)(Math.Floor(residualPixels / (double)rectangleWidth));
        //    // Compute every how many rectangle increase the dimension of one rectangle.
        //    int increaseFreq = (int)(Math.Ceiling(this.numEntries / (double)nIncreasedDimRectangle));

        //    //   X: The x-coordinate of the upper-left corner of the rectangle.
        //    //   Y: The y-coordinate of the upper-left corner of the rectangle.
        //    //   Width: The width of the rectangle.
        //    //   Height: The height of the rectangle.
        //    int count = 0;
        //    int totalWidthUsed = 0;
        //    for (int i = 0; i < arrayOfNumbers.Length; i++)
        //    {
        //        int increaseWidth = 0;
        //        if (count == increaseFreq)
        //        {
        //            increaseWidth = rectangleWidth;
        //            count = 0;
        //        }
        //        g.FillRectangle(new SolidBrush(Color.Blue), totalWidthUsed, panelGraphic.Height - arrayOfNumbers[i], rectangleWidth + increaseWidth, panelGraphic.Height); // + paddingFromPanel
        //        count++;
        //        totalWidthUsed += rectangleWidth + increaseWidth;
        //    }
        //}
    }
}
