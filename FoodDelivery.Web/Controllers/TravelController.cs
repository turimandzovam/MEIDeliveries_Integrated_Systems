using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data; // Your TravelAgencyDbContext
using FoodDelivery.Web.Models; // Models from Travel Agency Database

namespace FoodDelivery.Web.Controllers
{
    public class TravelController : Controller
    {
        private readonly TravelAgencyDbContext _context;

        // Inject the TravelAgencyDbContext into the controller
        public TravelController(TravelAgencyDbContext context)
        {
            _context = context;
        }

        // Action to retrieve all accommodations
        public async Task<IActionResult> Accommodations()
        {
            var accommodations = await _context.Accommodations.ToListAsync(); // Get all Accommodations
            return View(accommodations); // Pass accommodations to the view
        }

        // Action to retrieve all travel packages and show them on the same page
        public async Task<IActionResult> Index()
        {
            var travelPackages = await _context.TravelPackages
                .Include(tp => tp.Accommodation) // Eager load Accommodation related to the TravelPackage
                .ToListAsync(); // Get all TravelPackages with Accommodation details

            return View(travelPackages); // Pass travel packages with accommodation details to the view
        }

        // Action to retrieve a single accommodation by its ID (int instead of Guid)
        public async Task<IActionResult> AccommodationDetails(Guid id)  // Use Guid instead of int
        {
            var accommodation = await _context.Accommodations
                .FirstOrDefaultAsync(a => a.Id == id);  // Correct comparison for Guid

            if (accommodation == null)
            {
                return NotFound(); // If no accommodation found
            }

            return View(accommodation); // Return the details view with the accommodation
        }


        // Action to retrieve details for a specific travel package (optional)
        public async Task<IActionResult> TravelPackageDetails(Guid id)  // Use Guid instead of int
        {
            var travelPackage = await _context.TravelPackages
                .Include(tp => tp.Accommodation) // Include accommodation in travel package details
                .FirstOrDefaultAsync(tp => tp.Id == id);  // Correct comparison for Guid

            if (travelPackage == null)
            {
                return NotFound(); // If no travel package found
            }

            return View(travelPackage); // Return the view with the travel package details
        }

    }
}
