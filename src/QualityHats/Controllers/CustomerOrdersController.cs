using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityHats.Data;
using QualityHats.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace QualityHats.Controllers
{
    [Authorize(Roles = "Customer")]

    public class CustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CustomerOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CustomerOrders
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var order = _context.Orders
             .Where(o => o.User == user)
             .Include(i => i.User)
             .ToList();

            return View(order);
        }

        // GET: CustomerOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(i => i.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            var details = _context
                .OrderDetails
                .Where(detail => detail.Order.OrderID == order.OrderID)
                .Include(detail => detail.Hat).ToList();

            order.OrderDetails = details;

            return View(order);
        }
    }
}
