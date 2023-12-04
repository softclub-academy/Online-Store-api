using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index("CategoryName", IsUnique = true)]
public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public List<SubCategory> SubCategories { get; set; } = null!;
}