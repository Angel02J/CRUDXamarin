using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EmployeeRegister.Models
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int IdEmpl { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string MotherLastName { get; set; }

        public int Age { get; set; }

        public int Telephone { get; set; }



    }
}
