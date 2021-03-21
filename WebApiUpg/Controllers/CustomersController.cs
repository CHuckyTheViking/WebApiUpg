using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Data;
using SharedLibrary.Models.Customer;

namespace WebApiUpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CustomersController(SqlDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerModel model)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    Phone = model.Phone,
                    Contact = model.Contact
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult AllCustomers()
        {
            try
            {

                var customers = _context.Customers.ToList();
                return new OkObjectResult(customers);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
