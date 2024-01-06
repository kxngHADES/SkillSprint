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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = this; // Set the BindingContext to the current instance of ProfilePage
            UpdateProfile(); // Call the method to update the profile details
        }

        private void UpdateProfile()
        {
            // Check if the current user is a client
            if (App.CurrentClientID != 0)
            {
                var client = App.Database.DisplayClient(App.CurrentClientID).Result; // Use Result to get the client from the Task
                if (client != null)
                {
                    // Set the properties for data binding
                    FirstName = client.FirstName;
                    LastName = client.LastName;
                    Email = client.Email;
                    PhoneNumber = client.PhoneNumber;
                }
            }
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

        private async void OnEditClicked(object sender, EventArgs e)
        {
            // Navigate to the edit profile page
            await Navigation.PushModalAsync(new EditProfilePage());
        }
    }
}
