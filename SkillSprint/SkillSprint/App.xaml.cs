using SkillSprint.Modals.General;
using SkillSprint.Modals.Client;
using SkillSprint.Modals.Employees;
using SkillSprint.Modals.Admin;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkillSprint.Database;
using System.IO;

namespace SkillSprint
{
    public partial class App : Application
    {
        public static int CurrentClientID { get; set; } // store Client ID for client who logged ID
        public static int CurrentEmployeeID { get; set; } //Store EmployeeID for the employe who has logged in 
        public static int CurrentAdminID { get; set; } //Store AdminID
        public static DatabaseHelper Database { get; set; }
        public App()
        {
            InitializeComponent();

            //Path and datbase connection
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SkillSprint.db3");

            Database = new DatabaseHelper(databasePath);

            MainPage = new Start();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async void NavigateToMainPage()
        {
            //
        }

        public static void NavAdmin()
        {
            Current.MainPage = new NavigationPage(new AdminLogin());
        }

        public static void NavProfile()
        {
            //Current.MainPage = new NavigationPage(new ProfilePage(Database, CurrentClientID));
        }
    }
}
