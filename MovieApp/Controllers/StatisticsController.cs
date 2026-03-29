using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;

public class StatisticsController : Controller
{
    private readonly MovieDbContext _context;

    public StatisticsController(MovieDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Фільми :)
        var genreData = _context.Filmgenres
            .GroupBy(fg => fg.Genreid)
            .Select(g => new
            {
                Genre = _context.Genres
                    .Where(x => x.Id == g.Key)
                    .Select(x => x.Name)
                    .FirstOrDefault(),
                Count = g.Count()
            })
            .ToList();

        ViewBag.GenreLabels = genreData.Select(x => x.Genre).ToList();
        ViewBag.GenreCounts = genreData.Select(x => x.Count).ToList();

        // Рейтинг :)
        var ratingData = _context.Ratings
            .GroupBy(r => r.Filmid)
            .Select(g => new
            {
                Film = _context.Films
                    .Where(f => f.Id == g.Key)
                    .Select(f => f.Title)
                    .FirstOrDefault(),
                AvgRating = g.Average(x => x.Score)
            })
            .ToList();

        ViewBag.FilmLabels = ratingData.Select(x => x.Film).ToList();
        ViewBag.FilmRatings = ratingData.Select(x => x.AvgRating).ToList();

        return View();
    }
}