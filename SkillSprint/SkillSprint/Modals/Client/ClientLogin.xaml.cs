using SkillSprint.Modals.General;
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
    public partial class ClientLogin : ContentPage
    {
        public ClientLogin()
        {
            InitializeComponent();
            btnLogin.Clicked += OnLoginClick;
            btnReg.Clicked += OnRegisterClick;
        }

        private async void OnLoginClick(object sender, EventArgs e)
        {
            var client = await App.Database.ClientLogin(txtEmail.Text, txtPassword.Text);

            if (client != null)
            {
                App.CurrentClientID = client.ClientID;
                await Navigation.PushModalAsync(new Home());
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }

        private void OnRegisterClick(object sender, EventArgs e)
        {
            // Navigate to the registration page
            Navigation.PushModalAsync(new ClientRegistration());
        }
    }
}