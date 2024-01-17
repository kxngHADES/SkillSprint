using SkillSprint.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillSprint.Modals.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmplyeeDetails : ContentPage
    {
        private readonly DatabaseHelper _database;
        private readonly int _EmployeeID;
        public EmplyeeDetails(DatabaseHelper database, int EmployeeID)
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

        private async void OnReturn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewEmployee());
        }
    }
}