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
using DAL;

namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for AddDiscipline.xaml
    /// </summary>
    public partial class AddDiscipline : Window
    {
        bool isSaved = true;
        StudentService stud;
        public AddDiscipline(StudentService stud)
        {
            InitializeComponent();
            this.stud = stud;
            foreach (string item in stud.GetDisciplines)
            {
                ComboDiscipName.Items.Add(item);
            }
        }

        private void add_ocin_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(DiscipName.Text))
            {
                MessageBox.Show("Введіть назву предмета", "Помилка", MessageBoxButton.OK);
            }
            else if (stud.GetDisciplines.Any(x => (String.Compare(x, DiscipName.Text, true) == 0)))
            {
                MessageBox.Show("Предмет вже в базі", "Помилка", MessageBoxButton.OK);
            }
            else
            {
                stud.AddDiscipline(DiscipName.Text);
                ComboDiscipName.Items.Add(DiscipName.Text);
                SaveDisciplines.IsEnabled = true;
                DiscipName.Clear();
                isSaved = false;
                MessageBox.Show("Дисципліну додано", "Info", MessageBoxButton.OK);
            }

        }

        private void DelDisc_Click(object sender, RoutedEventArgs e)
        {
            if (ComboDiscipName.SelectedIndex != -1)
            {
                stud.GetDisciplines.Remove(ComboDiscipName.Text);
                ComboDiscipName.Items.RemoveAt(ComboDiscipName.SelectedIndex);
                SaveDisciplines.IsEnabled = true;
                isSaved = false;
                MessageBox.Show("Дисципліну видалено", "Info", MessageBoxButton.OK);
            }
            else if (ComboDiscipName.Items.Count == 0)
            {
                MessageBox.Show("Немає дисциплін у базі", "Error", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Оберіть дисципліну", "Error", MessageBoxButton.OK);
            }
        }

        private void SaveDisciplines_Click(object sender, RoutedEventArgs e)
        {
            stud.SaveDisciplines();
            isSaved = true;
            MessageBox.Show("Зміни збережено", "Info", MessageBoxButton.OK);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult dialog;
            if (!isSaved)
            {
                dialog = MessageBox.Show("Не збережені зміни після закриття програми буде втрачено. Зберегти зміни \"Так\". Продовжити без збереження \"Ні\"", "Warning", MessageBoxButton.YesNoCancel);
                if (dialog == MessageBoxResult.Cancel)
                {
                    e.Cancel = true; 
                }
                else if (dialog == MessageBoxResult.Yes)
                {
                    stud.SaveDisciplines();
                    MessageBox.Show("Зміни збережено", "Info", MessageBoxButton.OK);
                }
            }
        }
    }
}
