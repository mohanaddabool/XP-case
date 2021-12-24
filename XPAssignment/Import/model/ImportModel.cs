namespace XPAssignment.Import.model
{
    public class ImportModel
    {
        public List<string>? Genres { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public DateTime Premiered { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
