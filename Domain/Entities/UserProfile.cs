using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class UserProfile
{
    [Key] public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    [MaxLength(50)] public string FirstName { get; set; } = null!;
    [MaxLength(50)] public string LastName { get; set; } = null!;
    [MaxLength(50)] public string Email { get; set; } = null!;

    [MaxLength(50)] public string PhoneNumber { get; set; } = null!;
    public string Image { get; set; } = null!;
    public DateOnly Dob { get; set; }
    public List<IdentityRole> IdentityRoles { get; set; } = null!;
}