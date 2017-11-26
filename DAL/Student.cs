﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
namespace DAL
{
    [Serializable]
    public class Student
    {
        static ushort idCounter = 1;
        private string _name;
        // Ключ предмет + Ліст оцінок по тому предмету!
        private Dictionary<string,List<short>> Marks = new Dictionary<string, List<short>>();
        public string GetV(string key)
        {
            return Marks[key].ToString();

        }
        public Student()
        {
            Id = idCounter++;
        }

        public Dictionary<string, List<short>> Marks_M
        {
            get { return Marks; }
            set { Marks = value; }

        }
        private string _image;


        /// <summary>
        ///  Для виводу картинок по розмірам()
        /// </summary>
        public string M_img_small
        {
            get { return ConfigurationManager.AppSettings["ImagesPath_small"].ToString() + image; }
        }
        public string M_img_Middle
        {
            get { return ConfigurationManager.AppSettings["ImagesPath_middle"].ToString() + image; }
        }
        public string M_img_Original
        {
            get { return ConfigurationManager.AppSettings["ImagesPath"].ToString() + image; }
        }



        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Image_smal
        {
            get {
                return _image; }
            set { _image = value; }
        }
        public string image
        {
            get { return  _image; }
            set { _image = value; }
        }
        public int Id { get;set; }

    }
}
