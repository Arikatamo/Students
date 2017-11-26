﻿using System;
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
    /// Interaction logic for EditStud.xaml
    /// </summary>
    public partial class EditStud : Window
    {
        StudentService stud;
        public EditStud(StudentService stud)
        {
            InitializeComponent();
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
                //foreach (Student item in stud.GetAllStudents)
                //{
                //    if(item.Name == tmpstud)
                //    {
                //    image.Source = new BitmapImage(new Uri((item).M_img_Original)); 
                //    }
                //}






            }



        }
    }
}