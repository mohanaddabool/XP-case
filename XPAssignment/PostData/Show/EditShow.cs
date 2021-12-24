using System.ComponentModel.DataAnnotations;

namespace XPAssignment.PostData.Show
{
    public class EditShow
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Premiered is required")]
        [Range(typeof(DateTime), "01-01-2014", "31-12-2021", ErrorMessage = "Premiered should be between 2014 & 2021")]
        public DateTime Premiered { get; set; }
        public string Language { get; set; } = null!;
        public List<string>? Genres { get; set; }
    }
}
