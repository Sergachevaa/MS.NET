using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FlowersShop.DataAccess.Entities;

public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public string Surname { get; set; } 
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
    public int FlowersShopId { get; set; }
    public FlowersShop FlowersShop { get; set; }
    public int DiscountId { get; set; }
    public Discount Discounts { get; set; }
    
    public virtual List<Discount> Discount { get; set; }
    public class UserRole : IdentityRole<int>
    {
    }
}