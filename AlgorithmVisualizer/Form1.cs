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
            maxNumberOfEntries = panelGraphic.Width / 2;
            maxValue = panelGraphic.Height;
            // Default number of Entries.
            numEntries = maxNumberOfEntries / 2;
            trackBarSpeed.SetRange(1, panelGraphic.Width / 2);
            trackBarSpeed.Value = numEntries;
            textBoxSpeed.Text = numEntries.ToString();

            int[] randomValues = CreateRandomValues();
        }
        // Create Random Values and Bars.
        private int[] CreateRandomValues()
        {
            // Creating a random array of values.
            int[] arrayOfNumbers = new int[numEntries];
            Random random = new Random();
            for (int i = 0; i < numEntries; i++)
                arrayOfNumbers[i] = random.Next(maxValue);
            this.arrayOfNumbers = arrayOfNumbers;

            return arrayOfNumbers;
        }
        // Draw a rectangle every random value.
        private void DrawRectangle(int[] arrayOfNumbers)
        {
            // Compute the width dimension of each rectangle.
            int rectangleWidth = (int)(panelGraphic.Width - (panelGraphic.Width * 0.1) / numEntries);

            //   x: The x-coordinate of the upper-left corner of the rectangle.
            //   y: The y-coordinate of the upper-left corner of the rectangle.
            //   width: The width of the rectangle.
            //   height: The height of the rectangle.

            for (int i = 0; i < arrayOfNumbers.Length; i++)
            {
                //g.FillRectangle(new SolidBrush, i)
            }
        }
        #endregion

        #region Event Handlers
        // Cloes the application when the exit button is pressed.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Action to be performed at the Reset button click. Re-intialization of the array of numbers.
        private void buttonReset_Click(object sender, EventArgs e)
        {
            // Create a graphic subject on the Form.
            g = panelGraphic.CreateGraphics();
            // Set constraints for the numbers of random numbers created and the maximum values that can have based on the panel properties.
            int numEntries = this.numEntries;
            int maxValue = this.maxValue;
            // Initialize the background of the panel to the black color.
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, panelGraphic.Width, panelGraphic.Height);
            // Create the random array of values
            int[] arrayOfNumbers = CreateRandomValues();
        }
        // Action to be performed when the scroll bar changes value.
        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            // Reset the number of entries.
            this.numEntries = trackBarSpeed.Value;
            // Update the TextBoxEdit if necessary
            if (int.Parse(textBoxSpeed.Text) != trackBarSpeed.Value)
                textBoxSpeed.Text = trackBarSpeed.Value.ToString();

            _ = CreateRandomValues();
        }

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
        #endregion
    }
}
