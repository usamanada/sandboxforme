using System;
using System.ComponentModel;

namespace OrderedPropertyGrid.Examples
{
    [DefaultProperty("Name")]
    public class Person
    {
        protected const string PERSONAL_CAT = "Personal Details";
        
        private string _name = "Bob";
        private DateTime _birthday = new DateTime(1975,1,1);

        [Category(PERSONAL_CAT)]
        public string Name
        {
            get {return _name;} 
            set {_name = value;} 
        }

        [Category(PERSONAL_CAT)]
        public DateTime Birthday
        {
            get {return _birthday;}
            set {_birthday = value;}
        }

        [Category(PERSONAL_CAT)]
        public int Age
        {
            get 
            {
                TimeSpan age = DateTime.Now - _birthday; 
                return (int)age.TotalDays / 365; 
            }
        }
    }

    public class Employee : Person
    {
        protected const string WORK_CAT = "Work Details";

        private string _phoneExt = "80555";
        private string _contact = "Jill";
        private decimal _salary = 40000;
        private DateTime _startDate = new DateTime(2004,1,1);

        [Category(PERSONAL_CAT)]
        public string Contact
        {
            get {return _contact;}
            set {_contact = value;} 
        }
        
        [Category(WORK_CAT)]
        public string PhoneExt
        {
            get {return _phoneExt;}  
            set {_phoneExt = value;} 
        }      

        [Category(WORK_CAT)]
        public DateTime StartDate
        {
            get {return _startDate;}
            set {_startDate = value;}             
        }      

        [Category(WORK_CAT)]
        public decimal Salary
        {
            get {return _salary;}          
            set {_salary = value;} 
        }      
    }
}
