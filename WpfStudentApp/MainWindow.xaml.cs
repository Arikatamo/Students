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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using DAL;
using System.Configuration;
namespace WpfStudentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StudentService studService = new StudentService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd stud = new StudentAdd(studService);
            stud.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowStud stud = new ShowStud(studService);
            stud.ShowDialog();
        }

        //додати оцінки
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddMarks addMarks = new AddMarks(studService);
            addMarks.ShowDialog();
        }

        //додати дисципліну
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddDiscipline addDiscipline = new AddDiscipline(studService);
            addDiscipline.ShowDialog();
        }

        private void btn_editstud_Click(object sender, RoutedEventArgs e)
        {
            EditStud editStud = new EditStud(studService);
            editStud.ShowDialog();

        }
    }
}
