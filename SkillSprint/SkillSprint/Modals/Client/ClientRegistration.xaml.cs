using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillSprint.Modals.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientRegistration : ContentPage
    {
        public ClientRegistration()
        {
            InitializeComponent();
            btnRegister.Clicked += OnRegisterClick;
        }

        private async void OnRegisterClick(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                // Create a new Client object
                var newClient = new Database.Users.Client
                {
                    FirstName = txtName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    Password = txtPassword.Text
                };

                // Register the client in the database
                var result = await App.Database.ClientReg(newClient);

                if (result > 0)
                {
                    await DisplayAlert("Success", "Registration successful!", "OK");
                    // Navigate to login page or any other page as needed
                    await Navigation.PushModalAsync(new ClientLogin());
                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
                }
            }
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                DisplayAlert("Error", "All fields are required.", "OK");
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                DisplayAlert("Error", "Invalid email address.", "OK");
                return false;
            }

            if (!IsValidPhoneNumber(txtPhone.Text))
            {
                DisplayAlert("Error", "Invalid phone number.", "OK");
                return false;
            }

            if (!IsStrongPassword(txtPassword.Text))
            {
                DisplayAlert("Error", "Password must be at least 8 characters long and include at least 1 uppercase, 1 lowercase, 1 numeric digit, and 1 special character.", "OK");
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // You can implement more sophisticated phone number validation if needed
            return !string.IsNullOrWhiteSpace(phoneNumber);
        }

        private bool IsStrongPassword(string password)
        {
            // Password must be at least 8 characters long
            // and include at least 1 uppercase, 1 lowercase, 1 numeric digit, and 1 special character
            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
            return regex.IsMatch(password);
        }
    }
}
