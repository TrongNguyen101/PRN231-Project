using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Account")]
    public class Account
    {
        [Column("Account ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AcountId { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Role ID")]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public Profile? Profile { get; set; }
    }
}
