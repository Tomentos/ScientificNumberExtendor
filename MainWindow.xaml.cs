using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScientificNumberExtendor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Global Variable definition
        private char seperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        private string res;
        private float dec;
        private int mul;

        // Start MainWindow
        public MainWindow()
        {
            InitializeComponent();

            // Setting up the default calculation at startup
            Decimal_Input.Text = ("3" + seperator + "14");
            Multiplicator_Input.Text = "0";
            calculation();
        }

        // Decimal Input Text Change Event
        private void Decimal_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Try to Parse the Decimal Input into float
            try
            {
                dec = float.Parse(Decimal_Input.Text);

                // Check if the Decimal Input is double digits
                if (dec >= 10)
                {

                    // Defining Variables for Double Digit Correction
                    string[] oldDec = new string[2];
                    string[] newDec = new string[2];
                    string finDec;

                    // Seperating the Decimal input into two arrays
                    if (dec.ToString().Contains(seperator))
                    {
                        oldDec = new string[2];
                        oldDec = dec.ToString().Split(seperator);
                    }

                    else
                    {
                        oldDec[0] = dec.ToString();
                        oldDec[1] = null;
                    }

                    // Entering corrected values into new string
                    newDec[0] = oldDec[0].Remove(1);
                    newDec[1] = oldDec[0].Remove(0, 1);

                    if (oldDec[1] != null)
                        newDec[1] += oldDec[1];

                    // Parsing new values into Text Input and Global Variable
                    finDec = (newDec[0] + seperator + newDec[1]);
                    Decimal_Input.Text = finDec;
                    dec = float.Parse(finDec);

                    mul = mul + oldDec[0].Length - 1;
                    Multiplicator_Input.Text = mul.ToString();
                }

                calculation();
            }

            //Catch possible Format Exception
            catch (FormatException)
            {
                exception();
            }
        }

        // Multiplicator Input Text Change Event
        private void Multiplicator_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Try to Parse the Decimal Input into int
            try
            {
                mul = int.Parse(Multiplicator_Input.Text);
                calculation();
            }

            //Catch possible Format Exception
            catch (FormatException)
            {
                exception();
            }
        }

        // Result Calculation Function
        private void calculation()
        {
            // Defining variables used throughout the calculation
            string[] split = new string[2];
            int length = 0;

            // Additional variables in case the decimal input contans a seperator
            if (dec.ToString().Contains(seperator)) {
                split = dec.ToString().Split(seperator);
                length = split[1].Length;
            }   

            // When the multiplicator is zero, the result is automatically turned into the decimal input
            if (mul == 0)
            {
                res = dec.ToString();
            }

            // When the amount of decimal numbers are higher than the multiplicator this calculation is executed
            else if (length > mul)
            {
                int difference = split[1].Length - (length - mul);
                string pre = split[1].Remove(difference);
                string after = split[1].Remove(0, difference);

                res = (split[0] + pre + seperator + after);
            }
            
            // When the multiplicator is higher than the amount of decimal numbers, this calculation is executed
            else
            {
                int zeroesAmount = mul - length;

                string zeroes = "";
                for (int i = 0; i != zeroesAmount; i++)
                    zeroes += "0";

                res = (dec.ToString().Replace(seperator.ToString(), "") + zeroes);
            }

            // Write the Output and return the function
            Output.Text = res;
            return;
        }

        // Error Message Print Function
        private void exception()
        {
            Output.Text = "Format Exception\nOne or both of your inputs are invalid. Please check them and correct possible errors.";
            return;
        }
    }
}
