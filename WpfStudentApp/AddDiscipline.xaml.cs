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
        StudentService stud;
        public AddDiscipline(StudentService stud)
        {
            InitializeComponent();
            this.stud = stud;
        }

        private void add_ocin_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(DiscipName.Text))
            {
                MessageBox.Show("Помилка", "Введіть назву предмета", MessageBoxButton.OK);
            }
            else if (stud.GetDisciplines.Contains(DiscipName.Text))
            {
                MessageBox.Show("Помилка", "Предмет вже в базі", MessageBoxButton.OK);
            }
            else
            {
                stud.AddDiscipline(DiscipName.Text);
                stud.SaveDisciplines();
            }

        }
    }
}
