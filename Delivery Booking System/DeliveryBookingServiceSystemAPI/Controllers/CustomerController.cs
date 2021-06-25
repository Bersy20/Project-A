using DeliveryBookingServiceSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingServiceSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Context _context;

        public CustomerController(Context context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        [Route("WithVerifiedStatus")]

        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersWithVerifiedStatus()
        {
            var customers = _context.Customers.Where(e => e.IsVerified == true).ToList();
            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }
        [HttpGet]
        [Route("WithoutVerifiedStatus")]

        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersWithoutVerifiedStatus()
        {
            var customers = _context.Customers.Where(e => e.IsVerified == false).ToList();
            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }
        [HttpGet]
        [Route("AllCustomers")]

        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet]
        [Route("CustomerById")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [Route("UpdatePassword")]
        [HttpPut]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {
            Customer customer = new Customer();
            customer=_context.Customers.Where(e => e.CustomerId ==id).SingleOrDefault();
            customer.Password = password;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("PostCustomer")]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var check = _context.Customers.Where(e => e.CustomerEmail == customer.CustomerEmail).SingleOrDefault();
            if (check.CustomerEmail != customer.CustomerEmail)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
            }
            return NoContent();
        }
        [Route("LoginCustomer")]
        [HttpPost]
        public async Task<ActionResult<Customer>> LoginCustomer(Customer customer)
        {
            if (Verification(customer))
            {
                try
                {
                    customer = _context.Customers.SingleOrDefault(u => u.CustomerEmail == customer.CustomerEmail && u.Password == customer.Password);
                    if (customer == null)
                        return NotFound();
                    return customer;
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }        
        private bool Verification(Customer customer)
        {
            try
            {
                customer = _context.Customers.SingleOrDefault(u => u.CustomerEmail == customer.CustomerEmail);
                if (customer == null)
                    return false;
                else
                {
                    if (customer.IsVerified)
                        return true;
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
