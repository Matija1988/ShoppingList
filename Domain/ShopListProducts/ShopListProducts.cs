using Domain.Products;
using System.Text.Json.Serialization;

namespace Domain.ShopListProducts;

/// <summary>
/// Potrebno radi konfiguracije EF modela
/// </summary>

[Table("shop_listProducts")]
public class ShopListProduct
{
    [Key]
    [Column("ulp_id")]
    public int Id { get; set; }

    [Column("ulp_listId")]
    public int ShopListId { get; set; }

    [ForeignKey("ShopListId")]
    [JsonIgnore]
    public Domain.ShopList.ShopList ShopList { get; set; }

    [Column("ulp_productId")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    [Column("ulp_productQuantity")]
    public int ProductQuantity { get; set; } = 1;

    [Column("ulp_totalValue")]
    public decimal? TotalValue { get; set; }
}