using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Context;

namespace TestApplication.Models
{
    public interface IEmployeeRepo
    {
        ICollection<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(int employee_id);
        bool CreateEmployee(EmployeeDTO employee);
        bool UpdateEmployee(EmployeeDTO employee);
        bool DeleteEmployee(int employee_id);
        bool Save();
    }
}
