using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSprint.Modals.Client;
using SkillSprint.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace SkillSprint.Modals.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        public Home()
        {
            InitializeComponent();

            this.Children.Add(new ViewEmployee() { Title = "Skills" });
            this.Children.Add(new ProfilePage() { Title = "Profile" });
            this.Children.Add(new MessagePage() { Title = "Message" });
            this.Children.Add(new ContactPage() { Title = "Contact" });
            //this.Appearing += Home_Appearing;
        }

        /*private async void Home_Appearing(object sender, EventArgs e)
        {
            // Add a Clicked event handler for the "Profile" tab
            var profileNavigationPage = this.Children[3] as NavigationPage; // Assuming "Profile" is the second tab
            var profileTab = profileNavigationPage.CurrentPage;
            profileTab.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Profile",
                Command = new Command(async () =>
                {
                    int clientID = App.CurrentClientID;
                    var profilePage = new ProfilePage();
                    await Navigation.PushModalAsync(new NavigationPage(profilePage));
                })
            });
        }*/

        /*private async void UpdateHeader()
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

        private void Service_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ViewEmployee());
        }*/
    }
}
