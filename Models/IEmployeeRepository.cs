using Dapper;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace StaffBase.Models
{
    public interface IEmployeeRepository
    {
        void Create(Employee employee);
        void Delete(int id);
        Employee Get(int id);
        List<Employee> GetStaff();
        void Update(Employee employee);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        string connectionString = null;
        public EmployeeRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Employee> GetStaff()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Employee>("SELECT * FROM Staff").ToList();
            }
        }

        public Employee Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Employee>("SELECT * FROM Staff WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                /*var sqlQuery = "INSERT INTO Staff (Name, Surname, Phone) VALUES(@Name, @Surname, @Phone)";
                db.Execute(sqlQuery, employee);*/

                // работа с id
                employee.DepartmentId = employee.Department.id;
                var sqlQuery = "INSERT INTO Staff (Name, Surname, Phone, CompanyId) VALUES(@Name, @Surname, @Phone, @CompanyId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int employeeId = db.Query<int>(sqlQuery, employee).FirstOrDefault();

                sqlQuery = "INSERT INTO Passport (EmployeeId, Type, Number) VALUES(@EmployeeId, @Type, @Number)";
                employee.Passport.EmployeeID = employee.Id;
                db.Execute(sqlQuery, employee.Passport);

                sqlQuery = "INSERT INTO Department (Name, Phone) VALUES (@Name, @Phone)";
                db.Execute(sqlQuery, employee.Department);

            }
        }

        public void Update(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Staff SET Name = @Name, Surname = @Surname, Phone = @Phone, CompanyId = @CompanyId WHERE Id = @Id";
                db.Execute(sqlQuery, employee);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Staff WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
