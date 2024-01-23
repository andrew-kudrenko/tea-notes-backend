using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TeaNotes.Notes.Models
{
    [Index(nameof(Title), IsUnique = true)]
    public class TeaNote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        
        [RegularExpression(@"^(green|white|yellow|oolong|red|black|shen-puer|shu-puer)$")]
        public string? Kind { get; set; }

        [StringLength(100)]
        public string Region { get; set; } = string.Empty;

        [StringLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        [Range(1900, 2024)]
        public int? ManufacturingYear { get; set; }

        [Range(0, double.MaxValue)]
        public double? PricePerGram { get; set; }

        [RegularExpression(@"^(spill|infuse|boil|other)$")]
        public string? BrewingMethod { get; set; }

        [RegularExpression(@"^(teapot|gaiwan|cup|other)$")]
        public string? BrewingDishware { get; set; }
        
        [Range(0, 10_000)]
        public int? BrewingVolume { get; set; }

        [Range(0, 100)]
        public int? BrewingTemperature { get; set; }

        [Range(1, 100)]
        public int? BrewingQuantity { get; set; }

        [StringLength(2_000)]
        public string DryLeafAppearance { get; set; } = string.Empty;

        [StringLength(2_000)]
        public string DryLeafAroma { get; set; } = string.Empty;

        [StringLength(2_000)]
        public string InfusionAppearance { get; set; } = string.Empty;

        [StringLength(2_000)]
        public string InfusionAroma { get; set; } = string.Empty;

        [StringLength(2_000)]
        public string InfusionTaste { get; set; } = string.Empty;

        [Range(1, 5)]
        public double? InfusionBalance { get; set; }

        [Range(1, 5)]
        public double? InfusionBouquet { get; set; }

        [Range(1, 5)]
        public double? InfusionExtractivity { get; set; }

        [Range(1, 5)]
        public double? InfusionTartness { get; set; }

        [Range(1, 5)]
        public double? InfusionViscosity { get; set; }

        [Range(1, 5)]
        public double? InfusionDensity { get; set; }

        [StringLength(2_000)]
        public string AftertasteComment { get; set; } = string.Empty;

        [Range(1, 5)]
        public double? AftertasteDuration { get; set; }

        [Range(1, 5)]
        public double? AftertasteIntensity { get; set; }

        [StringLength(2_000)]
        public string ImpressionComment { get; set; } = string.Empty;

        [StringLength(2_000)]
        public string ImpressionWellCombinedWith { get; set; } = string.Empty;

        [Range(1, 5)]
        public double? ImpressionRate{ get; set; }

        public ICollection<TeaTaste> Tastes { get; set; } = null!;
     
        public User.Models.User User { get; set; } = null!;
    }
}
