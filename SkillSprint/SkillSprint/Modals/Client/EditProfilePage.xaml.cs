using SkillSprint.Database;
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
    public partial class EditProfilePage : ContentPage
    {
        private readonly DatabaseHelper _database;
        private readonly int _clientID;
        public EditProfilePage(DatabaseHelper database, int ClientID)
        {
            InitializeComponent();
            _database = database;
            _clientID = ClientID;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var Client = await _database.GetClientById(_clientID);

            BindingContext = Client;
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        // New Password properties
        private string _currentPassword;
        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Save the changes to the database
            if (App.CurrentClientID != 0)
            {
                var client = await App.Database.DisplayClient(App.CurrentClientID);
                if (client != null)
                {
                    // Update the client's profile
                    client.FirstName = FirstName;
                    client.LastName = LastName;
                    client.Email = Email;
                    client.PhoneNumber = PhoneNumber;

                    // Save changes to the database
                    await App.Database.UpdateClient(client);

                    // Check if the user wants to change the password
                    if (!string.IsNullOrEmpty(CurrentPassword) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword))
                    {
                        // Check if the current password is correct
                        if (client.Password == CurrentPassword)
                        {
                            // Check if the new password matches the confirmed password
                            if (NewPassword == ConfirmPassword)
                            {
                                // Update the client's password
                                client.Password = NewPassword;

                                // Save changes to the database
                                await App.Database.UpdateClient(client);
                            }
                            else
                            {
                                // Display an error message for password mismatch
                                await DisplayAlert("Error", "New password and confirm password do not match", "OK");
                                return;
                            }
                        }
                        else
                        {
                            // Display an error message for incorrect current password
                            await DisplayAlert("Error", "Incorrect current password", "OK");
                            return;
                        }
                    }

                    // Display success message or navigate back
                    await DisplayAlert("Success", "Profile updated successfully", "OK");
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}
