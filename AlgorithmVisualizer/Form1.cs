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
            numEntries = panelGraphic.Width / 2;
            maxValue = panelGraphic.Height;
            trackBarSpeed.SetRange(1, panelGraphic.Width / 2);
            trackBarSpeed.Value = numEntries / 2;
        }
        // Create Random Values and Bars.
        private void CreateRandomValuesBars()
        {
            // If the arrayOfNumbers is empty recreate the entire array of values. 

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
            // Creating a random array of values.
            arrayOfNumbers = new int[numEntries];
            Random random = new Random();
            for (int i = 0; i < numEntries; i++)
                arrayOfNumbers[i] = random.Next(maxValue);

            // Retrieve the width of each of the bar/rectangle.
            int rectangleWidth = (int)(panelGraphic.Width / numEntries != 0 ? numEntries : 1);
            // Drawing a bars for each of the random number in the array.
            for (int i = 0; i < numEntries; i++)
            {
                // Retrieve the dimension of each of the bar.
                //g.DrawRectangle(new SolidBrush(Color.White), new Rectangle());
            }
        }
        // Action to be performed when the scroll bar changes value.
        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            this.numEntries = trackBarSpeed.Value;
            
            CreateRandomValuesBars();
        }
        #endregion
    }
}
