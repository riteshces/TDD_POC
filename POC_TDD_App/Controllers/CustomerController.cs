using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POC_TDD_App.Data;
using POC_TDD_App.Model;

namespace POC_TDD_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CustomerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _appDbContext.Customers.ToListAsync();
            if (customers == null || !customers.Any())
            {
                return NotFound();
            }
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _appDbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerModel customer)
        {
            _appDbContext.Customers.Add(customer);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerModel customer)
        {
            var filtercustomer = await _appDbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (filtercustomer == null)
            {
                return NotFound();
            }
            if (filtercustomer.Id != id)
            {
                return BadRequest();
            }
            filtercustomer.Name= customer.Name;
            filtercustomer.Email= customer.Email;
            filtercustomer.Phone= customer.Phone;
            filtercustomer.City= customer.City;
            _appDbContext.SaveChangesAsync();
            filtercustomer = await _appDbContext.Customers.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return Ok(filtercustomer);
        }
    }
}
