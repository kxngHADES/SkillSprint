using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSprint.Database;
using SkillSprint.Modals.General;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace SkillSprint.Modals.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly DatabaseHelper _database;
        private readonly int _clientID;

        public ProfilePage()
        {
            InitializeComponent();
            _database = App.Database;
            _clientID = App.CurrentClientID;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var Client = await _database.GetClientById(_clientID);

            BindingContext = Client;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            // Navigate to the edit profile page
            await Navigation.PushModalAsync(new EditProfilePage(App.Database, App.CurrentClientID));
        }
    }
}
