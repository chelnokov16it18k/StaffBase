using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffBase.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public Passport Passport { get; set; }
        public Department Department { get; set; }

    }
}
