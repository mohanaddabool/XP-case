namespace XPAssignment.ViewModel
{
    public class ShowFromApi
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Language { get; set; }
        public List<string>? Genres { get; set; }
        public string? Status { get; set; }
        public DateTime? Premiered { get; set; }
        public Rating? Rating { get; set; }
    }

    public class Rating
    {
        public decimal? Average { get; set; }
    }
}
