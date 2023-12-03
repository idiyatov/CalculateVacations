using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateVacations
{
    internal class Employee
    {
        public string Name { get; set; }
        public int NumberOfVacationDays { get; set; }
        public List<DateTime> VacationDays { get; set; }

        public Employee(string name)
        {
            Name = name;
            NumberOfVacationDays = 28;
            VacationDays = new List<DateTime>();
        }
    }
}
