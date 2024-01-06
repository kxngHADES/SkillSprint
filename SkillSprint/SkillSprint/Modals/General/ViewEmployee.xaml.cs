using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkillSprint.Database;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static SkillSprint.Database.Users;

namespace SkillSprint.Modals.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEmployee : ContentPage
    {
        private readonly DatabaseHelper _databaseHelper;
        public ObservableCollection<Employee> Employees { get; set; }

        public ICommand SearchCommand => new Command<string>(PerformSearch);

        public ViewEmployee()
        {
            InitializeComponent();

            // Use an instance of App to access DatabasePath
            _databaseHelper = new DatabaseHelper(App.Database.DatabasePath);
            Employees = new ObservableCollection<Employee>();
            employeeListView.ItemsSource = Employees;

            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var employees = await _databaseHelper.GetAllEmployees();
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        private async void PerformSearch(string query)
        {
            var filteredEmployees = await _databaseHelper.GetAllEmployees();

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