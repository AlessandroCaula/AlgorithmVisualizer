using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;

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
            numEntries = maxNumberOfEntries / 2;
            trackBarSpeed.SetRange(1, maxNumberOfEntries);
            trackBarSpeed.Value = numEntries;
            textBoxSpeed.Text = numEntries.ToString();

            this.isFormSizeChanged = false;
            // Create a graphic subject on the Form.
            //g = panelGraphic.CreateGraphics();
            //int[] randomValues = CreateRandomValues();
            //DrawRectangle(randomValues);
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

        // Compute the ActualNumberOfEntries based on the dimension and space available in the panel. 
        private void ComputeActualNumberOfEntries()
        {

        }

        // Draw a rectangle every random value.
        private void DrawRectangle(int[] arrayOfNumbers)
        {
            g = panelGraphic.CreateGraphics();

            panelGraphic.Invalidate();
            panelGraphic.Update();

            // Compute the width dimension of each rectangle.
            int rectangleWidth = (int)(Math.Round(panelGraphic.Width / (double)numEntries));
            // Compute the residual, not used pixels in the panel.
            int residualPixels = panelGraphic.Width - (numEntries * rectangleWidth);
            // Find to how many rectangle I can increase the dimension.
            int nIncreasedDimRectangle = (int)(Math.Floor(residualPixels / (double)rectangleWidth));
            // Compute every how many rectangle increase the dimension of one rectangle.
            int increaseFreq = (int)(Math.Ceiling(this.numEntries / (double)nIncreasedDimRectangle));

            //   X: The x-coordinate of the upper-left corner of the rectangle.
            //   Y: The y-coordinate of the upper-left corner of the rectangle.
            //   Width: The width of the rectangle.
            //   Height: The height of the rectangle.
            int count = 0;
            int totalWidthUsed = 0;
            for (int i = 0; i < arrayOfNumbers.Length; i++)
            {
                int increaseWidth = 0;
                if (count == increaseFreq)
                {
                    increaseWidth = rectangleWidth;
                    count = 0;
                }
                g.FillRectangle(new SolidBrush(Color.Blue), totalWidthUsed, panelGraphic.Height - arrayOfNumbers[i], rectangleWidth + increaseWidth, panelGraphic.Height); // + paddingFromPanel
                count++;
                totalWidthUsed += rectangleWidth + increaseWidth;
            }
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
            if (isFormSizeChanged)
                LoadDefault();

            g = panelGraphic.CreateGraphics();
            // Set constraints for the numbers of random numbers created and the maximum values that can have based on the panel properties.
            int numEntries = this.numEntries;
            int maxValue = this.maxValue;
            // Create the random array of values and draw them.
            int[] arrayOfNumbers = CreateRandomValues();
            DrawRectangle(arrayOfNumbers);
        }
        // Action to be performed when the scroll bar changes value.
        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            // Update the numberOfentries based on the trackBarValue.
            this.numEntries = trackBarSpeed.Value;

            //ComputeActualNumberOfEntries();

            // Reset the number of entries.
            //trackBarSpeed.Value = this.numEntries;

            // Update the TextBoxEdit if necessary
            if (int.Parse(textBoxSpeed.Text) != trackBarSpeed.Value)
                textBoxSpeed.Text = trackBarSpeed.Value.ToString();

            // Re-create and re-draw the rectangles.
            int[] arrayOfNumbers = CreateRandomValues();
            DrawRectangle(arrayOfNumbers);
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
                    trackBarSpeed.Value = intTextBoxValue;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.arrayOfNumbers != null)
                DrawRectangle(this.arrayOfNumbers);

            this.isFormSizeChanged = true;
        }
        #endregion
    }
}
