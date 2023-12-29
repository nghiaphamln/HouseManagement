using static System.String;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

[Table("user")]
[Serializable]
public class UserEntity : BaseEntity
{
    [Column("email", TypeName = "varchar(100)")]
    public string Email { get; set; } = Empty;

    [Column("password", TypeName = "varchar(200)")]
    public string Password { get; set; } = Empty;

    [Column("full_name", TypeName = "varchar(200)")]
    public string FullName { get; set; } = Empty;

    [Column("phone", TypeName = "varchar(20)")]
    public string Phone { get; set; } = Empty;

    [Column("avatar", TypeName = "varchar(200)")]
    public string Avatar { get; set; } = "/images/no-avatar.png";

    [Column("role", TypeName = "varchar(20)")]
    public string Role { get; set; } = "user";

    [Column("date_of_birth", TypeName = "timestamp")]
    public DateTime? DateOfBirth { get; set; }
}