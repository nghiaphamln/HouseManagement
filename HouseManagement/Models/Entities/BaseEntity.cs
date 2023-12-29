using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

[Serializable]
public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")] 
    public long Id { get; set; }

    [Column("created_user", TypeName = "varchar(100)")]
    public string CreatedUser { get; set; } = "system";

    [Column("created_date", TypeName = "timestamp")]
    public DateTime CreatedDate { get; set; } = DateTime.Now.ToLocalTime();

    [Column("updated_user", TypeName = "varchar(100)")]
    public string? UpdatedUser { get; set; }

    [Column("last_updated", TypeName = "timestamp")]
    public DateTime? LastUpdated { get; set; }

    [Column("is_deleted", TypeName = "boolean")]
    public bool IsDeleted { get; set; }
}