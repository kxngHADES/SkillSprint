using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSprint;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SkillSprint.Database.Users;

namespace SkillSprint.Modals.Employees
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeProfile : ContentPage
    {
        public EmployeeProfile()
        {
            InitializeComponent();
            Employee = new Employee();
            LoadEmployeeDetails();
        }

        private async void LoadEmployeeDetails()
        {
            // Check if an employee is currently logged in
            if (App.CurrentEmployeeID != 0)
            {
                // Retrieve the employee details from the database
                var employee = App.Database.DisplayEmployee(App.CurrentEmployeeID).Result;

                if (employee != null)
                {
                    // Set the BindingContext to the current instance of EmployeeProfile
                    BindingContext = this;

                    // Set the properties for data binding
                    Employee = employee;
                }
            }
        }

        private Employee _employee;
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            // Save the changes to the database
            if (Employee != null)
            {
                // Update the employee's details
                Employee.FirstName = txtFirstName.Text;
                Employee.LastName = txtLastName.Text;
                Employee.Email = txtEmail.Text;
                Employee.PhoneNumber = txtPhoneNumber.Text;
                Employee.Service = txtService.Text;

                // Parse the price (assuming Price is a decimal)
                if (decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    Employee.Price = price;
                }
                // Handle other entry fields as needed

                // Save changes to the database
                await App.Database.UpdateEmployee(Employee);

                // Display success message or navigate back
                await DisplayAlert("Success", "Profile updated successfully", "OK");
                await Navigation.PopModalAsync();
            }
        }

        private async void OnChangeProfilePictureClicked(object sender, EventArgs e)
        {
            try
            {
                // Open the MediaPicker to select an image
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select Profile Picture"
                });

                if (result != null)
                {
                    // Set the selected image path to the ImagePath property
                    Employee.ImagePath = result.FullPath;

                    // Update the employee's profile picture path in the database
                    await App.Database.UpdateEmployee(Employee);

                    // Display success message
                    await DisplayAlert("Success", "Profile picture updated successfully", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                await DisplayAlert("Error", $"Error selecting image: {ex.Message}", "OK");
            }
        }
    }
}

