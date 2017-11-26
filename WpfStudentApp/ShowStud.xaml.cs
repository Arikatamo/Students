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
            //Discipline.AutoGenerateColumns = false;
            Discipline.IsReadOnly = true;
            StudentList.ItemsSource = stud.GetAllStudents;
            StudentList.SelectionMode = DataGridSelectionMode.Single;
            Disc.Content = "Предмет";

        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Онулення Колекції(без буде крашитись)
            Discipline.ItemsSource = null;
          //  Discipline.Items.Clear();
            if (StudentList.SelectedIndex < StudentList.Items.Count)
            {
                Student temp = StudentList.SelectedItem as Student;
                FullImage.Source = new BitmapImage(new Uri((temp).M_img_Original));
                if (temp.Marks_M != null)
                {
                    // Додає соурс предметів в ДатаГрід
                    Discipline.ItemsSource = temp.Marks_M;

                    // Discipline.CurrentColumn.Header;
                    //foreach (string item in temp.Marks_M.Keys.ToArray())
                    //{
                    //  
                    //    Discipline.Items.Add(item.ToString());
                    //}

                }

            }
        }

        private void Discipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //MessageBox.Show(Discipline.SelectedIndex.ToString());
            if (Discipline.SelectedIndex >= 0)
            {
                var a = (KeyValuePair<string,List<short>>)Discipline.Items[Discipline.SelectedIndex]; 

                Disc.Content = a.Key;
                dataOcin.Content = null;
                foreach (var item in a.Value)
                {
                    dataOcin.Content += " " + item + " ";
                }
               

            }
            else
            {
                Disc.Content = "Предмет";
            }
        }
    }
}
