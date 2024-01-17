using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SkillSprint.Database.Users;

namespace SkillSprint.Database
{
    public class DatabaseHelper
    {
        readonly SQLiteAsyncConnection database;
        public string DatabasePath { get; private set; }
        public DatabaseHelper(string dbPath) 
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Administration>().Wait();
            database.CreateTableAsync<Employee>().Wait();
            database.CreateTableAsync<Client>().Wait();
            database.CreateTableAsync<Rating>().Wait();
            database.CreateTableAsync<ClientLog>().Wait();
            database.CreateTableAsync<EmployeeLog>().Wait();
        }


        //Client
        #region ClientTasks

        //Login
        public async Task<Client> ClientLogin(string email, string password)
        {
            return await database.Table<Client>().Where(c => c.Email == email && c.Password == password).FirstOrDefaultAsync();
        }


        //Registration
        public async Task<int> ClientReg(Client client)
        {
            return await database.InsertAsync(client);
        }

        // Display Client
        public async Task<Client> DisplayClient(int clientID)
        {
            return await database.Table<Client>().FirstOrDefaultAsync(c => c.ClientID == clientID);
        }


        //Change Password
        public async Task ChangePassword(int clientId, string currentPassword, string newPassword)
        {
            var client = await database.Table<Client>().FirstOrDefaultAsync(c => c.ClientID == clientId && c.Password == currentPassword);

            if (client != null)
            {
                // Update the password
                client.Password = newPassword;

                // Save changes to the database
                await database.UpdateAsync(client);
            }
        }

        //Update Client
        public async Task UpdateClient(Client client)
        {
            await database.UpdateAsync(client);
        }

        //Get employee
        public async Task<Client> GetClientById(int ClientID)
        {
            return await database.Table<Client>().FirstOrDefaultAsync(x => x.ClientID == ClientID);
        }

        #endregion

        //Employee

        #region EmployeeTasks
        //Login
        public async Task<Employee> EmployeeLogin(string email, string password)
        {
            return await database.Table<Employee>().Where(e => e.Email == email && e.Password == password).FirstOrDefaultAsync();
        }
        //RegEmployee
        public async Task<int> EmployeReg(Employee employee)
        {
            return await database.InsertAsync(employee);
        }

        //Display Employees
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await database.Table<Employee>().ToListAsync();
        }

        //Display Employee
        public async Task<Employee> DisplayEmployee(int empID)
        {
            return await database.Table<Employee>().FirstOrDefaultAsync(p => p.EmployeeID == empID);
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            return await database.UpdateAsync(employee);
        }
        #endregion

        public async Task<Employee> GetEmployeeById(int EmployeeID)
        {
            return await database.Table<Employee>().FirstOrDefaultAsync(z => z.EmployeeID == EmployeeID);
        }

        #region Admin
        //Admin Login
        public async Task<Administration> AdminLogin(string password, int adminID)
        {
            return await database.Table<Administration>().Where(a => a.Password == password && a.AdminID == adminID).FirstOrDefaultAsync();
        }
        #endregion


        #region SQLs
        public async Task<List<Employee>> ServiceSearch(string Service)
        {
            string query = $"SELECT * FROM Employee WHERE Service = ?";
            var result = await database.QueryAsync<Employee>(query, Service);

            return result.ToList();
        }
        #endregion
    }
}
