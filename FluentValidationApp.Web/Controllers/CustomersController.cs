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
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _validator;
         public CustomersController(AppDbContext appDbContext, IValidator<Customer> validator)
        {
            _context = appDbContext;
            _validator = validator;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(int? id)
        {

         

            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            var result = _validator.Validate(customer);

            if (result.IsValid)
            {
                {

                    try
                    {
                        _context.Customers.Add(customer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        return View(ex.Message);
                    }
                }
            }
            // bu şekildede kullanabilrisn 
            //if (ModelState.IsValid)
            //{

            //    try
            //    {
            //        _context.Customers.Add(customer);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    catch (Exception ex)
            //    {
            //        return View(ex.Message);
            //    }
            //}
            return View(customer);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customers.Update(customer);
                   await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            return View(customer);

        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var customer = await _context.Customers.FindAsync(id);

            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
