using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using System.IO;
using System.Linq;

namespace MovieApp.Controllers
{
    public class ExcelController : Controller
    {
        private readonly MovieDbContext _context;

        public ExcelController(MovieDbContext context)
        {
            _context = context;
        }

        public IActionResult ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var films = _context.Films.ToList();
                var wsFilms = workbook.Worksheets.Add("Films");

                wsFilms.Cell(1, 1).Value = "Id";
                wsFilms.Cell(1, 2).Value = "Title";
                wsFilms.Cell(1, 3).Value = "Duration";
                wsFilms.Cell(1, 4).Value = "ReleaseDate";

                int row = 2;
                foreach (var f in films)
                {
                    wsFilms.Cell(row, 1).Value = f.Id;
                    wsFilms.Cell(row, 2).Value = f.Title;
                    wsFilms.Cell(row, 3).Value = f.Duration;
                    wsFilms.Cell(row, 4).Value = f.Releasedate?.ToDateTime(TimeOnly.MinValue);
                    row++;
                }

                var genres = _context.Genres.ToList();
                var wsGenres = workbook.Worksheets.Add("Genres");

                wsGenres.Cell(1, 1).Value = "Id";
                wsGenres.Cell(1, 2).Value = "Name";

                row = 2;
                foreach (var g in genres)
                {
                    wsGenres.Cell(row, 1).Value = g.Id;
                    wsGenres.Cell(row, 2).Value = g.Name;
                    row++;
                }

                var users = _context.Users.ToList();
                var wsUsers = workbook.Worksheets.Add("Users");

                wsUsers.Cell(1, 1).Value = "Id";
                wsUsers.Cell(1, 2).Value = "Username";
                wsUsers.Cell(1, 3).Value = "Email";

                row = 2;
                foreach (var u in users)
                {
                    wsUsers.Cell(row, 1).Value = u.Id;
                    wsUsers.Cell(row, 2).Value = u.Username;
                    wsUsers.Cell(row, 3).Value = u.Email;
                    row++;
                }

                var comments = _context.Comments.ToList();
                var wsComments = workbook.Worksheets.Add("Comments");

                wsComments.Cell(1, 1).Value = "Id";
                wsComments.Cell(1, 2).Value = "Text";
                wsComments.Cell(1, 3).Value = "FilmId";
                wsComments.Cell(1, 4).Value = "UserId";

                row = 2;
                foreach (var c in comments)
                {
                    wsComments.Cell(row, 1).Value = c.Id;
                    wsComments.Cell(row, 2).Value = c.Text;
                    wsComments.Cell(row, 3).Value = c.Filmid;
                    wsComments.Cell(row, 4).Value = c.Userid;
                    row++;
                }

                var ratings = _context.Ratings.ToList();
                var wsRatings = workbook.Worksheets.Add("Ratings");

                wsRatings.Cell(1, 1).Value = "Id";
                wsRatings.Cell(1, 2).Value = "Value";
                wsRatings.Cell(1, 3).Value = "FilmId";
                wsRatings.Cell(1, 4).Value = "UserId";

                row = 2;
                foreach (var r in ratings)
                {
                    wsRatings.Cell(row, 1).Value = r.Id;
                    wsRatings.Cell(row, 2).Value = r.Score;
                    wsRatings.Cell(row, 3).Value = r.Filmid;
                    wsRatings.Cell(row, 4).Value = r.Userid;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "FullExport.xlsx"
                    );
                }
            }
        }
    }
}