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
using System.Drawing;
using System.Data;

namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for ShowStud.xaml
    /// </summary>
    public partial class ShowStud : Window
    {
        StudentService stud;
        public ShowStud(StudentService a)
        {
            InitializeComponent();
            stud = a;
            StudentList.AutoGenerateColumns = false;
            Discipline.AutoGenerateColumns = false;
            StudentList.ItemsSource = stud.GetAllStudents;
            StudentList.SelectionMode = DataGridSelectionMode.Single;
            
        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Discipline.Items.Clear();
            if (StudentList.SelectedIndex < StudentList.Items.Count)
            {
                Student temp = StudentList.SelectedItem as Student;
                FullImage.Source = new BitmapImage(new Uri((temp).M_img_Original));
                if (temp.Marks_M != null)
                {
                    Dictionary<string, List<short>> temp2 = temp.Marks_M;
                    foreach (var item in temp2.Keys)
                    {
                        Discipline.Items.Add(item.ToString());
                    }

                }

            }
        }
    }
}
