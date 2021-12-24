namespace XPAssignment.ViewModel
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Language { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public DateTime Premiered { get; set; }
        public List<string>? Genres { get; set; }
    }
}
