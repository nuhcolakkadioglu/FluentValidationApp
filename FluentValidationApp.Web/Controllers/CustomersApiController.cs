using AutoMapper;
using FluentValidation;
using FluentValidationApp.Web.Dtos;
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
        private readonly IMapper _mapper;
        public CustomersApiController(AppDbContext appDbContext, IValidator<Customer> customerValidator, IMapper mapper)
        {
            _context = appDbContext;
            _customerValidator = customerValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {

            var result = _mapper.Map<IEnumerable<CustomerDto>>(await _context.Customers.ToListAsync());

            return result.ToList();
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
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


        [HttpPut]
        public async Task<ActionResult<Customer>> UpdateCustumer(Customer customer)
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
            var data = _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
