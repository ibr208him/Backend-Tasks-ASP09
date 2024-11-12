using learning_3.Data;
using learning_3.DTOs.Department;
using learning_3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learning_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext dBContext;

        public DepartmentsController(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = dBContext.departments.Select(department =>
            
                new GetDepartmentsDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                }
            );
            return Ok(departments);
        }

        [HttpGet("GetDetails")]
        public IActionResult GetById(int id)
        {
            var department = dBContext.departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            GetDepartmentsDTO getDepartmentsDTO = new GetDepartmentsDTO()
            {
                Id = department.Id,
                Name = department.Name
            };
            return Ok(getDepartmentsDTO);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDTO createDepartmentDTO)
        {
            Department department = new Department()
            {
                Name = createDepartmentDTO.Name,

            };
            dBContext.departments.Add(department);
            dBContext.SaveChanges();
            return Ok(createDepartmentDTO);
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateDepartmentDTO updateDepartmentDTO)
        {
            var departmentInDatabase = dBContext.departments.Find(updateDepartmentDTO.Id);
            if (departmentInDatabase == null)
            {
                return NotFound();
            }
            departmentInDatabase.Name = updateDepartmentDTO.Name;
            dBContext.SaveChanges();
            return Ok(updateDepartmentDTO);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var department = dBContext.departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            dBContext.Remove(department);
            dBContext.SaveChanges();
            return Ok("department deleted successfully");
        }
    }
}
