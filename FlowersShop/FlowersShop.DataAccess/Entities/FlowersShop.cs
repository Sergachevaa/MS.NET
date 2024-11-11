using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersShop.DataAccess.Entities
{
    [Table("FlowersShop")]
    public class FlowersShop : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; } 
        public string Email { get; set; }
        
        public virtual List<Item> Items { get; set; } // Изменено на множественное число
    }
}