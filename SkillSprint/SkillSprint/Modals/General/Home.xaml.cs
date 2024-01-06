using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSprint.Modals.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillSprint.Modals.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            BindingContext = this; // Set the BindingContext to the current instance of Home
            UpdateHeader(); // Call the method to update the header
        }

        private async void UpdateHeader()
        {
            // Check if the current user is a client
            if (App.CurrentClientID != 0)
            {
                var client = await App.Database.DisplayClient(App.CurrentClientID);
                if (client != null)
                {
                    // Set the UserName property for data binding
                    UserName = $"Welcome, {client.FirstName} {client.LastName}!";
                }
            }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private void ProfileButton_Clicked(Object  sender, EventArgs e)
        {
            App.NavProfile();
        }
    }
}