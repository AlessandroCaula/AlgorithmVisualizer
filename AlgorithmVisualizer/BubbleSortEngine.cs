using System;
using System.Drawing;
using System.Threading;

namespace AlgorithmVisualizer
{
    /// <summary>
    /// Bubble Sort is the simplest sorting algorithm that works by repeatedly swapping the adjacent elements if they are in the wrong order. 
    /// This algorithm is not suitable for large data sets as its average and worst-case time complexity is quite high.
    /// </summary>
    class BubbleSortEngine : ISortEngine
    {
        #region Fields
        private Graphics g;
        int[] valuesArray;
        int maxValue;
        int rectangleWidth;
        int paddingFromSideMargins;
        int panelHeight;
        SolidBrush redBrush;
        SolidBrush greenBrush;
        SolidBrush grayBrush;
        SolidBrush whiteBrush;
        int lastValueSortedIdx;
        int sleepDuration;
        #endregion


        #region Properties
        public bool IsToStopSorting { get; set; }
        #endregion


        #region Constructor
        public BubbleSortEngine(int[] valuesArray, Graphics g, int maxValue, int rectangleWidth, int paddingFromSideMargins, int panelHeight)
        {
            // Maximum value that can be found inside the array
            this.maxValue = maxValue;
            // Array with values to sort 
            this.valuesArray = valuesArray;
            // Graphic for the painting
            this.g = g;
            // Pixels width of the rectangles. Repainting purpose.
            this.rectangleWidth = rectangleWidth;
            // Padding from left and right margin of the panel. Repainting purpose.
            this.paddingFromSideMargins = paddingFromSideMargins;
            // Height of the panel. Repainting purpose.
            this.panelHeight = panelHeight;

            this.redBrush = new SolidBrush(Color.LightCoral);
            this.greenBrush = new SolidBrush(Color.LightGreen);
            this.grayBrush = new SolidBrush(Color.DarkGray);
            this.whiteBrush = new SolidBrush(Color.White);
            this.IsToStopSorting = false;

            //Determine the Duration of the Sleep. 
            this.sleepDuration = 0;
            if (valuesArray.Length <= 200)
                this.sleepDuration = 1;
            if (valuesArray.Length <= 100)
                this.sleepDuration = 5;
            if (valuesArray.Length <= 20)
                this.sleepDuration = 100;
        }
        #endregion


        #region Methods
        /// <summary>
        /// 1) Traverse from left and compare adjacent elements and the higher one is placed at right side. 
        /// 2) In this way, the largest element is moved to the rightmost end at first. 
        /// 3) This process is then continued to find the second largest and place it and so on until the data is sorted.
        /// Total no. of passes: n-1. Total no. of comparisons: n*(n-1)/2
        /// It can be optimized by stopping the algorithm if the inner loop didn’t cause any swap. 
        /// Time Complexity: O(N2). Auxiliary Space: O(1).
        /// </summary>
        public void NextStep()
        {
            int prevHigherValIdx = 0;

            // Flag for checking whether no swaps have occurred during this cycle, meaning that all the elements are already sorted.
            bool swapOccurred = false;
            prevHigherValIdx = 0;

            // Loop through all the elements before the current one (element at i). 
            for (int j = 0; j < this.valuesArray.Length - 1; j++)
            {
                Thread.Sleep(this.sleepDuration);

                // Check whether the Stop button is clicked, and the sorting must be stopped.
                if (this.IsToStopSorting)
                {
                    g.FillRectangle(this.grayBrush, (prevHigherValIdx * this.rectangleWidth) + this.paddingFromSideMargins, this.panelHeight - this.valuesArray[prevHigherValIdx], this.rectangleWidth, this.panelHeight);
                    return;
                }

                // Continue to the next index when j equal to 0.
                if (j == 0)
                    continue;

                // Compare the current element with the one before it. Swap the two element in order to have the higher one always to the right of the array.
                if (valuesArray[j - 1] > valuesArray[j])
                {
                    // Swap the two elements.
                    int tempVal = valuesArray[j - 1];
                    valuesArray[j - 1] = valuesArray[j];
                    valuesArray[j] = tempVal;
                    // Set to True the Flag. A swap has occurred.
                    swapOccurred = true;

                    // Repainting the new temporary sorted bar.
                    RepaintCurrentBars(j - 1, j);

                    // New higher value.
                    prevHigherValIdx = j;
                }
            }

        }
        /// <summary>
        /// Method used to repaint the rectangles in the panel.
        /// </summary>
        private void RepaintCurrentBars(int smallerValue, int higherValue)
        {
            // REPAINTING ACTIONS: Updating the bars on the screen to reflect what happened. 
            // Drawing the current rectangles as the background color (white). To then repaint them with the correct color. 
            g.FillRectangle(this.whiteBrush, ((higherValue - 1) * this.rectangleWidth) + this.paddingFromSideMargins, 0, this.rectangleWidth * 2, this.panelHeight);
            // Repaint the previous higher bar of gray.
            g.FillRectangle(this.grayBrush, (higherValue * this.rectangleWidth) + this.paddingFromSideMargins, this.panelHeight - this.valuesArray[higherValue], this.rectangleWidth, this.panelHeight);

            g.FillRectangle(this.grayBrush, (smallerValue * this.rectangleWidth) + this.paddingFromSideMargins, this.panelHeight - this.valuesArray[smallerValue], this.rectangleWidth, this.panelHeight);
            g.FillRectangle(this.redBrush, (higherValue * this.rectangleWidth) + this.paddingFromSideMargins, this.panelHeight - this.valuesArray[higherValue], this.rectangleWidth, this.panelHeight);
        }


        public void IsSorted()
        {
            return;
        }

        public void Redraw()
        {
            return;
        }
        #endregion


        #region Event Handlers
        /// <summary>
        /// Method used to subscribe to the External method.
        /// </summary>
        public void SubscribeToExternalMethods(Form mainForm)
        {
            mainForm.StopEvent += MainForm_StopEvent;
        }
        /// <summary>
        /// Actions performed when the stop events arrives.
        /// </summary>
        public void MainForm_StopEvent(object sender, EventArgs e)
        {
            this.IsToStopSorting = true;
        }
        #endregion
    }
}
