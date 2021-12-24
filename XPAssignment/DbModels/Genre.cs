using System.ComponentModel.DataAnnotations.Schema;

namespace XPAssignment.DbModels
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey("showId")]
        public int ShowId { get; set; }
        public virtual Show Show { get; set; }

    }
}
