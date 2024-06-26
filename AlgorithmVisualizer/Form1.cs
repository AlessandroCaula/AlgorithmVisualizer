using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlgorithmVisualizer
{
    public partial class Form1 : Form
    {
        #region Fields
        int[] arrayOfNumbers;
        int numEntries;
        int maxNumberOfEntries;
        int maxValue;
        Graphics g;
        bool isFormSizeChanged;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();

            LoadDefault();
        }
        #endregion

        #region Methods
        private void LoadDefault()
        {
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
        // Create Random Values and Bars.
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
        // Draw the random values.
        private void DrawRectangle1(int[] arrayOfNumbers)
        {
            g = panelGraphic.CreateGraphics();

            panelGraphic.Invalidate();
            panelGraphic.Update();

            // Compute the width dimension of each rectangle.
            int rectangleWidth = (int)(Math.Round(panelGraphic.Width / (double)numEntries)) != 0 ? (int)(Math.Round(panelGraphic.Width / (double)numEntries)) : 1;
            // Compute the residual, not used pixels in the panel.
            int residualPixels = panelGraphic.Width - (numEntries * rectangleWidth);
            // Divide the residual pixels so that half of them will be the padding from the side borders of the panel.
            int paddingFromSideMargins = residualPixels / 2;

            // X: The x-coordinate of the upper-left corner of the rectangle. Y: The y-coordinate of the upper-left corner of the rectangle.
            // Width: The width of the rectangle. Height: The height of the rectangle.
            for (int i = 0; i < arrayOfNumbers.Length; i++)
            {
                g.FillRectangle(new SolidBrush(Color.Blue), (i * rectangleWidth) + paddingFromSideMargins, panelGraphic.Height - arrayOfNumbers[i], rectangleWidth, panelGraphic.Height); // + paddingFromPanel
            }
        }
        // Reset and recall the creation and drawn of the random values.
        private void ResetAndRedrawnValues()
        {
            if (isFormSizeChanged)
                LoadDefault();

            g.Dispose();
            // Create the random array of values and draw them.
            int[] arrayOfNumbers = CreateRandomValues();
            DrawRectangle1(arrayOfNumbers);
        }
        #endregion

        #region Event Handlers
        // Close the application when the exit button is pressed.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Action to be performed at the Reset button click. Re-initialization of the array of numbers.
        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetAndRedrawnValues();
        }
        // Action to be performed when the scroll bar changes value.
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
        // Action to be performed when the TextBox value has changed.
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
        // Action performed when the size of the form has changed.
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //if (this.arrayOfNumbers != null)
            //    DrawRectangle1(this.arrayOfNumbers);

            this.isFormSizeChanged = true;
            // Every time that the dimension of the form changes, recompute the array of values.
            ResetAndRedrawnValues();
        }
        #endregion
    }
}
