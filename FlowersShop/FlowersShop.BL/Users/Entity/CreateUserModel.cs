using FlowersShop.DataAccess.Entities;

namespace FlowersShop.BL.Users.Entity;

public class CreateUserModel
{
    public string PasswordHash { get; set; } 
    public string Email { get; set; } 
    public string Name { get; set; } 
    public string Surname { get; set; } 
    public string? Patronymic { get; set; } 
    //public Role Role { get; set; } 
    public int FlowersShopId { get; set; } 
    public int? DiscountId { get; set; } 
    public string PhoneNumber { get; set; } 
}