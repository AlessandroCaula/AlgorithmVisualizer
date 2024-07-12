using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmVisualizer.SortingEngines
{
    internal class MergeSortEngine : ISortEngine
    {
        #region Fields
        #endregion

        #region Properties
        public bool IsToStopSorting { get; set; }
        public bool IsArraySorted { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Methods
        public void DoWork()
        {
            return;
        }
        /// <summary>
        /// Method used to repaint the rectangles in the panel.
        /// </summary>
        private void RepaintCurrentBars()
        {
        }
        /// <summary>
        /// Method used to subscribe to the External method.
        /// </summary>
        public void SubscribeToExternalMethods(Form mainForm)
        {
            return;
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
