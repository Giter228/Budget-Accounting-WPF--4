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

namespace Diary_WPF
{
    /// <summary>
    /// Логика взаимодействия для AddNewTypeNoteWindow.xaml
    /// </summary>
    public partial class AddNewTypeNoteWindow : Window
    {
        public string type;
        public AddNewTypeNoteWindow()
        {
            InitializeComponent();
        }

        private void Finish_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (NewTypeText.Text == "")
            {
                MessageBox.Show("Ошибка! Тип не введён.");
                return;
            }
            type = NewTypeText.Text;
            Close();
        }
    }
}
