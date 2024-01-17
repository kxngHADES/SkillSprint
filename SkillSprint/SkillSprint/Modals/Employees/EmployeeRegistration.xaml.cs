using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SkillSprint.Database.Users;

namespace SkillSprint.Modals.Employees
{
    public partial class EmployeeRegistration : ContentPage
    {
        private string selectedImagePath;

        public string SelectedImagePath
        {
            get => selectedImagePath;
            set
            {
                selectedImagePath = value;
                OnPropertyChanged(nameof(SelectedImagePath));
            }
        }

        public EmployeeRegistration()
        {
            InitializeComponent();
            btnRegister.Clicked += OnRegisterClicked;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                // Create a new Employee object
                var newEmployee = new Employee
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Service = txtService.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Password = txtPassword.Text,
                    StartDate = DateTime.Now,
                    ImagePath = SelectedImagePath
                };

                // Register the employee in the database
                var result = await App.Database.EmployeReg(newEmployee);

                if (result > 0)
                {
                    await DisplayAlert("Success", "Registration successful!", "OK");
                    // Navigate to login page or any other page as needed
                    await Navigation.PushModalAsync(new EmployeeLogin());
                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
                }
            }
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtService.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
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

            if (!IsValidPhoneNumber(txtPhoneNumber.Text))
            {
                DisplayAlert("Error", "Invalid phone number.", "OK");
                return false;
            }

            if (!IsStrongPassword(txtPassword.Text))
            {
                DisplayAlert("Error", "Password must be at least 8 characters long and include at least 1 uppercase, 1 lowercase, 1 numeric digit, and 1 special character.", "OK");
                return false;
            }

            // Validate Price as a decimal
            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                DisplayAlert("Error", "Invalid price. Please enter a valid number.", "OK");
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
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
            return regex.IsMatch(password);
        }

        private async void OnUploadPictureClicked(object sender, EventArgs e)
        {
            var mediaResult = await MediaPicker.PickPhotoAsync();

            if (mediaResult != null)
            {
                SelectedImagePath = mediaResult.FullPath;
                // Display the selected image in the Image control
                imgProfile.Source = ImageSource.FromFile(SelectedImagePath);
            }
        }

    }
}
