using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersShop.DataAccess.Entities;

[Table("ItemCategory")]

public class ItemCategory:BaseEntity
{
    public string Name { get; set; }
    
    public List<Item> Items { get; set; }
} 