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
            //видалив додавання оцінок тут 
            //foreach (string item in stud.GetDisciplines)
            //{
            //    pred.Items.Add(item);
            //}
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
                //переврка на наявність студента в списку
                if (!String.IsNullOrEmpty(_name.Text) && image.Source != null && _name.Text != "Name")
                {
                    //foreach (Student st in stud.GetAllStudents)
                    //{
                    //    if (String.Compare(st.Name, _name.Text, true) == 0)
                    //    {
                    //        MessageBox.Show("Помилка", "Такий студент вже існує. Змініть ім'я", MessageBoxButton.OK);
                    //        return;
                    //    }
                    //} 
                    ///Додаємо назву картинки + її розширення з оригіналу
                    string img_name = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.Source.ToString());
                    /// Додаємо студента до колекції з Назвою оригінальної картиник
                    stud.Add(new Student() { Name = _name.Text, image = img_name, Id = Guid.NewGuid().ToString() });
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
                    stud.SaveStud();
                    /// Почистили блоки від данних
                    // stud.GetAllStudents[stud.CountStudents].Marks_M.Clear();

                    //видалив додавання оцінок тут 
                    // add_ocin.IsEnabled = true;


                    MessageBox.Show("Студент успішно доданий в базу!", "Успішно");
                    _name.Text = "Name";
                    image.Source = null;
                }
                else
                {
                    MessageBox.Show("Помилка", "Ви не ввели дані студента", MessageBoxButton.OK);
                }
               
                //видалив додавання оцінок тут неробить 
                
            ///// Додавання оцінок до вже доданого Юзера... без Перевірок чи доданий юзер, іначе буде просто додавати до останнього пацана в в Діктіонарі!!
                ///// спробуй перевірити на роботоспособность і якусь перевірку, щоб кнопка додавання оцінок не появлялася поки не нажме кнопку додати студента!
                //if (but_add.Name == "Add_Ocin")
                //{
                //    try
                //    {
                //        if (pred.SelectedIndex == -1)
                //        {
                //            throw new Exception("Оберіть або додайте предмет");
                //        }
                //        if (short.TryParse(ocin.Text, out short a) && a < 0 || a > 100)
                //        {
                //            throw new Exception("Оцінка повинна бути від 1 - 100");
                //        }
                //        if (stud.GetAllStudents[stud.CountStudents].Marks_M.ContainsKey(pred.Text))
                //        {
                //            stud.GetAllStudents[stud.CountStudents].Marks_M[pred.Text].Add(a);
                //        }
                //        else
                //        {
                //            stud.GetAllStudents[stud.CountStudents].Marks_M.Add(pred.Text, new List<short> { a });
                //        }
                //    }
                //    catch (Exception s)
                //    {
                //        MessageBox.Show("Error", s.Message, MessageBoxButton.OK);
                //    }

                //}
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
