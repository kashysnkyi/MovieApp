using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MovieApp.Models;
using ClosedXML.Excel;

namespace MovieApp.Controllers
{
    public class FilmsController : Controller
    {
        private readonly MovieDbContext _context;

        public FilmsController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            return View(await _context.Films.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.Id == id);

            if (film == null) return NotFound();

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Duration,Description,ReleaseDate")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var film = await _context.Films.FindAsync(id);
            if (film == null) return NotFound();

            return View(film);
        }

        // POST: Films/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Duration,Description,ReleaseDate")] Film film)
        {
            if (id != film.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.Id == id);

            if (film == null) return NotFound();

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film != null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }

        // =========================
        // 📤 EXPORT TO EXCEL
        // =========================
        public IActionResult ExportToExcel()
        {
            var films = _context.Films.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Films");

                // Заголовки
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Title";
                worksheet.Cell(1, 3).Value = "Duration";
                worksheet.Cell(1, 4).Value = "ReleaseDate";

                int row = 2;

                foreach (var film in films)
                {
                    worksheet.Cell(row, 1).Value = film.Id;
                    worksheet.Cell(row, 2).Value = film.Title;
                    worksheet.Cell(row, 3).Value = film.Duration;
                    worksheet.Cell(row, 4).Value = film.Releasedate?.ToDateTime(TimeOnly.MinValue);
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Films.xlsx"
                    );
                }
            }
        }

        // ексель

        [HttpPost]
        public IActionResult ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1);

                    foreach (var row in rows)
                    {
                        var film = new Film
                        {
                            Title = row.Cell(2).GetString(),
                            Duration = row.Cell(3).GetValue<int>(),
                            Releasedate = DateOnly.FromDateTime(row.Cell(4).GetDateTime())
                        };

                        _context.Films.Add(film);
                    }

                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}