using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityHats.Data;
using QualityHats.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace QualityHats.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders
                .Include(i => i.User)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Orders/Create
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            string userId = _userManager.GetUserId(User);
            ViewBag.userInfo = _context.ApplicationUser.Where(i => i.Id == userId).AsNoTracking().Single();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Order order)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<OrderDetail> details = new List<OrderDetail>();
                foreach (CartItem item in items)
                {

                    OrderDetail detail = CreateOrderDetailForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);

                }

                order.User = user;
                order.OrderDate = DateTime.Now;
                order.Status = Status.Waiting;
                order.Subtotal = ShoppingCart.GetCart(this.HttpContext).GetTotal(_context);
                order.GST = ShoppingCart.GetCart(this.HttpContext).GetGST(_context);
                order.GrandTotal = ShoppingCart.GetCart(this.HttpContext).GetGrandTotal(_context);
                order.OrderDetails = details;
                _context.SaveChanges();


                return RedirectToAction("Purchased", new RouteValueDictionary(
                new { action = "Purchased", id = order.OrderID }));
            }

            return View(order);
        }

        private OrderDetail CreateOrderDetailForThisItem(CartItem item)
        {

            OrderDetail detail = new OrderDetail();


            detail.Quantity = item.Count;
            detail.Hat = item.Hat;
            detail.UnitPrice = item.Hat.UnitPrice;

            return detail;

        }
        public async Task<IActionResult> Purchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderDetails.Where(detail => detail.Order.OrderID == order.OrderID).Include(detail => detail.Hat).ToList();

            order.OrderDetails = details;
            ShoppingCart.GetCart(this.HttpContext).EmptyCart(_context);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(Status)), order.Status);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderToUpdate = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);

            if (await TryUpdateModelAsync<Order>(
                orderToUpdate,
                "",
                o => o.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(Status)), orderToUpdate.Status);
            return View(orderToUpdate);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
