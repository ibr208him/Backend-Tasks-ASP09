using learning_3.Data;
using learning_3.DTOs.Employee;
using learning_3.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learning_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dBContext;

        public EmployeesController(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees=dBContext.employees.ToList();
            var getEmployeesDTO = employees.Adapt<IEnumerable<GetEmployeesDTO>>();
            return Ok(getEmployeesDTO);
        }

        [HttpGet("GetDetails")]
        public IActionResult GetById(int id)
        {
            var employee = dBContext.employees.Find(id);
            if(employee == null)
            {
                return NotFound();
            }
            var getEmployeeDTO = employee.Adapt<GetEmployeesDTO>();
            return Ok(getEmployeeDTO);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDTO createEmployeeDTO)
        {
           var employee=createEmployeeDTO.Adapt<Employee>();
            dBContext.employees.Add(employee);
           dBContext.SaveChanges();
            return Ok(createEmployeeDTO);
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateEmployeeDTO updateEmployeeDTO)
        {
            var employeeInDatabase=dBContext.employees.Find(updateEmployeeDTO.Id);
            if (employeeInDatabase == null)
            {
                return NotFound();
            }
            var employee= employeeInDatabase.Adapt<Employee>();
            dBContext.SaveChanges();
            return Ok(updateEmployeeDTO);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
           var employee= dBContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dBContext.Remove(employee);
            dBContext.SaveChanges();
            return Ok("employee deleted successfully");
        }

    }
}
