namespace Movie_Management_System.Models
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? Image { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
