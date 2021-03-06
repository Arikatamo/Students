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
    /// Interaction logic for AddMarks.xaml
    /// </summary>

    /// Форма для додавання оцінок до доданого студента
    public partial class AddMarks : Window
    {
        StudentService stud;
        public AddMarks(StudentService stud)
        {
            InitializeComponent();
            this.stud = stud;
            //ComboStudName.ItemsSource = stud.GetAllStudents;

            foreach (Student item in stud.GetAllStudents)
            {
                ComboStudName.Items.Add(item.Name);
            }
            foreach (string item in stud.GetDisciplines)
            {
                ComboDiscipName.Items.Add(item);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(short.TryParse(ocin.Text,out short a) && a<100 && a>0 && ComboDiscipName.SelectedIndex!=-1 && ComboStudName.SelectedIndex != -1)
            {
                add_ocin.IsEnabled = true;
            }
            else
            {
                add_ocin.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if (stud.GetAllStudents.First(x => x.Name == ComboStudName.Text).Marks_M.ContainsKey(ComboDiscipName.Text) == null)
                // {
                //     stud.GetAllStudents.First(x => x.Name == ComboStudName.Text).Marks_M.Add(ComboDiscipName.Text, new List<short> { short.Parse(ocin.Text) });

                // }
                //else 
                MessageBox.Show(ComboStudName.SelectedIndex.ToString());
                if (stud.GetAllStudents[ComboStudName.SelectedIndex].Marks_M.ContainsKey(ComboDiscipName.Text))
                {
                    stud.GetAllStudents[ComboStudName.SelectedIndex].Marks_M[ComboDiscipName.Text].Add(short.Parse(ocin.Text));
                }
               // stud.GetAllStudents.First(x => x.Name == ComboStudName.Text).Marks_M = new Dictionary<string, List<short>>();
                stud.GetAllStudents[ComboStudName.SelectedIndex].Marks_M.Add(ComboDiscipName.Text, new List<short> { short.Parse(ocin.Text) });
            }
            catch (Exception s)
            {
     

             
            }

            MessageBox.Show("Оцінку успішно додано!", "Успіх");
            ocin.Text = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Student temp = stud.GetAllStudents[ComboStudName.SelectedIndex];
            stud.GetAllStudents.Remove(stud.GetAllStudents[ComboStudName.SelectedIndex]);
            stud.Add(temp);
            stud.SaveStud();
        }
    }
}
