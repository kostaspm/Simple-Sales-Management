using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simple_Sales_Management.Models;
using Simple_Sales_Management.Services.CommisionServices;
using Simple_Sales_Management.ViewModels.Seller;
using System.Globalization;

namespace Simple_Sales_Management.Controllers
{
    public class SellerController : Controller
    {
        private readonly SaleDbContext _context;
        private readonly ICommisionService _commisionService;

        public SellerController(SaleDbContext context, ICommisionService commisionService)
        {
            _context = context;
            _commisionService = commisionService;
        }

        // GET: Seller
        public async Task<IActionResult> Index()
        {
            if (_context.Sellers != null)
            {
                List<SellerIndexViewModel> sellerViewModelList = new List<SellerIndexViewModel>();
                List<Seller> sellers = await _context.Sellers.Include(m => m.Sales).ToListAsync();

                foreach (var seller in sellers)
                {
                    SellerIndexViewModel sellerViewModel = new()
                    {
                        SellerId = seller.SellerId,
                        FirstName = seller.FirstName,
                        LastName = seller.LastName,
                        Commision = _commisionService.CalculateCommision(seller)
                    };
                    //var commision = _commisionService.CalculateCommision(seller);
                    sellerViewModelList.Add(sellerViewModel);
                }
                return View(sellerViewModelList);
            }
            return Problem("Entity set 'SaleDbContext.Sellers'  is null.");
            //return _context.Sellers != null ? 
            //              View(await _context.Sellers.ToListAsync()) :
            //              Problem("Entity set 'SaleDbContext.Sellers'  is null.");
        }

        // GET: Seller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.Include(f => f.Sales)
                .FirstOrDefaultAsync(m => m.SellerId == id);

            if (seller == null)
            {
                return NotFound();
            }

            var viewModel = new SellerDetailsViewModel { SellerId = seller.SellerId, FirstName = seller.FirstName, LastName = seller.LastName };
            if (seller.Sales != null)
            {
                if(seller.Sales.Count == 0)
                {
                    ViewBag.ZeroSales = "This seller did not perform any sale this year.";
                    viewModel.SellerDetails = new List<MonthlyInfo>();
                    return View(viewModel);
                }
                var sellerDetails = seller.Sales.Where(x => x.SaleDate.Year == DateTime.Now.Year).GroupBy(x => x.SaleDate.Month).Select(m => new MonthlyInfo {
                    MonthId = m.Key,
                    MonthName = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(m.Key),
                    MonthlyCommision = _commisionService.CalculateMonthlyCommision(m.Select(f => f.Amount)),
                    MonthlySales = m.Count()
                }).OrderBy(m => m.MonthId);

                //var query = from s in seller.Sales
                //            where s.SaleDate.Year == DateTime.Now.Year
                //            group s by s.SaleDate.Month into g
                //            select new MonthlyInfo
                //            { 
                //              MonthName = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(g.Key),
                //              MonthlyCommision = _commisionService.CalculateMonthlyCommision(g.Select(m => m.Amount)),
                //              MonthlySales = g.Count()
                //            };
                viewModel.SellerDetails = sellerDetails.ToList();
            }

            return View(viewModel);
        }

        // GET: Seller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerId,FirstName,LastName")] Seller seller)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors); //to check the errors
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Seller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Seller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellerId,FirstName,LastName")] Seller seller)
        {
            if (id != seller.SellerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.SellerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Seller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'SaleDbContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return (_context.Sellers?.Any(e => e.SellerId == id)).GetValueOrDefault();
        }
    }
}
