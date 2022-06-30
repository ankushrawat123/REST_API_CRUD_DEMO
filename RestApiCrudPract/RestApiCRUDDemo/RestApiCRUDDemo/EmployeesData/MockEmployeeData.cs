using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiCRUDDemo.EmployeesData
{
    public class MockEmployeeData : IEmployeeData
    {

        //For implementation  of getEmployee controller Method so creating couple of employees
        private List<Employee> employees = new List<Employee>()
      { 
        new Employee()
          {
            Id = Guid.NewGuid(),
            Name =  "Employee one"
          },
        //Adding another employee
        new Employee()
            {
            Id = Guid.NewGuid(),
            Name ="Employee Two"
          }
      };

        //implementing 3rd controller method
        //implementing the mock repository 
        public Employee AddEmployee(Employee employee)
        {
            //client don't have id so we should give id to newly created employee here
            employee.Id = Guid.NewGuid();
          employees.Add(employee);
            return employee;
            //even though we r not using it we can change it in void return type
        }

        public void DeleteEmployee(Employee employee)
        {
          employees.Remove(employee);
        }

        public Employee EditEmployee(Employee employee)
        {
            //calling GetEmployee method
            var existingEmployee = GetEmployee(employee.Id);
            existingEmployee.Name = employee.Name;
            return existingEmployee;
        }

        public Employee GetEmployee(Guid id)
        {//from employee controller getEmployee method coming here to class inherited from interface
            return employees.SingleOrDefault(x => x.Id == id);//if employee with this id is found then return employee otherwise return status code=404
        }

      

        public List<Employee> GetEmployees()
        {
           return employees;
        }
    }
}
