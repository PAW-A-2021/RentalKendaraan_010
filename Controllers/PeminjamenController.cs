using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan.Models;

namespace RentalKendaraan.Controllers
{
    public class PeminjamenController : Controller
    {
        private readonly Rent_KendaraanContext _context;

        public PeminjamenController(Rent_KendaraanContext context)
        {
            _context = context;
        }

        // GET: Peminjamen
        public async Task<IActionResult> Index(string pmjm, string searchString)
        {
            //buat list menyimpan tanggal
            var pmjmList = new List<string>();
            //query
            var pmjmQuery = from d in _context.Peminjamen orderby d.TglPeminjaman select d.TglPeminjaman.ToString();

            pmjmList.AddRange(pmjmQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.pmjm = new SelectList(pmjmList);

            var menu = from m in _context.Peminjamen.Include(k => k.IdCustomerNavigation).Include(k => k.IdJaminanNavigation).Include(k => k.IdKendaraanNavigation) select m;

            //untuk memilih dropdownlist gender
            if (!string.IsNullOrEmpty(pmjm))
            {
                menu = menu.Where(x => x.TglPeminjaman.ToString() == pmjm);
            }

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s =>s.IdCustomerNavigation.NamaCustomer.Contains(searchString) || s.IdJaminanNavigation.NamaJaminan.Contains(searchString) || s.IdKendaraanNavigation.NamaKendaraan.Contains(searchString) || s.Biaya.ToString().Contains(searchString));
            }

            return View(await menu.ToListAsync());
        }

        // GET: Peminjamen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman = await _context.Peminjamen
                .Include(p => p.IdCustomerNavigation)
                .Include(p => p.IdJaminanNavigation)
                .Include(p => p.IdKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdPeminjaman == id);
            if (peminjaman == null)
            {
                return NotFound();
            }

            return View(peminjaman);
        }

        // GET: Peminjamen/Create
        public IActionResult Create()
        {
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer");
            ViewData["IdJaminan"] = new SelectList(_context.Jaminans, "IdJaminan", "IdJaminan");
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraans, "IdKendaraan", "IdKendaraan");
            return View();
        }

        // POST: Peminjamen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeminjaman,TglPeminjaman,IdKendaraan,IdCustomer,IdJaminan,Biaya")] Peminjaman peminjaman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peminjaman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", peminjaman.IdCustomer);
            ViewData["IdJaminan"] = new SelectList(_context.Jaminans, "IdJaminan", "IdJaminan", peminjaman.IdJaminan);
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraans, "IdKendaraan", "IdKendaraan", peminjaman.IdKendaraan);
            return View(peminjaman);
        }

        // GET: Peminjamen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman = await _context.Peminjamen.FindAsync(id);
            if (peminjaman == null)
            {
                return NotFound();
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", peminjaman.IdCustomer);
            ViewData["IdJaminan"] = new SelectList(_context.Jaminans, "IdJaminan", "IdJaminan", peminjaman.IdJaminan);
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraans, "IdKendaraan", "IdKendaraan", peminjaman.IdKendaraan);
            return View(peminjaman);
        }

        // POST: Peminjamen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeminjaman,TglPeminjaman,IdKendaraan,IdCustomer,IdJaminan,Biaya")] Peminjaman peminjaman)
        {
            if (id != peminjaman.IdPeminjaman)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peminjaman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeminjamanExists(peminjaman.IdPeminjaman))
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
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", peminjaman.IdCustomer);
            ViewData["IdJaminan"] = new SelectList(_context.Jaminans, "IdJaminan", "IdJaminan", peminjaman.IdJaminan);
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraans, "IdKendaraan", "IdKendaraan", peminjaman.IdKendaraan);
            return View(peminjaman);
        }

        // GET: Peminjamen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman = await _context.Peminjamen
                .Include(p => p.IdCustomerNavigation)
                .Include(p => p.IdJaminanNavigation)
                .Include(p => p.IdKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdPeminjaman == id);
            if (peminjaman == null)
            {
                return NotFound();
            }

            return View(peminjaman);
        }

        // POST: Peminjamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peminjaman = await _context.Peminjamen.FindAsync(id);
            _context.Peminjamen.Remove(peminjaman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeminjamanExists(int id)
        {
            return _context.Peminjamen.Any(e => e.IdPeminjaman == id);
        }
    }
}
