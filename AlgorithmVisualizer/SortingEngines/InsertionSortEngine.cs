using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlgorithmVisualizer.SortingEngines
{
    internal class InsertionSortEngine : ISortEngine
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
        public bool IsArraySorted { get; set; }
        #endregion


        #region Constructor
        public InsertionSortEngine(int[] valuesArray, Graphics g, int maxValue, int rectangleWidth, int paddingFromSideMargins, int panelHeight)
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
            // Create the different brush for coloring the bars.
            this.redBrush = new SolidBrush(Color.LightCoral);
            this.greenBrush = new SolidBrush(Color.LightGreen);
            this.grayBrush = new SolidBrush(Color.DarkGray);
            this.whiteBrush = new SolidBrush(Color.White);
            this.IsToStopSorting = false;
            this.IsArraySorted = false;

            //Determine the Duration of the Sleep. 
            this.sleepDuration = 0;
            if (valuesArray.Length <= 50)
                this.sleepDuration = 300;
            else if (valuesArray.Length <= 100)
                this.sleepDuration = 150;
            else if (valuesArray.Length <= 300)
                this.sleepDuration = 100;
            else
                this.sleepDuration = 0;
        }
        #endregion


        #region Methods
        /// <summary>
        /// 1) Start with the second element of the array as first element in the array is assumed to be sorted.
        /// 2) Compare second element with the first element and check if the second element is smaller then swap them.
        /// 3) Move to the third element and compare it with the second element, then the first element and swap as necessary to put it in the correct position among the first three elements.
        /// 4) Continue this process, comparing each element with the ones before it and swapping as needed to place it in the correct position among the sorted elements. 
        /// 5) Repeat until the entire array is sorted.
        /// Time Complexity: O(N^2). Auxiliary Space: O(1).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DoWork()
        {
            //valuesArray = new int[] { 6, 5, 3, 1, 8, 7, 2, 4 };

            // First index. Current Number.
            for (int i = 1; i < valuesArray.Length; i++)
            {
                // Current value.
                int key = valuesArray[i];
                int j = i - 1;

                // Shift the values higher than the key toward the right, making the space for the key value until this is in the temporary current place.
                while (j >= 0 && key < valuesArray[j])
                {
                    // Check whether the Stop button is clicked, and the sorting must be stopped.
                    if (this.IsToStopSorting)
                        return;

                    RepaintCurrentBars(j + 1, j);

                    // Shift to the right.
                    valuesArray[j + 1] = valuesArray[j];

                    // This exchange is just for visual representation, It can also be done when the exiting the while loop. 
                    valuesArray[j] = key;

                    RepaintCurrentBars(j + 1, j);

                    Thread.Sleep(sleepDuration);

                    // Repaint these two selected values in Red.
                    g.FillRectangle(this.grayBrush, ((j + 1) * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[j + 1], this.rectangleWidth, this.panelHeight);
                    g.FillRectangle(this.grayBrush, (j * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[j], this.rectangleWidth, this.panelHeight);

                    j--;
                }
            }

            return;
        }
        /// <summary>
        /// Method used to Repaint the object bars.
        /// </summary>
        private void RepaintCurrentBars(int currentValueIdx, int previousValueIdx)
        {
            // REPAINTING ACTIONS: Updating the bars on the screen to reflect what happened. 
            // Drawing the current rectangles as the background color (white). To then repaint them with the correct color. 
            g.FillRectangle(this.whiteBrush, (currentValueIdx * this.rectangleWidth) + paddingFromSideMargins, 0, this.rectangleWidth, this.panelHeight);
            g.FillRectangle(this.whiteBrush, (previousValueIdx * this.rectangleWidth) + paddingFromSideMargins, 0, this.rectangleWidth, this.panelHeight);
            // Repaint these two selected values in Red.
            g.FillRectangle(this.redBrush, (currentValueIdx * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[currentValueIdx], this.rectangleWidth, this.panelHeight);
            g.FillRectangle(this.redBrush, (previousValueIdx * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[previousValueIdx], this.rectangleWidth, this.panelHeight);
        }
        /// <summary>
        /// Subscribe to the Events fired from the main form.
        /// </summary>
        public void SubscribeToExternalMethods(Form mainForm)
        {
            mainForm.StopEvent -= MainForm_StopEvent;
            mainForm.StopEvent += MainForm_StopEvent;
        }

        public void IsSorted()
        {
            return;
        }
        public void NextStep()
        {
            return;
        }
        public void Redraw()
        {
            return;
        }
        #endregion


        #region Event Handlers
        public void MainForm_StopEvent(object sender, EventArgs e)
        {
            this.IsToStopSorting = true;
        }
        #endregion
    }
}
