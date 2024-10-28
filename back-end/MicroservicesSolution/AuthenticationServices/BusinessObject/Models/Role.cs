using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Role")]
    public class Role
    {
        [Column("Role ID")]
        public int RoleId { get; set; }
        [Column("Role Name")]
        public string? RoleName { get; set; }
        public ICollection<Account> Accounts { get; } = new List<Account>();
    }
}
