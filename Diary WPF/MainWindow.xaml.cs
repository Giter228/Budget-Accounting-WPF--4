using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Diary_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> types = new List<string>();
        private static string way = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Budget";

        List<budgetInput> budgetOutputs = De_Serialize_.Deserialize<budgetInput>(way);
        List<budgetInput> budgetsNow = new List<budgetInput>();

        DateTime selectedDate = DateTime.Today;

        private string nameCheck;
        private string typeCheck;
        private string balanceCheck;
        
        public MainWindow()
        {
            InitializeComponent();
            
            DatePick.SelectedDate = selectedDate;
            Fill_DataGrid();
        }

        private void AddNote_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (Input_NoteName_Box.Text == "" || ComboBox.SelectedItem == null || Input_Money_Box.Text == "")
            {
                MessageBox.Show("Не все поля выбраны и/или заполнены.");
                return;
            }

            double sum;
            if (double.TryParse(Input_Money_Box.Text, out sum))
            {
                budgetOutputs.Add(new budgetInput(Input_NoteName_Box.Text, ComboBox.SelectedValue.ToString(), Convert.ToInt32(Input_Money_Box.Text), Convert.ToDateTime(DatePick.Text)));

                Fill_DataGrid();

                Input_NoteName_Box.Text = null;
                ComboBox.Text = null;
                Input_Money_Box.Text = null;

                De_Serialize_.Serialize(budgetOutputs, way + "\\Budgets.json");
            }
            else MessageBox.Show("Ошибка! В поле суммы введите число.");
        }

        private void Fill_DataGrid()
        {
            int result = 0;
            budgetsNow.Clear();
            foreach (var item in budgetOutputs)
            {
                if (item.Date == selectedDate)
                {
                    budgetsNow.Add(item);
                    result += item.Balance;
                }

                if (!types.Contains(item.TypeNote))
                {
                    types.Add(item.TypeNote);
                }
            }
            ComboBox.ItemsSource = null;
            ComboBox.ItemsSource = types;
            
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = budgetsNow;

            Result.Text = $"Итог: {result}";
        }

        private void Create_NewTypeNote_BTN_Click(object sender, RoutedEventArgs e)
        {
            AddNewTypeNoteWindow window = new AddNewTypeNoteWindow();
            window.ShowDialog();

            if (window.type != null)
            {
                if (window.type.Trim() != null && window.type.Trim() != "")
                {
                    ComboBox.ItemsSource = null;
                    types.Add(window.type.Trim());

                    ComboBox.ItemsSource = types;
                    ComboBox.SelectedValue = window.type;
                }
            }
        }

        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = Convert.ToDateTime(DatePick.Text);

            Input_NoteName_Box.Text = null;
            Input_Money_Box.Text = null;

            Fill_DataGrid();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            budgetInput selected = DataGrid.SelectedItem as budgetInput;

            if (selected != null)
            {
                Input_NoteName_Box.Text = selected.Name;
                ComboBox.SelectedValue = selected.TypeNote;
                Input_Money_Box.Text = selected.Balance.ToString();
                
                nameCheck = Input_NoteName_Box.Text;
                typeCheck = ComboBox.SelectedValue.ToString();
                balanceCheck = Input_Money_Box.Text;
            }
        }

        private void EditNote_Btn_Click(object sender, RoutedEventArgs e)
        {
            CheckSelectedItem();

            if (nameCheck == Input_NoteName_Box.Text && typeCheck == ComboBox.SelectedValue.ToString() && balanceCheck == Input_Money_Box.Text)
            {
                MessageBox.Show("Изменения не найдены");
                return;
            }

            int index = 0;
            foreach (var item in budgetOutputs)
            {
                if (nameCheck == item.Name && typeCheck == item.TypeNote && balanceCheck == item.Balance.ToString())
                {
                    budgetOutputs[index] = new budgetInput(Input_NoteName_Box.Text, ComboBox.SelectedValue.ToString(), Convert.ToInt32(Input_Money_Box.Text), Convert.ToDateTime(DatePick.Text));

                    Fill_DataGrid();

                    Input_NoteName_Box.Text = null;
                    ComboBox.Text = null;
                    Input_Money_Box.Text = null;

                    De_Serialize_.Serialize(budgetOutputs, way + "\\Budgets.json");
                    return;
                }
                index++;
            }
        }
        private void Delete_BTN_Click(object sender, RoutedEventArgs e)
        {
            CheckSelectedItem();

            int index = 0;
            foreach (var item in budgetOutputs)
            {
                if (nameCheck == item.Name && typeCheck == item.TypeNote && balanceCheck == item.Balance.ToString())
                {
                    budgetOutputs.RemoveAt(index);

                    Fill_DataGrid();

                    Input_NoteName_Box.Text = null;
                    ComboBox.Text = null;
                    Input_Money_Box.Text = null;

                    De_Serialize_.Serialize(budgetOutputs, way + "\\Budgets.json");
                    return;
                }
                index++;
            }
        }
        private void CheckSelectedItem()
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали пункта для изменения");
                return;
            }
        }

    }
}
