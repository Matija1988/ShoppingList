using Domain.ShopListProducts;
using Domain.Users;

namespace Domain.ShopList;

[Table("shop_lists")]
public class ShopList
{
    [Column("lst_id")]
    public int Id { get; set; }

    [Column("lst_name")]
    public string Name { get; set; }

    [Column("lst_dateCreated")]
    public DateTime DateCreated { get; set; }

    [Column("lst_dateUpdated")]
    public DateTime DateUpdated { get; set; }

    [Column("lst_isActive")]
    public bool IsActive { get; set; }

    [Column("lst_userId")]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    [Column("lst_totalValue")]
    public decimal? TotalValue { get; set; }
    public List<ShopListProduct> ShopListProducts {get; set;}
}
