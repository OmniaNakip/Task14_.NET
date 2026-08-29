using Movie_Management_System.Models;

namespace Movie_Management_System.ViewModels
{
    public class DashboardViewModel
    {
        public int MoviesCount { get; set; }

        public int ActorsCount { get; set; }

        public int CinemasCount { get; set; }

        public int CategoriesCount { get; set; }

        public List<Category> Categories { get; set; } = new();
        public List<Cinema> Cinemas { get; set; } = new();
    }
}