using Microsoft.Win32;
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
using System.Configuration;
using System.IO;
namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        StudentService stud;
        public StudentAdd(StudentService a)
        {
            InitializeComponent();
            stud = a;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog images;
            Button but_add = sender as Button;
            if (but_add.Name == "add_image")
            {
                images = new OpenFileDialog();
                images.Filter = @"All Img|*.jpg;*.jpeg;*.png";

                if (images.ShowDialog() == true)
                {

                    image.Source = new BitmapImage(new Uri(images.FileName));
                }

            }
            if (but_add.Name == "add")
            {
                if (!String.IsNullOrEmpty(_name.Text) && image.Source != null && _name.Text != "Name")
                {
                    string img_name = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.Source.ToString());
                    stud.Add(new Student() { Name = _name.Text, image = img_name });
                    Bitmap a = new Bitmap(image.Source.ToString().Trim(@"file://".ToCharArray()));
                    a.Save(ConfigurationManager.AppSettings["ImagesPath"].ToString() + img_name);
                    stud.Save();

                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_name.Text == "Name")
            {
                _name.Text = "";
                _name.Foreground = System.Windows.Media.Brushes.Black ;
            }
        }

        private void _name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_name.Text))
            {
                _name.Text = "Name";
                _name.Foreground = System.Windows.Media.Brushes.LightGray;
            }
        }
    }
}
