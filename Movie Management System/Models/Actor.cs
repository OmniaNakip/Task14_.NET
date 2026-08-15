namespace Movie_Management_System.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Image { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
