using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
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

        ///// <summary>
        ///// Action to be performed when the sort button is clicked.
        ///// </summary>
        //private void buttonSort_Click1(object sender, EventArgs e)
        //{
        //    // If the sorting task is running don't interrupt it.
        //    if (this.runningSortTask?.Status == TaskStatus.Running)
        //        return;

        //    // Create an instance of the Sort Engine. 
        //    ISortEngine se = new BubbleSortEngine();
        //    // Call the method used to subscribe to the 
        //    se.SubscribeToExternalMethods(this);
        //    // Call the DoWork Method in a separate Task.
        //    this.runningSortTask = Task.Run(() =>
        //    se.DoWork(this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height)
        //    );
        //}



    }




    ///////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    /// Previous Implementation of the Bubble Sort Engine Class.///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Bubble Sort is the simplest sorting algorithm that works by repeatedly swapping the adjacent elements if they are in the wrong order. 
    /// This algorithm is not suitable for large data sets as its average and worst-case time complexity is quite high.
    /// </summary>
    //class BubbleSortEngine1 : ISortEngine
    //{
    //    #region Fields
    //    private Graphics g;
    //    int[] valuesArray;
    //    int maxValue;
    //    SolidBrush redBrush;
    //    SolidBrush greenBrush;
    //    SolidBrush grayBrush;
    //    SolidBrush whiteBrush;
    //    int lastValueSortedIdx;
    //    #endregion


    //    #region Properties
    //    public bool IsToStopSorting { get; set; }
    //    #endregion


    //    #region Constructor
    //    public BubbleSortEngine1() { }
    //    #endregion


    //    #region Methods
    //    /// <summary>
    //    /// 1) Traverse from left and compare adjacent elements and the higher one is placed at right side. 
    //    /// 2) In this way, the largest element is moved to the rightmost end at first. 
    //    /// 3) This process is then continued to find the second largest and place it and so on until the data is sorted.
    //    /// Total no. of passes: n-1. Total no. of comparisons: n*(n-1)/2
    //    /// It can be optimized by stopping the algorithm if the inner loop didn’t cause any swap. 
    //    /// Time Complexity: O(N2). Auxiliary Space: O(1).
    //    /// </summary>
    //    public void DoWork(int[] valuesArray, Graphics g, int maxValue, int rectangleWidth, int paddingFromSideMargins, int panelHeight)
    //    {
    //        this.maxValue = maxValue;
    //        this.valuesArray = valuesArray;
    //        this.g = g;
    //        this.redBrush = new SolidBrush(Color.LightCoral);
    //        this.greenBrush = new SolidBrush(Color.LightGreen);
    //        this.grayBrush = new SolidBrush(Color.DarkGray);
    //        this.whiteBrush = new SolidBrush(Color.White);
    //        this.IsToStopSorting = false;
    //        int prevHigherValIdx = 0;

    //        //Determine the Duration of the Sleep. 
    //        int sleepDuration = 0;
    //        if (valuesArray.Length <= 200)
    //            sleepDuration = 1;
    //        if (valuesArray.Length <= 100)
    //            sleepDuration = 5;
    //        if (valuesArray.Length <= 20)
    //            sleepDuration = 100;

    //        // Flag used to interrupt the computation if the array has been completely sorted.
    //        bool swapOccurred;
    //        // Loop through the length of the entire array.
    //        for (int i = valuesArray.Length; i >= 0; i--)
    //        {
    //            // Flag for checking whether no swaps have occurred during this cycle, meaning that all the elements are already sorted.
    //            swapOccurred = false;
    //            prevHigherValIdx = 0;

    //            // Loop through all the elements before the current one (element at i). 
    //            for (int j = 0; j < i; j++)
    //            {
    //                Thread.Sleep(sleepDuration);

    //                // Check whether the Stop button is clicked, and the sorting must be stopped.
    //                if (this.IsToStopSorting)
    //                {
    //                    g.FillRectangle(this.grayBrush, (prevHigherValIdx * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[prevHigherValIdx], rectangleWidth, panelHeight);
    //                    return;
    //                }

    //                // Continue to the next index when j equal to 0.
    //                if (j == 0)
    //                    continue;

    //                // Compare the current element with the one before it. Swap the two element in order to have the higher one always to the right of the array.
    //                if (valuesArray[j - 1] > valuesArray[j])
    //                {
    //                    // Swap the two elements.
    //                    int tempVal = valuesArray[j - 1];
    //                    valuesArray[j - 1] = valuesArray[j];
    //                    valuesArray[j] = tempVal;
    //                    // Set to True the Flag. A swap has occurred.
    //                    swapOccurred = true;

    //                    // REPAINTING ACTIONS: Updating the bars on the screen to reflect what happened. 
    //                    // Drawing the current rectangles as the background color (white). To then repaint them with the correct color. 
    //                    g.FillRectangle(this.whiteBrush, ((j - 1) * rectangleWidth) + paddingFromSideMargins, 0, rectangleWidth * 2, panelHeight);
    //                    // Repaint the previous higher bar of gray.
    //                    g.FillRectangle(this.grayBrush, (prevHigherValIdx * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[prevHigherValIdx], rectangleWidth, panelHeight);
    //                    // Repainting the new temporary sorted bar.
    //                    RepaintCurrentBars(j - 1, j, valuesArray, g, rectangleWidth, paddingFromSideMargins, panelHeight);
    //                    // New higher value.
    //                    prevHigherValIdx = j;
    //                }
    //                else if (j == i - 1)
    //                {
    //                    g.FillRectangle(this.greenBrush, (j * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[j], rectangleWidth, panelHeight);
    //                }
    //            }
    //            // Color the last element, which has been sorted in green.
    //            g.FillRectangle(this.greenBrush, ((prevHigherValIdx) * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[prevHigherValIdx], rectangleWidth, panelHeight);

    //            // Check if no swapped have occurred, therefore the Array has been completely sorted.
    //            if (swapOccurred == false)
    //            {
    //                // Color all the previous bars to green. Since they have been already sorted.
    //                for (int z = 0; z < i; z++)
    //                    g.FillRectangle(this.greenBrush, (z * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[z], rectangleWidth, panelHeight);
    //                return;
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// Method used to repaint the rectangles in the panel.
    //    /// </summary>
    //    private void RepaintCurrentBars(int smallerValue, int higherValue, int[] valuesArray, Graphics g, int rectangleWidth, int paddingFromSideMargins, int panelHeight)
    //    {
    //        g.FillRectangle(this.grayBrush, (smallerValue * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[smallerValue], rectangleWidth, panelHeight);
    //        g.FillRectangle(this.redBrush, (higherValue * rectangleWidth) + paddingFromSideMargins, panelHeight - valuesArray[higherValue], rectangleWidth, panelHeight);
    //    }


    //    public void NexStep()
    //    {
    //        return;
    //    }

    //    public void IsSorted()
    //    {
    //        return;
    //    }

    //    public void Redraw()
    //    {
    //        return;
    //    }
    //    #endregion


    //    #region Event Handlers
    //    /// <summary>
    //    /// Method used to subscribe to the External method.
    //    /// </summary>
    //    public void SubscribeToExternalMethods(Form mainForm)
    //    {
    //        mainForm.StopEvent += MainForm_StopEvent;
    //    }
    //    /// <summary>
    //    /// Actions performed when the stop events arrives.
    //    /// </summary>
    //    public void MainForm_StopEvent(object sender, EventArgs e)
    //    {
    //        this.IsToStopSorting = true;
    //    }

    //    public void NextStep()
    //    {
    //        throw new NotImplementedException();
    //    }
    //    #endregion
    //}






    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// FORM1
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    //public partial class Form1 : System.Windows.Forms.Form
    //{
    //    #region Fields
    //    int[] arrayOfNumbers;
    //    int numEntries;
    //    int maxNumberOfEntries;
    //    int maxValue;
    //    Graphics g;
    //    bool isFormSizeChanged;
    //    int rectangleWidth;
    //    int paddingFromSideMargins;
    //    Task runningSortTask;
    //    ISortEngine se;
    //    #endregion

    //    #region Properties
    //    #endregion

    //    #region Constructor
    //    public Form1()
    //    {
    //        InitializeComponent();

    //        LoadDefault();
    //    }
    //    #endregion

    //    #region Methods
    //    /// <summary>
    //    /// Loading the default values on form launched.
    //    /// </summary>
    //    private void LoadDefault()
    //    {
    //        if (panelGraphic.Width == 0 || panelGraphic.Height == 0)
    //            return;
    //        // At first initialization the number of entries and their maximum number is defined by the width and height of the panel.
    //        maxNumberOfEntries = panelGraphic.Width;
    //        maxValue = panelGraphic.Height;
    //        // Default number of Entries.
    //        numEntries = maxNumberOfEntries;
    //        trackBarSpeed.SetRange(1, maxNumberOfEntries);
    //        trackBarSpeed.Value = maxNumberOfEntries;
    //        textBoxSpeed.Text = maxNumberOfEntries.ToString();
    //        this.isFormSizeChanged = false;

    //        //g = panelGraphic.CreateGraphics();

    //        //ResetAndRedrawnValues();
    //    }
    //    /// <summary>
    //    /// Create Randm Values.
    //    /// </summary>
    //    private int[] CreateRandomValues()
    //    {
    //        // Creating a random array of values.
    //        int[] arrayOfNumbers = new int[numEntries];
    //        Random random = new Random(); // 120696
    //        for (int i = 0; i < numEntries; i++)
    //            arrayOfNumbers[i] = random.Next(maxValue);
    //        this.arrayOfNumbers = arrayOfNumbers;

    //        return arrayOfNumbers;
    //    }
    //    /// <summary>
    //    /// Draw the rectangles in the panel.
    //    /// </summary>
    //    private void DrawRectangle(int[] arrayOfNumbers)
    //    {
    //        g = panelGraphic.CreateGraphics();

    //        panelGraphic.Invalidate();
    //        panelGraphic.Update();

    //        // Compute the width dimension of each rectangle.
    //        this.rectangleWidth = (int)(Math.Floor(panelGraphic.Width / (double)numEntries)) != 0 ? (int)(Math.Floor(panelGraphic.Width / (double)numEntries)) : 1;
    //        // Compute the residual, not used pixels in the panel.
    //        int residualPixels = panelGraphic.Width - (numEntries * rectangleWidth);
    //        // Divide the residual pixels so that half of them will be the padding from the side borders of the panel.
    //        this.paddingFromSideMargins = residualPixels / 2;

    //        // X: The x-coordinate of the upper-left corner of the rectangle. Y: The y-coordinate of the upper-left corner of the rectangle.
    //        // Width: The width of the rectangle. Height: The height of the rectangle.
    //        for (int i = 0; i < arrayOfNumbers.Length; i++)
    //        {
    //            g.FillRectangle(new SolidBrush(Color.DarkGray), (i * this.rectangleWidth) + paddingFromSideMargins, panelGraphic.Height - arrayOfNumbers[i], rectangleWidth, panelGraphic.Height); // + paddingFromPanel
    //        }
    //    }
    //    /// <summary>
    //    /// Reset and call the creation and drawn of the random values. 
    //    /// </summary>
    //    private void ResetAndRedrawnValues()
    //    {
    //        // If the sorting task is running don't interrupt it.
    //        if (this.runningSortTask?.Status == TaskStatus.Running)
    //            return;

    //        if (isFormSizeChanged)
    //            LoadDefault();
    //        //g.Dispose();
    //        // Create the random array of values and draw them.
    //        int[] arrayOfNumbers = CreateRandomValues();
    //        DrawRectangle(arrayOfNumbers);
    //    }
    //    #endregion

    //    #region Event Handlers
    //    /// <summary>
    //    /// Close the application when the exit button is pressed.
    //    /// </summary>
    //    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    //    {
    //        this.Close();
    //    }
    //    /// <summary>
    //    /// Action to be performed when the sort button is clicked.
    //    /// </summary>
    //    private void buttonSort_Click(object sender, EventArgs e)
    //    {
    //        // If the sorting task is running don't interrupt it.
    //        if (this.runningSortTask?.Status == TaskStatus.Running)
    //            return;

    //        if (se == null)
    //        {
    //            // Create an instance of the Sort Engine. 
    //            this.se = new BubbleSortEngine();
    //            // Call the method used to subscribe to the 
    //            se.SubscribeToExternalMethods(this);
    //        }
    //        // Call the DoWork Method in a separate Task.
    //        this.runningSortTask = Task.Run(() =>
    //        se.DoWork(this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height)
    //        );
    //    }
    //    /// <summary>
    //    /// Action performed when the Stop button is clicked.
    //    /// </summary>
    //    private void buttonStop_Click(object sender, EventArgs e)
    //    {
    //        RaiseStopEvent(sender, e);
    //    }
    //    /// <summary>
    //    /// Action to be performed at the Reset button click. Re-initialization of the array of numbers.
    //    /// </summary>
    //    private void buttonReset_Click(object sender, EventArgs e)
    //    {
    //        ResetAndRedrawnValues();
    //    }
    //    /// <summary>
    //    /// Action to be performed when the scroll bar changes value.
    //    /// </summary>
    //    private void trackBarSpeed_Scroll(object sender, EventArgs e)
    //    {
    //        // Update the numberOfentries based on the trackBarValue.
    //        this.numEntries = trackBarSpeed.Value;

    //        // Update the TextBoxEdit if necessary
    //        if (int.Parse(textBoxSpeed.Text) != trackBarSpeed.Value)
    //            textBoxSpeed.Text = trackBarSpeed.Value.ToString();

    //        // Re-create and re-draw the rectangles.
    //        ResetAndRedrawnValues();
    //    }
    //    /// <summary>
    //    /// Action to be performed when the TextBox value has changed.
    //    /// </summary>
    //    private void textBoxSpeed_TextChanged(object sender, EventArgs e)
    //    {
    //        // Check if the input value is correct.
    //        int intTextBoxValue = -1;
    //        bool isTextBoxInteger = int.TryParse(textBoxSpeed.Text, out intTextBoxValue);

    //        if (isTextBoxInteger == false || intTextBoxValue < 1 || intTextBoxValue > trackBarSpeed.Maximum)
    //            textBoxSpeed.Text = trackBarSpeed.Value.ToString();
    //        else
    //        {
    //            if (intTextBoxValue != trackBarSpeed.Value)
    //            {
    //                trackBarSpeed.Value = intTextBoxValue;
    //                trackBarSpeed_Scroll(sender, e);
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// Action performed when the size of the form has changed.
    //    /// </summary>
    //    protected override void OnSizeChanged(EventArgs e)
    //    {
    //        base.OnSizeChanged(e);

    //        this.isFormSizeChanged = true;
    //        // Every time that the dimension of the form changes, recompute the array of values.
    //        ResetAndRedrawnValues();
    //    }
    //    /// <summary>
    //    /// Raise the event to stop the execution of the sorting algorithm.
    //    /// </summary>
    //    private void RaiseStopEvent(object sender, EventArgs e)
    //    {
    //        StopEvent?.Invoke(sender, e);
    //    }
    //    #endregion


    //    #region Events
    //    /// <summary>
    //    /// Delegate used for stopping the execution of the 
    //    /// </summary>
    //    public delegate void StopEventHandler(object sender, EventArgs e);
    //    public event StopEventHandler StopEvent;
    //#endregion
    //}



    //private void buttonSort_Click(object sender, EventArgs e)
    //{
    //    // If the sorting task is running don't interrupt it.
    //    if (this.runningSortTask?.Status == TaskStatus.Running)
    //        return;

    //    ////if (se == null)
    //    ////{
    //    ////    // Create an instance of the Sort Engine. 
    //    ////    this.se = new BubbleSortEngine(this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height);
    //    ////    // Call the method used to subscribe to the 
    //    ////    se.SubscribeToExternalMethods(this);
    //    ////}

    //    //// Create an instance of the Sort Engine. 
    //    //this.se = new BubbleSortEngine(this.arrayOfNumbers, this.g, this.maxValue, this.rectangleWidth, this.paddingFromSideMargins, this.panelGraphic.Height);
    //    //// Call the method used to subscribe to the 
    //    //se.SubscribeToExternalMethods(this);

    //    //// Call the DoWork Method in a separate Task.
    //    //this.runningSortTask = Task.Run(() =>
    //    //se.DoWork()
    //    //);


    //    // Initialize the background worker.
    //    bgw = new BackgroundWorker();
    //    bgw.WorkerSupportsCancellation = true;
    //    bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
    //    // Running the background worker and pass to it the Sorting Algorithm as the argument. 
    //    bgw.RunWorkerAsync(argument: comboBoxAlgorithmSelector.SelectedItem);
    //}



    // INSERTION SORT (MAYBE ????)
    //// Loop through the previous (temporary sorted) values.
    //for (int j = 0; j < i; j++)
    //{
    //    // Check whether the Stop button is clicked, and the sorting must be stopped.
    //    if (this.IsToStopSorting)
    //        return;

    //    // If the current number is lower than the previous, exchange the two.
    //    if (valuesArray[i] < valuesArray[j])
    //    {
    //        RepaintCurrentBars(i, j);

    //        Thread.Sleep(sleepDuration);

    //        // Exchange the two values.
    //        int tempValue = valuesArray[j];
    //        valuesArray[j] = valuesArray[i];
    //        valuesArray[i] = tempValue;

    //        RepaintCurrentBars(i, j);

    //        Thread.Sleep(sleepDuration);

    //        // Repaint these two selected values in Gray.
    //        g.FillRectangle(this.grayBrush, (i * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[i], this.rectangleWidth, this.panelHeight);
    //        g.FillRectangle(this.grayBrush, (j * this.rectangleWidth) + paddingFromSideMargins, this.panelHeight - valuesArray[j], this.rectangleWidth, this.panelHeight);
    //    }
    //}
}


