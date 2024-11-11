using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersShop.DataAccess.Entities;

[Table("Discounts")]

public class Discount : BaseEntity
{
    public string Name { get; set; }
    public int Percent { get; set; }
    
    public int UserId { get; set; }
    
    public UserEntity UserEntitys { get; set; }
   // [ForeignKey("UserId")]
    public virtual List<UserEntity> UserEntity { get; set; }
}