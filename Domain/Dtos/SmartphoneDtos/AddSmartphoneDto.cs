using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.SmartphoneDtos;

public class AddSmartphoneDto : SmartphoneDto
{
    [Required]
    public int SubCategoryId { get; set; }
}