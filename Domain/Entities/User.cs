using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public List<Product> Products { get; set; } = null!;
    public UserProfile UserProfile { get; set; } = null!;
}