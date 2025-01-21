namespace Domain.Users;

[Table("shop_users")]
public sealed class User : Entity
{
    [Column("usr_id"), Key]
    public Guid Id { get; set; }

    [Column("usr_username")]
    public required string Username { get; set; }

    [Column("usr_password")]
    public required string Password { get; set; }

    [Column("usr_email")]
    public required string Email { get; set; }

    [Column("usr_dateCreated")]
    public DateTime DateCreated { get; set; }

    [Column("usr_dateUpdated")]
    public DateTime DateUpdated { get; set; }

    [Column("usr_isActive")]
    public bool IsActive { get; set; }

}
