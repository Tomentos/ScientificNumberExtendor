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
        // Variable definition
        private char seperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        private string res;
        private float dec;
        private int mul;

        // Start MainWindow
        public MainWindow()
        {
            InitializeComponent();
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
            string[] split = dec.ToString().Split(seperator);
            int length = split[1].Length;

            // When the multiplicator is zero, the result is automatically made turned to 1
            if (mul == 0)
            {
                res = "1";
            }

            // When the amount of decimal numbers are higher than the multiplicator this calculation is executed
            else if (length > mul)
            {
                int difference = length - mul;
                int seperationLength = split[1].Length - difference;
                string pre = split[1].Remove(seperationLength);
                string after = split[1].Remove(1, seperationLength);

                res = (split[0] + pre + seperator + after);
            }
            
            // When the multiplicator is higher than the amount of decimal numbers, this calculation is executed
            else
            {
                int zeroesAmount = mul - length;

                string zeroes = "";
                for (int i = 0; i != zeroesAmount; i++)
                    zeroes += "0";

                res = (split[0] + split[1] + zeroes);
            }

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
