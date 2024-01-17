using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillSprint.Modals.Employees
{
    public partial class EmployeeLogin : ContentPage
    {
        public EmployeeLogin()
        {
            InitializeComponent();
            btnELog.Clicked += OnLoginClicked;
            btnEReg.Clicked += OnRegisterClicked;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                // Perform employee login
                var email = txtEmail.Text;
                var password = txtPassword.Text;

                var employee = await App.Database.EmployeeLogin(email, password);

                if (employee != null)
                {
                    // Store EmployeeID in App class for future reference
                    App.CurrentEmployeeID = employee.EmployeeID;

                    await DisplayAlert("Success", "Login successful!", "OK");
                    // Navigate to the main page or any other page as needed
                    await Navigation.PushModalAsync(new EmployeeProfile(App.Database, employee.EmployeeID));
                }
                else
                {
                    await DisplayAlert("Error", "Invalid email or password. Please try again.", "OK");
                }
            }
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navigate to the employee registration page
            Navigation.PushModalAsync(new EmployeeRegistration());
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                DisplayAlert("Error", "Email and password are required.", "OK");
                return false;
            }

            return true;
        }
    }
}
