using System;
using System.Drawing;
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
        ISortEngine se;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Form()
        {
            InitializeComponent();

            LoadDefault();
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

            g = panelGraphic.CreateGraphics();
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
            this.rectangleWidth = (int)(Math.Round(panelGraphic.Width / (double)numEntries)) != 0 ? (int)(Math.Round(panelGraphic.Width / (double)numEntries)) : 1;
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

            if (isFormSizeChanged)
                LoadDefault();
            //g.Dispose();
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
        /// Action to be performed at the Reset button click. Re-initialization of the array of numbers.
        /// </summary>
        private void buttonReset_Click(object sender, EventArgs e)
        {
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
        /// Action to be performed when the sort button is clicked.
        /// </summary>
        private void buttonSort_Click1(object sender, EventArgs e)
        {
            // If the sorting task is running don't interrupt it.
            if (this.runningSortTask?.Status == TaskStatus.Running)
                return;

            // Create an instance of the Sort Engine. 
            ISortEngine se = new BubbleSortEngine();
            // Call the method used to subscribe to the 
            se.SubscribeToExternalMethods(this);
            // Call the DoWork Method in a separate Task.
            this.runningSortTask = Task.Run(() =>
            se.DoWork(this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height)
            );
        }


        /// <summary>
        /// Action to be performed when the sort button is clicked.
        /// </summary>
        private void buttonSort_Click(object sender, EventArgs e)
        {
            // If the sorting task is running don't interrupt it.
            if (this.runningSortTask?.Status == TaskStatus.Running)
                return;

            if (se == null)
            {
                // Create an instance of the Sort Engine. 
                this.se = new BubbleSortEngine();
                // Call the method used to subscribe to the 
                se.SubscribeToExternalMethods(this);
            }
            // Call the DoWork Method in a separate Task.
            this.runningSortTask = Task.Run(() =>
            se.DoWork(this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height)
            );
        }


        /// <summary>
        /// Action performed when the Stop button is clicked.
        /// </summary>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            RaiseStopEvent(sender, e);
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
            //if (this.runningSortTask != null)
            //{   
            //    this.runningSortTask.Wait();
            //}

            StopEvent?.Invoke(sender, e);
        }
        #endregion


        #region Delegates
        /// <summary>
        /// Delegate used for stopping the execution of the 
        /// </summary>
        public delegate void StopEventHandler(object sender, EventArgs e);
        #endregion


        #region Events
        public event StopEventHandler StopEvent;
        #endregion
    }
}
