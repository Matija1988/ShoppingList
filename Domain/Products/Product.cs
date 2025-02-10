namespace Domain.Products;

[Table("shop_products")]
public sealed class Product : Entity
{
    [Column("prod_id")]
    public int Id { get; set; }

    [Column("prod_unitPrice")]
    public decimal UnitPrice { get; set; }

    [Column("prod_name")]
    public string Name { get; set; }

    [Column("prod_dateCreated")]
    public DateTime DateCreated { get; set; }

    [Column("prod_dateUpdated")]
    public DateTime DateUpdated { get; set; }

    [Column("prod_isActive")]
    public bool IsActive { get; set; }
}
