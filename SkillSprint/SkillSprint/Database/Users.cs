using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SkillSprint.Database
{
    public  class Users
    {
        public class Administration
        {
            [PrimaryKey, AutoIncrement]
            public int AdminID { get; set; }
            public string FirstName { get; set; }
            public string Password { get; set; }
        }

        public class Employee
        {
            [PrimaryKey, AutoIncrement]
            public int EmployeeID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Service { get; set; }
            public DateTime StartDate { get; set; }
            public decimal Price { get; set; }
            public string Password { get; set; }
            public string ImagePath { get; set; }
        }

        public class Client
        {
            [PrimaryKey, AutoIncrement]
            public int ClientID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
        }

        public class Rating
        {
            [PrimaryKey, AutoIncrement]
            public int RatingID { get; set; }
            public int RatingValue { get; set; }
            public int ClientID { get; set; }
            public int EmployeeID { get; set; }
            public DateTime DateTime { get; set; }
        }

        public class ClientLog
        {
            [PrimaryKey, AutoIncrement]
            public int CLogID { get; set; }
            public int ClientID { get; set; }
            public string Action { get; set; }
            public DateTime DateTime { get; set; }
        }

        public class EmployeeLog
        {
            [PrimaryKey, AutoIncrement]
            public int ELogID { get; set; }
            public int EmployeeID { get; set; }
            public string Action { get; set; }
            public DateTime DateTime { get; set; }
        }

    }
}
