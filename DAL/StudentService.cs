using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StudentService
    {

        //переробив на ліст
        private string _fileName;// = "students.bin";
        private string _fileNameDisciplines;// = "students.bin";

        private IList<Student> _students;
        static private IList<string> _disciplines;

        public IList<string> GetDisciplines
        {
            get { return _disciplines; }
            set { _disciplines = value; }
        }

        public StudentService()
        {
            _fileName = ConfigurationManager.AppSettings["StudentFileName"].ToString();
            if(File.Exists(_fileName))
            {
                using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _students = (IList<Student>)bf.Deserialize(fs);
                }
            }
            else
            {
                _students = new List<Student>();
            }

            //завантаження списку дисциплін
            _fileNameDisciplines = ConfigurationManager.AppSettings["DisciplinesFileName"].ToString();
            if (File.Exists(_fileNameDisciplines))
            {
                using (FileStream fs = new FileStream(_fileNameDisciplines, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _disciplines = (IList<string>)bf.Deserialize(fs);
                }
            }
            else
            {
                _disciplines = new List<string>();
            }

        }
        public void Add(Student student)
        {
            _students.Add(student);
        }
        public void AddDiscipline(string discipline)
        {
            _disciplines.Add(discipline);
        }

        public void SaveStud()
        {
            using (FileStream fs = new FileStream(_fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, _students);
            }
        }
        public void SaveDisciplines()
        {
            using (FileStream fs = new FileStream(_fileNameDisciplines, FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, _disciplines);
            }
        }


        public IList<Student> GetAllStudents
        {
            get { return _students; }
        }
        public int CountStudents
        {
            get { return _students.Count(); }
        }
    }
}
