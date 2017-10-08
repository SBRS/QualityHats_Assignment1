using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityHats.Data;
using QualityHats.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace QualityHats.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public HatsController(ApplicationDbContext context, IHostingEnvironment hEnv)
        {
            _context = context;
            _hostingEnv = hEnv;
        }

        // GET: Hats
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            ViewData["CurrentFilter"] = searchString;

            var hats = from h in _context.Hats
                           select h;

            if (!String.IsNullOrEmpty(searchString))
            {
                hats = hats.Where(s => s.HatName.Contains(searchString) || s.Description.Contains(searchString));
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
            return View(await PaginatedList<Hat>.CreateAsync(hats.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Hats/Details/5
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

        // GET: Hats/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers.AsNoTracking(), "SupplierID", "SupplierName");
            return View();
        }

        // POST: Hats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,Description,HatName,ImageName,SupplierID,UnitPrice")] Hat hat, IList<IFormFile> _files)
        {
            hat.ImagePath = await LoadImage(_files);

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(hat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers.AsNoTracking(), "SupplierID", "SupplierName", hat.SupplierID);
            return View(hat);
        }

        // GET: Hats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hats.SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers.AsNoTracking(), "SupplierID", "SupplierName", hat.SupplierID);
            return View(hat);
        }

        // POST: Hats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HatID,CategoryID,Description,HatName,SupplierID,UnitPrice,ImagePath")] Hat hat, IList<IFormFile> _files)
        {
            if (id != hat.HatID)
            {
                return NotFound();
            }

            if (_files.Count != 0)
            {
                hat.ImagePath = await LoadImage(_files);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }

            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers.AsNoTracking(), "SupplierID", "SupplierName", hat.SupplierID);
            return View(hat);
        }

        // GET: Hats/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
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

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again, and if the problem persists " + "see your system administrator.";
            }

            return View(hat);
        }

        // POST: Hats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hat = await _context.Hats
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.HatID == id);

            if (hat == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Hats.Remove(hat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData["HatUsed"] = "The Hat being deleted has been used in previous orders. Delete those orders before trying again.";
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool HatExists(int id)
        {
            return _context.Hats.Any(e => e.HatID == id);
        }

        public async Task<string> LoadImage(IList<IFormFile> _files)
        {
            var relativeName = "";
            var fileName = "";

            if (_files.Count < 1)
            {
                relativeName = "/images/Default.jpg";
            }
            else
            {
                foreach (var file in _files)
                {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    relativeName = "/images/HatImages/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }

            return relativeName;
        }
    }
}
