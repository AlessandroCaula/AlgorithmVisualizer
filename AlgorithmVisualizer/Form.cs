using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmVisualizer
{
    public partial class Form : System.Windows.Forms.Form
    {
        #region Fields
        int[] arrayOfNumbers;
        int numEntries;
        int maxNumberOfEntries;
        int maxValue;
        Graphics g;
        bool isFormSizeChanged;
        int rectangleWidth;
        int paddingFromSideMargins;
        Task runningSortTask;
        BackgroundWorker bgw = null;
        bool isPaused;
        #endregion


        #region Properties
        #endregion


        #region Constructor
        public Form()
        {
            InitializeComponent();
            LoadDefault();
            PopulateDropDownSortingAlgorithm();
        }
        #endregion


        #region Methods
        /// <summary>
        /// Loading the default values on form launched.
        /// </summary>
        private void LoadDefault()
        {
            if (panelGraphic.Width == 0 || panelGraphic.Height == 0)
                return;
            // At first initialization the number of entries and their maximum number is defined by the width and height of the panel.
            maxNumberOfEntries = panelGraphic.Width;
            maxValue = panelGraphic.Height;
            // Default number of Entries.
            numEntries = maxNumberOfEntries;
            trackBarSpeed.SetRange(1, maxNumberOfEntries);
            trackBarSpeed.Value = maxNumberOfEntries;
            textBoxSpeed.Text = maxNumberOfEntries.ToString();
            this.isFormSizeChanged = false;
        }
        /// <summary>
        /// Populate the drop down that will list all the sorting algorithm.
        /// </summary>
        private void PopulateDropDownSortingAlgorithm()
        {
            // To retrieve all the sorting algorithms, we are gonna ask to the form to look through its own internal structure to find all the classes that implements the ISortEngine interface.
            List<string> classList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            // Sort the list alphabetically.
            classList.Sort();
            // Populate the drop down menu with the names of the algorithms.
            foreach(string entry in classList)
            {
                comboBoxAlgorithmSelector.Items.Add(entry);
            }
            // Set the default combo box value to the first algorithm.
            comboBoxAlgorithmSelector.SelectedItem = 0;
            comboBoxAlgorithmSelector.Text = classList[0];
        }
        /// <summary>
        /// Create Randm Values.
        /// </summary>
        private int[] CreateRandomValues()
        {
            // Creating a random array of values.
            int[] arrayOfNumbers = new int[numEntries];
            Random random = new Random(); // 120696
            for (int i = 0; i < numEntries; i++)
                arrayOfNumbers[i] = random.Next(maxValue);
            this.arrayOfNumbers = arrayOfNumbers;

            return arrayOfNumbers;
        }
        /// <summary>
        /// Draw the rectangles in the panel.
        /// </summary>
        private void DrawRectangle(int[] arrayOfNumbers)
        {
            g = panelGraphic.CreateGraphics();

            panelGraphic.Invalidate();
            panelGraphic.Update();

            // Compute the width dimension of each rectangle.
            this.rectangleWidth = (int)(Math.Floor(panelGraphic.Width / (double)numEntries)) != 0 ? (int)(Math.Floor(panelGraphic.Width / (double)numEntries)) : 1;
            // Compute the residual, not used pixels in the panel.
            int residualPixels = panelGraphic.Width - (numEntries * rectangleWidth);
            // Divide the residual pixels so that half of them will be the padding from the side borders of the panel.
            this.paddingFromSideMargins = residualPixels / 2;

            // X: The x-coordinate of the upper-left corner of the rectangle. Y: The y-coordinate of the upper-left corner of the rectangle.
            // Width: The width of the rectangle. Height: The height of the rectangle.
            for (int i = 0; i < arrayOfNumbers.Length; i++)
            {
                g.FillRectangle(new SolidBrush(Color.DarkGray), (i * this.rectangleWidth) + paddingFromSideMargins, panelGraphic.Height - arrayOfNumbers[i], rectangleWidth, panelGraphic.Height); // + paddingFromPanel
            }
        }
        /// <summary>
        /// Reset and call the creation and drawn of the random values. 
        /// </summary>
        private void ResetAndRedrawnValues()
        {
            // If the sorting task is running don't interrupt it.
            if (this.runningSortTask?.Status == TaskStatus.Running)
                return;

            this.isPaused = false;

            if (isFormSizeChanged)
                LoadDefault();

            // Create the random array of values and draw them.
            int[] arrayOfNumbers = CreateRandomValues();
            DrawRectangle(arrayOfNumbers);
        }
        #endregion


        #region Event Handlers
        /// <summary>
        /// Close the application when the exit button is pressed.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// Action to be performed when the sort button is clicked.
        /// </summary>
        private void buttonSort_Click(object sender, EventArgs e)
        {
            // If the sorting task is running don't interrupt it.
            if (this.runningSortTask?.Status == TaskStatus.Running)
                return;
            // Initialize the background worker.
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            // Running the background worker and pass to it the Sorting Algorithm as the argument. 
            bgw.RunWorkerAsync(argument: comboBoxAlgorithmSelector.SelectedItem);
            this.isPaused = false;
        }
        /// <summary>
        /// Action to be performed when the sort button is clicked.
        /// </summary>
        private void buttonSort_Click1(object sender, EventArgs e)
        {
            // Initialize the background worker.
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork1);
            // Running the background worker and pass to it the Sorting algorithm as the argument.
            bgw.RunWorkerAsync(argument: comboBoxAlgorithmSelector.SelectedItem);
        }


        /// <summary>
        /// Action performed when the Stop button is clicked.
        /// </summary>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            RaiseStopEvent(sender, e);
            this.isPaused = true;
            bgw.Dispose();
            bgw.CancelAsync();
        }
        /// <summary>
        /// Action performed when the Stop button is clicked.
        /// </summary>
        private void buttonStop_Click1(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                bgw.CancelAsync();
                isPaused = true;
            }
        }



        /// <summary>
        /// Action to be performed at the Reset button click. Re-initialization of the array of numbers.
        /// </summary>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (bgw != null)
            {
                RaiseStopEvent(sender, e);
                bgw.CancelAsync();
                bgw.Dispose();
            }

            ResetAndRedrawnValues();
        }
        /// <summary>
        /// Action to be performed when the scroll bar changes value.
        /// </summary>
        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            // Update the numberOfentries based on the trackBarValue.
            this.numEntries = trackBarSpeed.Value;

            // Update the TextBoxEdit if necessary
            if (int.Parse(textBoxSpeed.Text) != trackBarSpeed.Value)
                textBoxSpeed.Text = trackBarSpeed.Value.ToString();

            // Re-create and re-draw the rectangles.
            ResetAndRedrawnValues();
        }
        /// <summary>
        /// Action to be performed when the TextBox value has changed.
        /// </summary>
        private void textBoxSpeed_TextChanged(object sender, EventArgs e)
        {
            // Check if the input value is correct.
            int intTextBoxValue = -1;
            bool isTextBoxInteger = int.TryParse(textBoxSpeed.Text, out intTextBoxValue);

            if (isTextBoxInteger == false || intTextBoxValue < 1 || intTextBoxValue > trackBarSpeed.Maximum)
                textBoxSpeed.Text = trackBarSpeed.Value.ToString();
            else
            {
                if (intTextBoxValue != trackBarSpeed.Value)
                { 
                    trackBarSpeed.Value = intTextBoxValue;
                    trackBarSpeed_Scroll(sender, e);
                }
            }
        }
        /// <summary>
        /// Action performed when the size of the form has changed.
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.isFormSizeChanged = true;
            // Every time that the dimension of the form changes, recompute the array of values.
            ResetAndRedrawnValues();
        }
        /// <summary>
        /// Raise the event to stop the execution of the sorting algorithm.
        /// </summary>
        private void RaiseStopEvent(object sender, EventArgs e)
        {
            StopEvent?.Invoke(sender, e);
        }
        #endregion


        #region BackGround
        /// <summary>
        /// Background worker for the Bubble sort algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Cast the sender.
            BackgroundWorker bw = sender as BackgroundWorker;
            string sortEngineName = (string)e.Argument;
            // Now that we know the sorting algorithm, we will figure out the actual type using Reflection. 
            // Figuring out the concrete class that's going to implement the algorithm. (prepend the namespace: AlgorithmVisualizer
            Type type = Type.GetType("AlgorithmVisualizer." + sortEngineName);
            // Get the constructor of this class Type.
            var ctors = type.GetConstructors();
            try
            {
                // Create a sort engine of the type identified with reflection. And invoke it's constructor.
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height });
                se.SubscribeToExternalMethods(this);
                se.DoWork();
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// Background worker for the Bubble sort algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bgw_DoWork1(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Cast the sender.
            BackgroundWorker bw = sender as BackgroundWorker;
            string sortEngineName = (string)e.Argument;
            // Now that we know the sorting algorithm, we will figure out the actual type using Reflection. 
            // Figuring out the concrete class that's going to implement the algorithm. (prepend the namespace: AlgorithmVisualizer
            Type type = Type.GetType("AlgorithmVisualizer." + sortEngineName);
            // Get the constructor of this class Type.
            var ctors = type.GetConstructors();
            try
            {
                // Create a sort engine of the type identified with reflection. And invoke it's constructor.
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height });
                while (!bgw.CancellationPending && !se.IsArraySorted) // !se.IsSorted
                {
                    se.NextStep();
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion


        #region Events
        /// <summary>
        /// Delegate used for stopping the execution of the 
        /// </summary>
        public delegate void StopEventHandler(object sender, EventArgs e);
        public event StopEventHandler StopEvent;
        #endregion
    }
}
