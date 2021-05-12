using ReinforcementIronCalculator.Factories;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace ReinforcementIronCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private string customer;
        private int count;
        private int reinforcementNumber;
        private double length;
        private double multiplier = 1;
        private double totalWeight = 0;
        private bool isFi = false;
        public MainWindow()
        {
            InitializeComponent();
            PrintTotalWeight();
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

        private void CustomerBox(object sender, TextChangedEventArgs e)
        {
            this.customer = Customer.Text;
        }

        private void CountBox(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.count = int.Parse(Count.Text);
            }
            catch (Exception)
            {
            }
        }

        private void ReinforcementBox(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.reinforcementNumber = int.Parse(Number.Text);

            }
            catch (Exception)
            {
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.reinforcementNumber = 8;
            isFi = true;
            Number.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Number.Text = string.Empty;
            isFi = false;
            Number.Visibility = Visibility.Visible;
        }

        private void LengthBox(object sender, TextChangedEventArgs e)
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

        private void MultiplierBox(object sender, SelectionChangedEventArgs e)
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

        private void CalculateButton(object sender, RoutedEventArgs e)
        {
            ReinforcementFactory factory = new ReinforcementFactory();
            var weightForCalculation = factory.Create(reinforcementNumber, count, length, isFi);
            if (weightForCalculation == null)
            {
                MessageBox.Show("Невалидна стойност!");
            }
            else
            {
                double weight = Math.Round(weightForCalculation.CalculateWeight() * this.multiplier, 2);
                this.totalWeight += weight;

                string message = GetOutputMessage(this.reinforcementNumber, this.count, this.length, weight, this.isFi);

                if (ListBox.Items.Count == 0)
                {
                    ListBox.Items.Add(customer);
                }
                ListBox.Items.Add(message);

                PrintTotalWeight();
            }

            Customer.IsReadOnly = true;

            ResetPrimaryAttributes();
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem == null || ListBox.SelectedItem.ToString() == this.customer)
            {
                MessageBox.Show("Невалиден елемент за премахване!");
            }
            else
            {
                var selectedItem = ListBox.SelectedItem.ToString();
                var splited = selectedItem.Split(": ", StringSplitOptions.RemoveEmptyEntries);
                var weightToRemove = double.Parse(splited[splited.Length - 1]);
                ListBox.Items.Remove(selectedItem);

                this.totalWeight -= weightToRemove;
                PrintTotalWeight();
            }
        }

        private void SaveFileButton(object sender, RoutedEventArgs e)
        {
            if (ListBox.Items.Count <= 0)
            {
                MessageBox.Show("Няма нищо за записване!");
                return;
            }

            string path = $"{Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory)}\\Заявка {this.customer} - {DateTime.Now.ToString("dd-MM-yyyy")}.txt";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
            foreach (var item in ListBox.Items)
            {
                SaveFile.WriteLine(item);
            }
            SaveFile.WriteLine();
            SaveFile.WriteLine($"Всичко килограми: {this.totalWeight:F2}");
            SaveFile.Close();

            ResetAllAttributes();

            MessageBox.Show("Запазено!");
        }

        private void PrintTotalWeight()
        {
            TotalWeight.Text = $"Всичко килограми: {this.totalWeight:F2}";
        }

        private string GetOutputMessage(int reinforcementNumber, int count, double length, double weight, bool isFi)
        {
            if (isFi)
            {
                return $"Арматурно желязо Ф{this.reinforcementNumber:D2} * {this.length:F2} Брой:{this.count:D2} Килограми: {weight:F2}";
            }
            return $"Арматурно желязо №{this.reinforcementNumber:D2} * {this.length:F2} Брой:{this.count:D2} Килограми: {weight:F2}";
        }

        private void ResetPrimaryAttributes()
        {
            Count.Text = string.Empty;
            Number.Text = string.Empty;
            Length.Text = string.Empty;
            this.count = 0;
            this.reinforcementNumber = 0;
            this.length = 0;
            this.Checkbox.IsChecked = false;
        }

        private void ResetAllAttributes()
        {
            Count.Text = string.Empty;
            Number.Text = string.Empty;
            Length.Text = string.Empty;
            ListBox.Items.Clear();
            Customer.Text = string.Empty;
            this.count = 0;
            this.reinforcementNumber = 0;
            this.length = 0;
            this.totalWeight = 0;

            PrintTotalWeight();
        }


    }
}
