using static System.String;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

[Table("group")]
[Serializable]
public class GroupEntity : BaseEntity
{
    [Column("group_name", TypeName = "varchar(200)")]
    public string GroupName { get; set; } = Empty;
    
    [Column("limit_member", TypeName = "integer")]
    public int? LimitMember { get; set; }
    
    [Column("note", TypeName = "varchar(200)")]
    public string? Note { get; set; }
}