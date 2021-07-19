using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffBase.Models
{
    public class CreateViewModel
    {
        public Employee employee { get; set; }
        public Passport passport { get; set; }
        public Department department { get; set; }
    }
}
