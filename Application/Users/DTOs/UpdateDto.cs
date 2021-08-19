using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.DTOs
{
    public class UpdateDto
    {
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public decimal MonthSalary { get; set; }
        public Address Address { get; set; }
    }
}
