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
        /// Property that will set if the stop event has been raised from the main form, and the sorting algorithm has to stop.
        /// </summary>
        bool IsToStopSorting { get; set; }
        bool IsArraySorted { get; set; }
        /// <summary>
        /// Method that will be used for subscribe to external events. Like the Stop event.
        /// </summary>
        void SubscribeToExternalMethods(Form mainForm);
        /// <summary>
        /// Method that we need to subscribe in order to handle the Stop event.
        /// </summary>
        void MainForm_StopEvent(object sender, EventArgs e);
        /// <summary>
        /// DoWork interface method, that will be called for the execution of the sorting algorithm. 
        /// </summary>
        void DoWork();
        /// <summary>
        /// Every time it is called it will take one step toward sorting the output.
        /// </summary>
        void NextStep();
        /// <summary>
        /// Test method used to know if the array has been completely sorted.
        /// </summary>
        void IsSorted();
        /// <summary>
        /// Method that allows you to pause and resume and refresh the graphics object.
        /// </summary>
        void Redraw();
    }
}
