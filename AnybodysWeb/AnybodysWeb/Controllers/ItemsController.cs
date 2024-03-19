using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnybodysModels;
using AnybodysWeb.Data;
using AnybodysWebDBLibrary;

namespace AnybodysWeb.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AnybodysDbContext _context;
        private readonly SelectList _categories;

        public ItemsController(AnybodysDbContext context)
        {
            _context = context;
            _categories = new SelectList(_context.Categories, "Id", "Name");
        }

        // GET: Top10 Items
        public async Task<IActionResult> Top10Index()
        {
            return View(await _context.Items
                .Include(x => x.Category)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Take(10)
                .ToListAsync());
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            // select * from Items 
            //return View(await _context.Items.ToListAsync());
            // select * from items inner join categories on items.id = categories.id
            return View(await _context.Items
                .Include(x => x.Category)
                .ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["CategoryId"] = _categories;
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(x => x.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = _categories;
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,CategoryId")] Item item)
        {
            ViewData["CategoryId"] = _categories;
            if (item.CategoryId is null)
            {
                ModelState.AddModelError("CategoryId", "Category id Required");
                /*ViewData["CategoryId"] = _categories;*/
                return View(item);
            }
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CategoryId"] = _categories;
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, ImagePath, CategoryId")] Item item)
        {
            ViewData["CategoryId"] = _categories;
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(x => x.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
