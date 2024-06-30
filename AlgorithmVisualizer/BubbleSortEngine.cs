using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmVisualizer
{
    /// <summary>
    /// Bubble Sort is the simplest sorting algorithm that works by repeatedly swapping the adjacent elements if they are in the wrong order. 
    /// This algorithm is not suitable for large data sets as its average and worst-case time complexity is quite high.
    /// </summary>
    class BubbleSortEngine : ISortEngine
    {
        private Graphics g;
        int[] valuesArray;
        int maxValue;
        SolidBrush redBrush;
        SolidBrush greenBrush;
        
        
        /// <summary>
        /// 1) Traverse from left and compare adjacent elements and the higher one is placed at right side. 
        /// 2) In this way, the largest element is moved to the rightmost end at first. 
        /// 3) This process is then continued to find the second largest and place it and so on until the data is sorted.
        /// Total no. of passes: n-1. Total no. of comparisons: n*(n-1)/2
        /// It can be optimized by stopping the algorithm if the inner loop didn’t cause any swap. 
        /// Time Complexity: O(N2). Auxiliary Space: O(1).
        /// </summary>
        public void DoWork(int[] valuesArray, Graphics g, int maxValue, int rectangleWidth, int paddingFromSideMargins, int panelHeight)
        {
            this.maxValue = maxValue;
            this.valuesArray = valuesArray;
            this.g = g;
            redBrush = new SolidBrush(Color.LightCoral);
            greenBrush = new SolidBrush(Color.LightGreen);

            int iteration = 0;
            bool swapOccurred;
            // Loop through the length of the entire array.
            for (int i = valuesArray.Length; i >= 0; i--)
            {
                // Flag for checking whether no swaps have occurred during this cycle, meaning that all the elements are already sorted.
                swapOccurred = false;

                // Loop through all the elements before the current one (element at i). 
                for (int j = 0; j < i; j++)
                {
                    if (j == 0)
                        continue;

                    // Compare the current element with the one before it. Swap the two element in order to have the higher one always to the right of the array.
                    if (valuesArray[j - 1] > valuesArray[j])
                    {
                        // Swap the two elements.
                        int tempVal = valuesArray[j - 1];
                        valuesArray[j - 1] = valuesArray[j];
                        valuesArray[j] = tempVal;
                        // Set to True the Flag. A swap has occured.
                        swapOccurred = true;

                        // Updating the bars on the screen to reflect what happend. 
                        // Drawing the coloured rectangles as the background color (white). 
                        g.FillRectangle(new SolidBrush(Color.White), ((j - 1) * rectangleWidth) + paddingFromSideMargins, 0, rectangleWidth * 2, panelHeight);
                        // Repainintg the new temporary sorted bar.
                        g.FillRectangle(new SolidBrush(Color.LightCoral), ((j - 1) * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[j - 1], rectangleWidth, panelHeight);
                        g.FillRectangle(new SolidBrush(Color.LightCoral), (j * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[j], rectangleWidth, panelHeight);
                    }

                    iteration++;
                    // 268088
                }

                // Check if no swapped have occurred, therefore the Array has been completely sorted.
                if (swapOccurred == false)
                    return;
            }
        }
    }
}
