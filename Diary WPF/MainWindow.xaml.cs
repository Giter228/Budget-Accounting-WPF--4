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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diary_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string way = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static Note example = new Note("Example name", "Example description", selectedDate);

        public static List<Note> notes = De_Serialize_.Deserialize<Note>(way + "\\notes", example);
        public static DateTime selectedDate = DateTime.Today;

        public MainWindow()
        {
            InitializeComponent();
            DatePick.SelectedDate = DateTime.Today;
            
            CheckLBX();
        }
        private void NotesLBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NoteName.Text = NotesLBX.SelectedItem.ToString();
            foreach (var _ in notes)
            {
                if (NoteName.Text == _.Name)
                {
                    NoteDesc.Text = _.Description;
                }
            }
            CheckLBX();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (NoteName.Text == "" || NoteDesc.Text == "")
            {
                MessageBox.Show("Ошибка. Поля не заполнены");
                return;
            }
            if (NotesLBX.Items.Contains(NoteName.Text))
            {
                MessageBox.Show("Заметка с данным названием уже существует. Если вы хотите её отредактировать, измените данные в полях, нажав на «Сохранить»");
                return;
            }
            
            Note note = new Note(NoteName.Text, NoteDesc.Text, (DateTime)DatePick.SelectedDate);
            notes.Add(note);

            foreach (var _ in notes)
            {
                if (DatePick.SelectedDate == _.Date)
                {
                    if (NotesLBX.Items.Contains(_.Name)) { }
                    else NotesLBX.Items.Add(_.Name);
                }
            }
            NotesLBX.SelectedIndex = notes.Count - 1;

            De_Serialize_.Serialize(notes, way + "\\notes\\allNotes.json");
            
            NoteName.Text = null;
            NoteDesc.Text = null;

            CheckLBX();
        }

        private void CheckLBX()
        {
            if (NotesLBX.Items.Count == 0 || NotesLBX.SelectedItem == null)
            {
                DeleteBTN.IsEnabled = false;
                SaveBTN.IsEnabled = false;
            }
            else
            {
                DeleteBTN.IsEnabled = true;
                SaveBTN.IsEnabled = true;
            }
        }

        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = Convert.ToDateTime(DatePick.Text);
            
            LBX_ItemsCheck();
        }

        private void LBX_ItemsCheck()
        {
            NotesLBX.Items.Clear();

            foreach (var _ in notes)
            {
                if (_.Date == selectedDate)
                {
                    NotesLBX.Items.Add(_.Name);
                }
            }
        }
        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (NoteName.Text == "" || NoteDesc.Text == "")
            {
                MessageBox.Show("Ошибка. Поля не заполнены");
                return;
            }

            string selectedNote = NotesLBX.SelectedItem.ToString();
            foreach (var _ in notes)
            {
                if (_.Name == selectedNote)
                {
                    _.Name = NoteName.Text;
                    _.Description = NoteDesc.Text;
                }
            }
            De_Serialize_.Serialize(notes, way + "\\notes\\allNotes.json");
            
            NoteName.Text = null;
            NoteDesc.Text = null;
            
            LBX_ItemsCheck();
            CheckLBX();
        }
        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            foreach (var _ in notes)
            {
                if (_.Name == NotesLBX.SelectedItem.ToString())
                {
                    index = notes.IndexOf(_);
                }
            }
            NotesLBX.SelectedIndex = notes.Count - 1;
            notes.RemoveAt(index);
            De_Serialize_.Serialize(notes, way + "\\notes\\allNotes.json");

            NoteName.Text = null;
            NoteDesc.Text = null;

            LBX_ItemsCheck();
            CheckLBX();
        }
    }
}
