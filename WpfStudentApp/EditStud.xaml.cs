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
using Microsoft.Win32;
using System.Drawing;
using System.Configuration;
using System.IO;
namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for EditStud.xaml
    /// </summary>
    public partial class EditStud : Window
    {
        StudentService stud;
        Student temp;
        string[] ImageSDell;

        public EditStud(StudentService stud)
        {
            InitializeComponent();
            temp = null;
            this.stud = stud;
            foreach (Student item in stud.GetAllStudents)
            {
                ComboStudName.Items.Add(item.Name);

            }

        }

        private void ComboStudName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    MessageBox.Show("Hello");

            if(ComboStudName.SelectedIndex!=-1)
            {
                //по вибраному студентові підтягує картинку
                // Student temp = ComboStudName.SelectedItem as Student;
                //string tmpstud = ComboStudName.SelectedItem as string;
                image.Source = new BitmapImage(new Uri((stud.GetAllStudents[ComboStudName.SelectedIndex]).M_img_Original));
                temp = stud.GetAllStudents[ComboStudName.SelectedIndex];
                //foreach (Student item in stud.GetAllStudents)
                //{
                //    if(item.Name == tmpstud)
                //    {
                //    image.Source = new BitmapImage(new Uri((item).M_img_Original)); 
                //    }
                //}






            }



        }

        private void add_image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openimages;
          
                openimages = new OpenFileDialog();
                openimages.Filter = @"All Img|*.jpg;*.jpeg;*.png";
                // При додаванні картинки вона виводиться в блоці image
                if (openimages.ShowDialog() == true)
                {
                    image.Source = new BitmapImage(new Uri(openimages.FileName));
                }



            
        }

        private void _name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_name.Text == "New Name")
            {
                _name.Text = "";
                _name.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void _name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_name.Text))
            {
                _name.Text = "New Name";
                _name.Foreground = System.Windows.Media.Brushes.LightGray;
            }
        }

        private void save_change_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(_name.Text)) && _name.Text.Length>3 && _name.Text!="New Name")
                temp.Name = _name.Text;
            
            if(image.Source != null)
            {
                // взято з додавання студента 
                ImageSDell = new string[3]
    {
                    (stud.GetAllStudents[ComboStudName.SelectedIndex]).M_img_Original,
                    (stud.GetAllStudents[ComboStudName.SelectedIndex]).M_img_Middle,
                    (stud.GetAllStudents[ComboStudName.SelectedIndex]).M_img_small
 };

                string img_name = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.Source.ToString());
                /// Додаємо студента до колекції з Назвою оригінальної картиник
                //  stud.Add(new Student() { Name = _name.Text, image = img_name, Id = Guid.NewGuid().ToString() });
                /// Створюємо  Бітмап оригінальної картинки
                /// 
                temp.image = img_name;
                Bitmap origin = new Bitmap(image.Source.ToString().Trim(@"file://".ToCharArray()));
                ///Створюємо картинки під різні розміри
                ///
                ///
                ///Маленькі картинки
                Bitmap resized = new Bitmap(origin, new System.Drawing.Size(32, 32));
                resized.Save(ConfigurationManager.AppSettings["ImagesPath_small"].ToString() + img_name);
                /// Середені картинки
                resized = new Bitmap(origin, new System.Drawing.Size(150, 150));
                resized.Save(ConfigurationManager.AppSettings["ImagesPath_middle"].ToString() + img_name);
                /// Оригінал
                Bitmap s = new Bitmap(image.Source.ToString().Trim(@"file://".ToCharArray()));
                origin.Save(ConfigurationManager.AppSettings["ImagesPath"].ToString() + img_name);
                DirectoryInfo a = new DirectoryInfo("ImagesPath_small");


                /// Збереження тсудента
                stud.SaveStud();

                MessageBox.Show("Успішно збережено!", "Успіх");
            }

            this.Close();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                foreach (var item in ImageSDell)
                {
                    File.Delete(item);
                }

            }
            catch (Exception)
            {

            
            }

        }
    }
}
