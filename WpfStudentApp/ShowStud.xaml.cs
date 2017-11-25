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
            StudentList.ItemsSource = stud.GetAllStudents;
            StudentList.SelectionMode = DataGridSelectionMode.Single;
            
        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentList.SelectedIndex < StudentList.Items.Count)
            {
                FullImage.Source = new BitmapImage(new Uri((StudentList.SelectedItem as Student).M_img_Original));
            }
        }
    }
}
