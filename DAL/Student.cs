using System;
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
        private int _id;
        private string _name;        
        private IList<Mark> _marks;
        private string _image;

        public string M_img_small
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

        public IList<Mark> Marks
        {
            get { return _marks; }
            set { _marks = value; }
        }


    }
}
