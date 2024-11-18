using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersShop.DataAccess.Entities;

public class UserEntity : BaseEntity
{
    public string Surname { get; set; } 
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    
    public int FlowersShopId { get; set; }
    public FlowersShop FlowersShop { get; set; }
    public int DiscountId { get; set; }
    public Discount Discounts { get; set; }
    
    public virtual List<Discount> Discount { get; set; }
}