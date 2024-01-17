using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkillSprint.Database;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static SkillSprint.Database.Users;
using System;

namespace SkillSprint.Modals.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEmployee : ContentPage
    {
        private ObservableCollection<Employee> Employees { get; set; }

        public ViewEmployee()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var allEmployeesTask = App.Database.GetAllEmployees();

            var allEmployees = await allEmployeesTask;
            Employees = new ObservableCollection<Employee>();
            employeeListView.ItemsSource = allEmployees.ToList();
        }

        private void OnViewDetailsClicked(object sender, EventArgs eventArgs)
        {
            if (sender is Button btn && btn.CommandParameter is int EmployeeID)
            {
                Navigation.PushModalAsync(new EmplyeeDetails(App.Database, EmployeeID));
            }
        }

        
        private async void PerformSearch(string query)
        {
            var filteredEmployees = await App.Database.GetAllEmployees();

            if (!string.IsNullOrWhiteSpace(query))
            {
                filteredEmployees = filteredEmployees.Where(e =>
                    e.FirstName.ToLower().Contains(query.ToLower()) ||
                    e.LastName.ToLower().Contains(query.ToLower()) ||
                    e.Email.ToLower().Contains(query.ToLower())
                ).ToList();
            }

            Employees.Clear();
            foreach (var employee in filteredEmployees)
            {
                Employees.Add(employee);
            }
        }



    }
}