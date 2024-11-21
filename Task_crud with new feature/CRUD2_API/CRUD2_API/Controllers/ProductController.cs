using CRUD2_API.Data;
using CRUD2_API.DTOs.Product;
using CRUD2_API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace CRUD2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ProductHeaderValue> logger;

        public ProductController(ApplicationDbContext context,ILogger<ProductHeaderValue> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
          //  throw new Exception("this is for test errors");
            logger.LogInformation("hello");
           
            var result = await context.Products.ToListAsync();
            var products = result.Adapt<List<GetAllPrpductDTO>>();
            return Ok(products);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductDTO creatProductDTO)
        {
            var product = creatProductDTO.Adapt<Product>();
            var result = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            if(result is null)
            {
                
                return BadRequest();
            }
            return Ok(product);   
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var product=await context.Products.FindAsync(id);
            if(product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CreateProductDTO createProductDTO)
        {
            var product = await context.Products.FindAsync(createProductDTO.Id);
            if (product is null)
            {
                return NotFound();
            }
            var result= product.Adapt<CreateProductDTO>();
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }
             context.Products.Remove(product);
            context.SaveChanges();
            return Ok();
        }
    }
}
