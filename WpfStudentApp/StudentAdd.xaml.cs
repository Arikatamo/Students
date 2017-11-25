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
                // При додаванні картинки вона виводиться в блоці image
                if (images.ShowDialog() == true)
                {
                    image.Source = new BitmapImage(new Uri(images.FileName));
                }

            }
            if (but_add.Name == "add")
            {
                if (!String.IsNullOrEmpty(_name.Text) && image.Source != null && _name.Text != "Name")
                {
                    ///Додаємо назву картинки + її розширення з оригіналу
                    string img_name = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.Source.ToString());
                    /// Додаємо студента до колекції з Назвою оригінальної картиник
                    stud.Add(new Student() { Name = _name.Text, image = img_name });
                    /// Створюємо  Бітмап оригінальної картинки
                    Bitmap origin = new Bitmap(image.Source.ToString().Trim(@"file://".ToCharArray()));
                    ///Створюємо картинки під різні розміри
                    ///
                    ///
                    ///Маленькі картинки
                    Bitmap resized = new Bitmap(origin, new System.Drawing.Size(32, 32));
                    resized.Save(ConfigurationManager.AppSettings["ImagesPath_small"].ToString() + img_name);
                    /// Середені картинки
                    resized = new Bitmap(origin, new System.Drawing.Size(150,150));
                    resized.Save(ConfigurationManager.AppSettings["ImagesPath_middle"].ToString() + img_name);
                    /// Оригінал
                    Bitmap s = new Bitmap(image.Source.ToString().Trim(@"file://".ToCharArray()));
                    origin.Save(ConfigurationManager.AppSettings["ImagesPath"].ToString() + img_name);
                    /// Збереження тсудента
                    stud.Save();
                    /// Почистили блоки від данних
                    stud.GetAllStudents[stud.CountStudents].Marks_M.Clear();
                }
                else
                {
                    MessageBox.Show("Помилка", "Ви не ввели дані студента", MessageBoxButton.OK);
                }
                ///// Додавання оціенок до вже доданого Юзера... без Перевірок чи доданий юзер, іначе буде просто додавати до останнього пацана в в Діктіонарі!!
                ///// спробуй перевірити на роботоспособность і якусь перевірку, щоб кнопка додавання оцінок не появлялася поки не нажме кнопку додати студента!
                if (but_add.Name == "Add_Ocin")
                {
                    short a;
                    try
                    {
                        if (!String.IsNullOrEmpty(ocin.Name))
                        {
                            throw new Exception("Поле Пусте");
                        }
                        if (short.TryParse(ocin.Text, out a) && a < 0 || a > 100)
                        {
                            throw new Exception("Оцінка повинна бути від 1 - 100");
                        }
                        if (stud.GetAllStudents[stud.CountStudents].Marks_M[pred.Text] != null)
                        {
                            stud.GetAllStudents[stud.CountStudents].Marks_M[pred.Text].Add(a);
                        }
                        else
                        {
                            stud.GetAllStudents[stud.CountStudents].Marks_M.Add(pred.Text, new List<short> { a });
                        }
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show("Error", s.Message, MessageBoxButton.OK);
                    }

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
