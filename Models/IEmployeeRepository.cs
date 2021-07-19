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
                var sqlQuery = "INSERT INTO Department (Name, Phone) VALUES (@Name, @Phone); SELECT MAX(Id) FROM Department";
                int departmentId = db.Query<int>(sqlQuery, employee.Department).FirstOrDefault();
                employee.DepartmentId = departmentId;

                sqlQuery = "INSERT INTO Staff (Name, Surname, Phone, CompanyId, DepartmentId) VALUES(@Name, @Surname, @Phone, @CompanyId, @DepartmentId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int employeeId = db.Query<int>(sqlQuery, employee).FirstOrDefault();

                employee.Passport.EmployeeId = employeeId;
                sqlQuery = "INSERT INTO Passport (EmployeeId, Type, Number) VALUES(@EmployeeId, @Type, @Number)";
                db.Execute(sqlQuery, employee.Passport);

                
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