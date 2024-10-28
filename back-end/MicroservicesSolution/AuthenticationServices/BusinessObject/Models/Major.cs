using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Major")]
    public class Major
    {
        [Column("Major ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MajorId { get; set; }
        [Column("Major Name")]
        public string? MajorName { get; set; }
        public Profile? Profile { get; set; }
    }
}
