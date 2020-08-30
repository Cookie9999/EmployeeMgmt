 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestApplication.Context;

namespace TestApplication.Models
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly hSenidEntities dbContext_;
        public EmployeeRepo(hSenidEntities dbContext)
        {
            dbContext_ = dbContext;
        }
        public bool CreateEmployee(EmployeeDTO employee)
        {
            Employee employeeCreate = new Employee
            {
                EmployeeName = employee.EmployeeName,
                EmployeeDOB = DateTime.Parse(employee.DOB),
                PositionId = Int32.Parse(employee.Position)
            };
            dbContext_.Employees.Add(employeeCreate);
            return Save();
        }
    
        public bool DeleteEmployee(int employee_id)
        {
            var result = dbContext_.Employees.Where(a => a.EmployeeId == employee_id).FirstOrDefault();
            dbContext_.Employees.Remove(result);
            return Save();
        }

        public EmployeeDTO GetEmployee(int employee_id)
        {
            var result = dbContext_.Employees.Where(a => a.EmployeeId == employee_id).FirstOrDefault();
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeId = result.EmployeeId,
                EmployeeName = result.EmployeeName,
                DOB = result.EmployeeDOB.ToString("yyyy-MM-dd"),
                Position = result.PositionId.ToString()
            };
            return employee;
        }

        public ICollection<EmployeeDTO> GetEmployees()
        {
            var result = dbContext_.Employees.OrderBy(a => a.EmployeeId).ToList();

            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var item in result)
            {
                EmployeeDTO employee = new EmployeeDTO();
                employee.EmployeeId = item.EmployeeId;
                employee.EmployeeName = item.EmployeeName;
                employee.DOB = item.EmployeeDOB.ToString();
                employee.Position = item.Position.PositionDescription;
                employeeDTOs.Add(employee);
            }
            return employeeDTOs;
        }

        public bool Save()
        {
            var save = dbContext_.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateEmployee(EmployeeDTO employee)
        {
            Employee employeeUpdate = new Employee();
            employeeUpdate.EmployeeId = employee.EmployeeId;
            employeeUpdate.EmployeeName = employee.EmployeeName;
            employeeUpdate.EmployeeDOB = DateTime.Parse(employee.DOB);
            employeeUpdate.PositionId = Int32.Parse(employee.Position);
            dbContext_.Entry(employeeUpdate).State = EntityState.Modified;
            return Save();
        }
    }
}