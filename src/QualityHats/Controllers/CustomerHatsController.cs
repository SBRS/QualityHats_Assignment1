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

namespace QualityHats.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Customer")]

    public class CustomerHatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerHatsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: CustomerHats
        public async Task<IActionResult> Index(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentCategory"] = "All Hats";
            ViewData["CurrentFilter"] = searchString;

            var hats = from h in _context.Hats
                       select h;

            if (!String.IsNullOrEmpty(searchString))
            {
                hats = hats.Where(s => s.HatName.Contains(searchString) || s.Description.Contains(searchString));
            }

            if (id != null)
            {
                hats = hats.Where(s => s.CategoryID.Equals(id));
                ViewData["CurrentCategory"] = _context.Categories.Where(i => i.CategoryID == id.Value).Single().CategoryName;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    hats = hats.OrderByDescending(s => s.HatName);
                    break;
                case "Price":
                    hats = hats.OrderBy(s => s.UnitPrice);
                    break;
                case "price_desc":
                    hats = hats.OrderByDescending(s => s.UnitPrice);
                    break;
                default:
                    hats = hats.OrderBy(s => s.HatName);
                    break;
            }

            int pageSize = 3;
            ViewBag.Categories = _context.Categories.AsNoTracking().ToList();
            return View(await PaginatedList<Hat>.CreateAsync(hats.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: CustomerHats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hats
                .Include(h => h.Category)
                .Include(h => h.Supplier)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }

            return View(hat);
        }
    }
}
