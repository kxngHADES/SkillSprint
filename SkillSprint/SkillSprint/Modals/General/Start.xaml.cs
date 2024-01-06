using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillSprint.Modals.Employees;
using SkillSprint.Modals.Client;
using SkillSprint.Modals.Admin;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillSprint.Modals.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Start : ContentPage
    {
        public Start()
        {
            InitializeComponent();
            btnEmployee.Clicked += OnEmployee_Clicked;
            btnClient.Clicked += OnClient_Clicked;

            var swipeGestureRecognizer = new SwipeGestureRecognizer();
            swipeGestureRecognizer.Swiped += OnSecretSwipe;
            mainStackLayout.GestureRecognizers.Add(swipeGestureRecognizer);

        }

        private const string secretSwipePattern = "RightRightLeftDownUp";
        private string currentSwipePattern = "";

        private void OnSecretSwipe(object sender, SwipedEventArgs e)
        {
            currentSwipePattern += e.Direction.ToString();

            if (currentSwipePattern.EndsWith(secretSwipePattern))
            {
                btnAdminLogin.IsVisible = true;
                currentSwipePattern = "";
                Navigation.PushModalAsync(new AdminLogin());
            }
        }

        private void OnEmployee_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EmployeeLogin());
        }

        private void OnClient_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ClientLogin());
        }
    }
}