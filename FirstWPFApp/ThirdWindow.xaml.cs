using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstWPFApp
{
    /// <summary>
    /// Логика взаимодействия для FirstWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        string output = "0";
        char[] operationsigns = {'.', '+', '-', '×', '÷'};
        public ThirdWindow()
        {
            InitializeComponent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void GoToMainWin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private bool NumHasOneDot(string input)
        {
            string[] parts = input.Split(new char[] { '+', '-', '×', '÷' }, StringSplitOptions.RemoveEmptyEntries);
            string currentNumber = parts.Length > 0 ? parts[parts.Length - 1] : "";

            if (currentNumber.Contains("."))
            {
                return true;
            }
            return false;
        }
        
        private async void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            string result_string = output;
       
            if (result_string.Length > 1 && Array.Exists(operationsigns, c => c == result_string[result_string.Length - 1]))
            {
                result_string = result_string.Substring(0, result_string.Length - 1);
                output = result_string;
            }

            if (result_string == "-") 
                output = "0";

            result_string = output.Replace('÷', '/')
                                  .Replace('×', '*');

            if (result_string.Contains("/0"))
            {
                OutputText.Text = "Error. div by 0";
                await Task.Delay(1000);
                output = "0";
            }
            else
            {
                if (output.Length > 11)
                {
                    OutputText.Text = "OverFlow";
                    await Task.Delay(1000);
                    output = "0";
                }
                else
                {
                    result_string = new DataTable().Compute(result_string, null).ToString();         
                    result_string = result_string.Replace(',', '.');
                    output = result_string;                
                }
            }
            OutputText.Text = output;
        }       
        private async void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            string operation = clickedButton.Name;

            if (output.Length > 12)
            {
                OutputText.Text = "OverFlow";
                await Task.Delay(1000);
                output = "0";
                OutputText.Text = output;
            }
            else
            {
                switch (operation)
                {
                    case "ClearButton":
                        output = "0";
                        OutputText.Text = output;
                        break;

                    case "ClearEntryButton":
                        if (output.Length > 1)
                        {
                            output = output.Substring(0, output.Length - 1);
                            OutputText.Text = output;
                            break;
                        }
                        else
                        {
                            output = "0";
                            OutputText.Text = output;
                            break;
                        }

                    case "ChangeSignButton":
                        if (output.Length < 1)
                            break;
                        if (output.StartsWith("-"))
                        {
                            output = output.Substring(1);
                            OutputText.Text = output;
                        }                         
                        else
                        {
                            string result = "-" + output;
                            output = result;
                            OutputText.Text = output;
                            break;
                        }
                        break;
                     

                    case "ZeroButton":
                        if (OutputText.Text == "0")
                            break;
                        output += "0";
                        OutputText.Text = output;
                        break;

                    case "OneButton":
                        if (OutputText.Text == "0")
                            output = "1";
                        else
                            output += "1";
                        OutputText.Text = output;
                        break;

                    case "TwoButton":
                        if (OutputText.Text == "0")
                            output = "2";
                        else
                            output += "2";
                        OutputText.Text = output;
                        break;

                    case "ThreeButton":
                        if (OutputText.Text == "0")
                            output = "3";
                        else
                            output += "3";
                        OutputText.Text = output;
                        break;

                    case "FourButton":
                        if (OutputText.Text == "0")
                            output = "4";
                        else
                            output += "4";
                        OutputText.Text = output;
                        break;

                    case "FiveButton":
                        if (OutputText.Text == "0")
                            output = "5";
                        else
                            output += "5";
                        OutputText.Text = output;
                        break;

                    case "SixButton":
                        if (OutputText.Text == "0")
                            output = "6";
                        else
                            output += "6";
                        OutputText.Text = output;
                        break;

                    case "SevenButton":
                        if (OutputText.Text == "0")
                            output = "7";
                        else
                            output += "7";
                        OutputText.Text = output;
                        break;

                    case "EightButton":
                        if (OutputText.Text == "0")
                            output = "8";
                        else
                            output += "8";
                        OutputText.Text = output;
                        break;

                    case "NineButton":
                        if (OutputText.Text == "0")
                            output = "9";
                        else
                            output += "9";
                        OutputText.Text = output;
                        break;

                    case "PlusButton":
                        if (Array.Exists(operationsigns, c => c == output[output.Length - 1]))
                        {
                            output = output.Substring(0, output.Length - 1);
                            output += "+";
                            OutputText.Text = output;
                            break;
                        }
                        output += "+";
                        OutputText.Text = output;
                        break;

                    case "MinusButton":
                        if (Array.Exists(operationsigns, c => c == output[output.Length - 1]))
                        {
                            output = output.Substring(0, output.Length - 1);
                            output += "-";
                            OutputText.Text = output;
                            break;
                        }
                        output += "-";
                        OutputText.Text = output;
                        break;

                    case "MultiplyButton":
                        if (Array.Exists(operationsigns, c => c == output[output.Length - 1]))
                        {
                            output = output.Substring(0, output.Length - 1);
                            output += "×";
                            OutputText.Text = output;
                            break;
                        }
                        output += "×";
                        OutputText.Text = output;
                        break;

                    case "DivideButton":
                        if (Array.Exists(operationsigns, c => c == output[output.Length - 1]))
                        {
                            output = output.Substring(0, output.Length - 1);
                            output += "÷";
                            OutputText.Text = output;
                            break;
                        }
                        output += "÷";
                        OutputText.Text = output;
                        break;

                    case "DotButton":
                        if (NumHasOneDot(output))
                            break;

                        if (Array.Exists(operationsigns, c => c == output[output.Length - 1]))
                        {
                            output = output.Substring(0, output.Length - 1);
                            output += ".";
                            OutputText.Text = output;
                            break;
                        }
                        output += ".";
                        OutputText.Text = output;
                        break;
                }
            }          
        }
    }
}
