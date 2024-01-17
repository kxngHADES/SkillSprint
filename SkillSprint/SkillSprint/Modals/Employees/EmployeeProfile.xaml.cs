using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSprint;
using SkillSprint.Database;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SkillSprint.Database.Users;

namespace SkillSprint.Modals.Employees
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeProfile : ContentPage
    {
        private readonly DatabaseHelper _database;
        private readonly int _EmployeeID;
        public EmployeeProfile(DatabaseHelper database, int EmployeeID)
        {
            InitializeComponent();
            _database = database;
            _EmployeeID = EmployeeID;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var Employee = await _database.GetEmployeeById(_EmployeeID);

            BindingContext = Employee;
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
                // Ensure the Employee instance is initialized
                if (Employee == null)
                {
                    return;
                }

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

                    // Update the binding context to reflect the changes
                    BindingContext = null;
                    BindingContext = Employee;

                    // Display success message
                    await DisplayAlert("Success", "Profile picture updated successfully", "OK");

                    // Update image preview
                    imgPreview.Source = result.FullPath;
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

