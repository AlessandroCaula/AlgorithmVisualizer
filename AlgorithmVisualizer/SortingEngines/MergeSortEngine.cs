using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlgorithmVisualizer.SortingEngines
{
    internal class MergeSortEngine : ISortEngine
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
        public MergeSortEngine(int[] valuesArray, Graphics g, int maxValue, int rectangleWidth, int paddingFromSideMargins, int panelHeight)
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
                this.sleepDuration = 5;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Merge sort is a sorting algorithm that follows the divide-and-conquer approach. It works by recursively dividing the input array into smaller subarrays and 
        /// sorting those subarrays then merging them back together to obtain the sorted array. In simple terms, we can say that the process of merge sort is to divide 
        /// the array into two halves, so each half, and then merge the sorted halves back together. This process is repeated until the entire array is sorted.
        /// Divide-and-conquer
        /// 1) Divide: divide the list or array recursively into two halves until it can no more be divided.
        /// 2) Conquer: each subarray is sorted individually using the merge sort algorithm. 
        /// 3) Merge: the sorted subarrays are merged back together in sorted order. The process continues until all elements from both subarrays have been merged.
        /// Time Complexity: O(n log n). Space Complexity: O(n).
        /// </summary>
        public void DoWork()
        {
            MergeSort(this.valuesArray, 0, this.valuesArray.Length - 1);

            return;
        }
        //Recursive MergeSort for dividing the initial array.
        public void MergeSort(int[] array, int begin, int end)
        {
            // Check if the array can be still divided.
            if (begin < end)
            {
                // Compute the index of the half of the list.
                int half = begin + (end - begin) / 2;
                // Keep recursively dividing the array until Half is >= than End (at most one element).
                MergeSort(array, begin, half);
                MergeSort(array, half + 1, end);
                Merge(array, begin, half, end);
            }
        }
        // Merging and sorting the arrays.
        public void Merge(int[] array, int begin, int half, int end)
        {
            // Compute the length of the first half of the array.
            int n1 = half - begin + 1;
            // Compute the length of the second half of the array.
            int n2 = end - half;

            // Initialize two new array. The Left and Right arrays.
            int[] leftArray = new int[n1 + 1];
            int[] rightArray = new int[n2 + 1];

            // Fill the left array with the values from the beginning to the half 
            for (int ii = 0; ii < leftArray.Length - 1; ii++)
                leftArray[ii] = array[begin + ii];
            // Fill the right array with the values from the half to the end 
            for (int jj = 0; jj < rightArray.Length - 1; jj++)
                rightArray[jj] = array[half + jj + 1];
            // Put the "sentinel value"
            leftArray[leftArray.Length - 1] = int.MaxValue;
            rightArray[rightArray.Length - 1] = int.MaxValue;

            // Starting indexes with which loop through the Left and Right array.
            int i = 0;
            int j = 0;

            int k = begin;
            while (i < n1 && j < n2) 
            {
                Thread.Sleep(sleepDuration);

                // Check whether the Stop button is clicked, and the sorting must be stopped.
                if (this.IsToStopSorting)
                    return;

                if (leftArray[i] <= rightArray[j])
                {
                    valuesArray[k] = leftArray[i];

                    RepaintCurrentBars(k);

                    i++;
                }
                else // rightArray[j] < leftArray[i]
                {
                    valuesArray[k] = rightArray[j];

                    RepaintCurrentBars(k);

                    j++;
                }
                k++;
            }

            // Copy remaining elements of the left array.
            while (i < n1)
            {
                valuesArray[k] = leftArray[i];

                RepaintCurrentBars(k);

                i++;
                k++;
            }
            // Copy remaining elements of the right array.
            while (j < n2)
            {
                valuesArray[k] = rightArray[j];

                RepaintCurrentBars(k);

                j++;
                k++;
            }
        }

        /// <summary>
        /// Method used to repaint the rectangles in the panel.
        /// </summary>
        private void RepaintCurrentBars(int indexToRepaint)
        {
            // REPAINTING ACTIONS: Updating the bars on the screen to reflect what happened. 
            // Drawing the current rectangles as the background color (white). To then repaint them with the correct color. 
            g.FillRectangle(this.whiteBrush, (indexToRepaint * this.rectangleWidth) + paddingFromSideMargins, 0, this.rectangleWidth, this.panelHeight);
            // Repaint these two selected values in Red.
            //g.FillRectangle(this.redBrush, (indexToRepaint * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[indexToRepaint], this.rectangleWidth, this.panelHeight);
            //g.FillRectangle(this.redBrush, (previousValueIdx * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[previousValueIdx], this.rectangleWidth, this.panelHeight);
            g.FillRectangle(this.grayBrush, (indexToRepaint * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[indexToRepaint], this.rectangleWidth, this.panelHeight);
        }
        /// <summary>
        /// Method used to subscribe to the External method.
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


        #region Event Handler
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
