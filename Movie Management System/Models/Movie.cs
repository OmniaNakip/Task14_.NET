namespace Movie_Management_System.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? Status { get; set; }

        public DateTime DateTime { get; set; }

        public string? MainImg { get; set; }

        public List<string>? SubImages { get; set; }

        public List<Actor>? Actors { get; set; }

        public int CategoryId { get; set; }

        public int CinemaId { get; set; }

        public Category? Category { get; set; }

        public Cinema? Cinema { get; set; }
    }
}