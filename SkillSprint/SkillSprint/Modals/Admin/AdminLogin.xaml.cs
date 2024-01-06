using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkillSprint.Database;

namespace SkillSprint.Modals.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminLogin : ContentPage
    {
        private DatabaseHelper _database;

        public AdminLogin()
        {
            InitializeComponent();
            _database = App.Database;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string adminId = adminIdEntry.Text;
            string password = passwordEntry.Text;

            if (!string.IsNullOrWhiteSpace(adminId) && !string.IsNullOrWhiteSpace(password))
            {
                // Authenticate admin
                var admin = await _database.AdminLogin(password, int.Parse(adminId));

                if (admin != null)
                {
                    // Store AdminID for later use
                    App.CurrentAdminID = admin.AdminID;

                    // Navigate to the main page or perform other actions
                    App.NavAdmin();
                }
                else
                {
                    // Admin authentication failed
                    await DisplayAlert("Login Failed", "Invalid Admin ID or Password", "OK");
                }
            }
            else
            {
                // Display an alert if Admin ID or Password is empty
                await DisplayAlert("Login Failed", "Admin ID and Password are required", "OK");
            }
        }
    }
}
