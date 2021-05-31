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
    public class BookingController : ControllerBase
    {
        private readonly Context _context;

        public BookingController(Context context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet]
        [Route("BookingId")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        [HttpGet]
        [Route("customerId")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingByCustomerId(int customerId)
        {
            var booking = _context.Bookings.Where(e => e.CustomerId == customerId).ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }
        private bool CustomerIdExists(int customerId)
        {
            return _context.Bookings.Any(e => e.CustomerId == customerId);
        }
        [HttpGet]
        [Route("executiveId")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingByExecutiveId(int executiveId)
        {
            var booking = _context.Bookings.Where(e => e.ExecutiveId == executiveId).ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }
        [HttpGet]
        [Route("WithCompletedRequestsOfEachExecutive")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetCompletetedRequestsOfEachExecutive(int executiveId)
        {
            var booking = _context.Bookings.Where(e => e.ExecutiveId == executiveId && e.DeliveryStatus=="Completed").ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }
        [HttpGet]
        [Route("WithPendingRequestsOfEachExecutive")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetPendingRequestsOfEachExecutive(int executiveId)
        {
            var booking = _context.Bookings.Where(e => e.ExecutiveId == executiveId && e.DeliveryStatus != "Completed").ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }
        [HttpGet]
        [Route("WithCompletedDelivery")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingWithCompletedDelivery(int executiveId)
        {
            var booking = _context.Bookings.Where(e=> e.DeliveryStatus=="Completed").ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }
        [HttpGet]
        [Route("WithUnCompletedDelivery")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingWithUnCompletedDelivery(int executiveId)
        {
            var booking = _context.Bookings.Where(e => e.DeliveryStatus != "Completed").ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }
        [HttpGet]
        [Route("WithStatusCheck")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingByExecutiveIdWithStatusCheck()
        {
            var booking = _context.Bookings.Where(e => e.DeliveryStatus == "Completed").ToList();
            if (booking == null)
            {
                return NotFound();
            }

            return booking;

        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("PutBooking")]
        public async Task<ActionResult<Booking>> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return booking;
        }
        [HttpPut]
        [Route("UpdateDeliveryStatus")]
        public async Task<ActionResult<Booking>> UpdateDeliveryStatus(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return booking;
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddBooking")]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

    }
}
