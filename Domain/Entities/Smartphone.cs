using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Smartphone
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    [MaxLength(100)]
    public string Model { get; set; } = null!;
    [MaxLength(50)]
    public string Os { get; set; } = null!;
    [MaxLength(50)]
    public string? SimCard { get; set; }
    public string? Communication { get; set; }
    [MaxLength(50)]
    public string? DisplayResolution { get; set; }
    [MaxLength(50)]
    public string? PixelPerInch { get; set; }
    [MaxLength(50)]
    public string? AspectRatio { get; set; }
    [MaxLength(50)]
    public string? ScreenRefreshRate { get; set; }
    [MaxLength(50)]
    public string? DisplayType { get; set; }
    [MaxLength(50)]
    public string? Diagonal { get; set; }
    [MaxLength(50)]
    public string? Processor { get; set; }
    [MaxLength(50)]
    public string? VideoProcessor { get; set; }
    [MaxLength(50)]
    public string? ProcessorFrequency { get; set; }
    [MaxLength(50)]
    public string? NumberOfCores { get; set; }
    [MaxLength(50)]
    public string? Ram { get; set; }
    [MaxLength(50)]
    public string? Rom { get; set; }
}