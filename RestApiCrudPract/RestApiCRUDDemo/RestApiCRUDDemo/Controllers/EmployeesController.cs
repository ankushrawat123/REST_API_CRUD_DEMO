using Microsoft.AspNetCore.Mvc;
using RestApiCRUDDemo.EmployeesData;
using RestApiCRUDDemo.Models;
using System;

namespace RestApiCRUDDemo.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;

        //here injecting our Iemployeedata into our controller that we r adding to our services
        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        //1st Method creating GetEmployess 
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());//wrapping in ok object
        }


        //2nd controller Method
        //creating single get employee method
        [HttpGet]
        [Route("api/[controller]/{id}")]//need particular id of employee that we want to fetch
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);    //here employee is getting return or not checking
            if(employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with Id:{id} was not found");//otherwise returning not found employee 
        }


        //3rd Controller Method

        [HttpPost]
        [Route("api/[controller]")]//No need of id of employee
        public IActionResult GetEmployee(Employee employee)//need Employee model as part of request
        {
            //here we want to create so using AddEmployee method 
            //here getting employee parameter from client
            _employeeData.AddEmployee(employee);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
            //returning server has created your request
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]//need particular id of employee that we want to delete
        public IActionResult DeleteEmployee(Guid id)//need Employee model as part of request
        {
            //here first we will check whether employee exist 
            //here getting employee parameter from client
            //here we will get employee id from Guid
            var employee = _employeeData.GetEmployee(id);

            if (employee != null)
            {
                _employeeData.DeleteEmployee(employee);
                return Ok();

            }
            return NotFound($"employee with Id:{id} was not found");

            //returning server has created your request
        }


        [HttpPatch]
        [Route("api/[controller]/{id}")]//need particular id of employee that we want to edit
        public IActionResult EditEmployee(Guid id,Employee employee)
        {
            //here first we will check whether employee exist 
            //here getting employee parameter from client
            //here we will get employee id from Guid
            var existingEmployee = _employeeData.GetEmployee(id);

            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);
           

            }
            return Ok(employee);
            //returning server has created your request
        }
    }
}
