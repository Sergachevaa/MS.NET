using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersShop.DataAccess.Entities;

[Table("Items")]
public class Item : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool Availability { get; set; }
    public int Quantity { get; set; }
    
    public int FlowerId { get; set; }
   // [ForeignKey("FlowerId")]
    public FlowersShop Flower { get; set; }
    public List<ItemCategory> ItemsCategories { get; set; }
    

}