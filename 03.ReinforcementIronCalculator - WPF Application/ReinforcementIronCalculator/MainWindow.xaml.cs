using Microsoft.Office.Interop.Excel;
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
    public partial class MainWindow : System.Windows.Window
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

        private void CustomerBox(object sender, TextChangedEventArgs e)
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
                double weight = Math.Round(weightForCalculation.CalculateWeight() * this.multiplier, 2);
                this.totalWeight += weight;

                string message = $"Reinforcement Number: {this.reinforcementNumber} Count: {this.count} Length: {this.length} Weight: {weight:F2}";
                PrintTotalWeight();

                if (ListBox.Items.Count == 0)
                {
                    ListBox.Items.Add(customer);
                }
                ListBox.Items.Add(message);
            }

            Customer.IsReadOnly = true;

            ResetPrimaryAttributes();
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

        private void EditButton(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem == null || ListBox.SelectedItem.ToString() == this.customer)
            {
                MessageBox.Show("Invalid element to remove!");
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

            PrintTotalWeight();
        }

        private void PrintTotalWeight()
        {
            TotalWeight.Text = $"Total Weight: {this.totalWeight:F2}";
        }

        private void GenerateExcelFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            DateTime currentDate = DateTime.Now;

            ws.Range["A1:A3"].Value = "Who is number one? :)";
            ws.Range["A4"].Value = "vitoshacademy.com";
            ws.Range["A5"].Value = currentDate;
            ws.Range["B6"].Value = "Tommorow's date is: =>";
            ws.Range["C6"].FormulaLocal = "= A5 + 1";
            ws.Range["A7"].FormulaLocal = "=SUM(D1:D10)";
            for (int i = 1; i <= 10; i++)
                ws.Range["D" + i].Value = i * 2;

            wb.SaveAs($"{Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory)}\\Заявка {this.customer} - {DateTime.Now.ToString("dd-MM-yyyy")}.xlsx");
        }
    }
}
