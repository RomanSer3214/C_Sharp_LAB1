using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class SecondWindow : Window
    {
        private bool _isXTurn = true;
        public SecondWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
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
        private void InitializeComboBoxes()
        {
            foreach (var child in gameGrid.Children)
            {
                if (child is ComboBox comboBox)
                {
                    comboBox.Items.Add("×");
                    comboBox.Items.Add("○");
                }
            }
        }
        private void ClearComboBoxes()
        {
            foreach (var child in gameGrid.Children)
            {
                if (child is ComboBox comboBox)
                {
                    comboBox.SelectedItem = null;
                    comboBox.IsEnabled = true;
                    _isXTurn = true;
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox == null || comboBox.SelectedItem == null)
                return;

            string selectedValue = comboBox.SelectedItem.ToString();

            if (selectedValue == "×" && !_isXTurn || selectedValue == "○" && _isXTurn)
            {
                MessageBox.Show("Не ваш хід!");
                comboBox.SelectedItem = null;
                return;
            }

            comboBox.IsEnabled = false;

            _isXTurn = !_isXTurn;

            CheckWinner();
        }
        private async void CheckWinner()
        {
            string[,] grid = new string[5, 5];
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var comboBox = (ComboBox)FindName($"cb{row}{col}");
                    grid[row, col] = comboBox.SelectedItem as string;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    if (grid[i, j] != null && grid[i, j] == grid[i, j + 1] && grid[i, j + 1] == grid[i, j + 2] && grid[i, j + 2] == grid[i, j + 3])
                    {
                        await Task.Delay(1);
                        MessageBox.Show($"{grid[i, j]} виграв!");
                        ClearComboBoxes();
                        return;
                    }

                    if (grid[j, i] != null && grid[j, i] == grid[j + 1, i] && grid[j + 1, i] == grid[j + 2, i] && grid[j + 2, i] == grid[j +3, i])
                    {
                        await Task.Delay(1);
                        MessageBox.Show($"{grid[j, i]} виграв!");
                        ClearComboBoxes();
                        return;
                    }
                }
            }

            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    if (grid[i, j] != null && grid[i, j] == grid[i + 1, j + 1] && grid[i + 1, j + 1] == grid[i + 2, j + 2] && grid[i + 2, j + 2] == grid[i + 3, j + 3])
                    {
                        await Task.Delay(1);
                        MessageBox.Show($"{grid[i, j]} виграв!");
                        ClearComboBoxes();
                        return;
                    }   

                    if (grid[i, 4 - j] != null && grid[i, 4 - j] == grid[i + 1, 3 - j] && grid[i + 1, 3 - j] == grid[i + 2, 2 - j] && grid[i + 2, 2 - j] == grid[i + 3, 1 - j])
                    {
                        await Task.Delay(1);
                        MessageBox.Show($"{grid[i, 4 - j]} виграв!");
                        ClearComboBoxes();
                        return;
                    }
                }
            }

            bool allDisabled = true;

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var comboBox = (ComboBox)FindName($"cb{row}{col}");

                    if (comboBox != null && comboBox.IsEnabled)
                    {
                        allDisabled = false;
                        break;
                    }
                }

                if (!allDisabled)
                    break;
            }

            if (allDisabled)
            {
                MessageBox.Show("Нічия"); 
                ClearComboBoxes();
            }         
        }
    }
}
