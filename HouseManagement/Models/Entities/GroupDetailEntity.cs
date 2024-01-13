using static System.String;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

[Table("group_detail")]
[Serializable]
public class GroupDetailEntity : BaseEntity
{
    [Column("email", TypeName = "varchar(100)")]
    public string Email { get; set; } = Empty;
    
    [Column("group_id", TypeName = "integer")]
    public long GroupId { get; set; }
}