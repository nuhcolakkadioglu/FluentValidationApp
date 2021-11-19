using FluentValidation;
using FluentValidationApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _customerValidator;
        public CustomersApiController(AppDbContext appDbContext, IValidator<Customer> customerValidator)
        {
            _context = appDbContext;
            _customerValidator = customerValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {

            return await _context.Customers.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCostumer(Customer customer)
        {
            var valid = _customerValidator.Validate(customer);
            if (!valid.IsValid)
            {
                return BadRequest(valid.Errors.Select(m => new
                {
                        HataKodu = m.ErrorCode,
                        HataMesaji = m.ErrorMessage,
                        Alan = m.PropertyName
                }));
            }
            var data = _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}
