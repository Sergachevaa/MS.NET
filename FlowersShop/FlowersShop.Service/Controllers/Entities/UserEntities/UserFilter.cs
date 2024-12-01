using FlowersShop.DataAccess.Entities;

namespace FlowersShop.Service.Controllers.Entities.UserEntities;

public class UserFilter
{
    public string? NamePart { get; set; } 
    public string? EmailPart { get; set; } 
    public DateTime? CreationTimeFrom { get; set; } 
    public DateTime? CreationTimeTo { get; set; } 
    public DateTime? ModificationTimeFrom { get; set; } 
    public DateTime? ModificationTimeTo { get; set; } 
    //public Role? Role { get; set; }
}