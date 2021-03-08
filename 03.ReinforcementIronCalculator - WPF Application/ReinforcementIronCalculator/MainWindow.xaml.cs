using ReinforcementIronCalculator.Factories;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReinforcementIronCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string customer;
        private int count;
        private int reinforcementNumber;
        private double length;
        private double multiplier = 1;
        private double totalWeight = 0;
        public MainWindow()
        {
            InitializeComponent();
            TotalWeight.Text = $"Total Weight: {this.totalWeight:F2}";
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DecimalNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.count = int.Parse(Count.Text);
            }
            catch (Exception)
            {
            }
        }

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.reinforcementNumber = int.Parse(Number.Text);

            }
            catch (Exception)
            {
            }
        }

        private void Length_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string doubleNumber = Length.Text.Replace(',', '.');
                this.length = double.Parse(doubleNumber);
            }
            catch (FormatException)
            {
                Length.Text = string.Empty;
            }
        }

        private void Customer_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.customer = Customer.Text;
        }

        private void CalculateButton(object sender, RoutedEventArgs e)
        {
            ReinforcementFactory factory = new ReinforcementFactory();
            var weightForCalculation = factory.Create(reinforcementNumber, count, length);
            if (weightForCalculation == null)
            {
                MessageBox.Show("Invalid Data");
            }
            else
            {
                double weight = Math.Round(weightForCalculation.CalculateWeight() * this.multiplier,2);
                this.totalWeight += weight;

                string message = $"Reinforcement Number: {this.reinforcementNumber} Count: {this.count} Length:{this.length} Weight:{weight:F2}";
                TotalWeight.Text = $"Total Weight: {this.totalWeight:F2}";

                if (ListBox.Items.Count == 0)
                {
                    ListBox.Items.Add(customer);
                }
                ListBox.Items.Add(message);
            }

            ResetPrimaryAttributes();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string box = ComboBox.SelectedItem.ToString();
            switch (box)
            {
                case "System.Windows.Controls.ComboBoxItem: x1.1":
                    multiplier = 1.1;
                    break;
                case "System.Windows.Controls.ComboBoxItem: x1.3":
                    multiplier = 1.3;
                    break;
                default:
                    multiplier = 1;
                    break;
            }
        }

        private void SaveFileButton(object sender, RoutedEventArgs e)
        {
            if (ListBox.Items.Count <= 0)
            {
                MessageBox.Show("Noting To Save!");
                return;
            }

            string path = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory)}\\Заявка {this.customer} - {DateTime.Now.ToString("dd-MM-yyyy")}.txt";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
            foreach (var item in ListBox.Items)
            {
                SaveFile.WriteLine(item);
            }
            SaveFile.WriteLine();
            SaveFile.WriteLine($"Total Weight: {this.totalWeight:F2}");
            SaveFile.Close();

            ResetAllAttributes();

            MessageBox.Show("Program saved!");
        }

        private void ResetPrimaryAttributes()
        {
            Count.Text = string.Empty;
            Number.Text = string.Empty;
            Length.Text = string.Empty;
            this.count = 0;
            this.reinforcementNumber = 0;
            this.length = 0;
        }

        private void ResetAllAttributes()
        {
            Count.Text = string.Empty;
            Number.Text = string.Empty;
            Length.Text = string.Empty;
            ListBox.Items.Clear();
            Customer.Text = "";
            this.count = 0;
            this.reinforcementNumber = 0;
            this.length = 0;
            this.totalWeight = 0;

            TotalWeight.Text = $"Total Weight: {this.totalWeight:F2}";

        }

    }
}
