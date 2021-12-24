using System.ComponentModel.DataAnnotations.Schema;

namespace XPAssignment.DbModels
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Language { get; set; } = null!;
        [Column(TypeName = "Date")]
        public DateTime Premiered { get; set; }
        public string Summary { get; set; } = null!;

        public virtual ICollection<Genre>? Genres { get; set; }
    }
}
