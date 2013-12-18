using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        private int secretNumber;

        public Form1()
        {
            InitializeComponent();
            secretNumber = new Random().Next(10);
            Console.Error.WriteLine("nombre secret " + secretNumber);
            resultTextBox.Text = "essayer de trouver un chiffre entre 0 et 10...\n";
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            int value=int.Parse(inputTextBox.Text);
            if (value.Equals(secretNumber))
            {
                resultTextBox.Text += "\n c'est gagné";
            }
            else
            {

                if (value.CompareTo(secretNumber) > 0)
                {
                    resultTextBox.Text += "\n" + value + " trop grand ";
                }
                else
                {
                    resultTextBox.Text += "\n" + value + " trop petit ";
                }
            }
        }
    }
}
