using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeaNotes.Users.Models;

namespace TeaNotes.Notes.Models
{
    public class TeaNote
    {
        public int? Id { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateOnly TastingDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

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

        [Range(0, 10_000)]
        public int? PricePerGram { get; set; }

        [RegularExpression(@"^(spill|infuse|boil|other)$")]
        public string? BrewingMethod { get; set; }

        [RegularExpression(@"^(teapot|gaiwan|cup|thermos|other)$")]
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
        public int? InfusionBalance { get; set; }

        [Range(1, 5)]
        public int? InfusionBouquet { get; set; }

        [Range(1, 5)]
        public int? InfusionExtractivity { get; set; }

        [Range(1, 5)]
        public int? InfusionTartness { get; set; }

        [Range(1, 5)]
        public int? InfusionViscosity { get; set; }

        [Range(1, 5)]
        public int? InfusionDensity { get; set; }

        [StringLength(2_000)]
        public string AftertasteComment { get; set; } = string.Empty;

        [Range(1, 5)]
        public int? AftertasteDuration { get; set; }

        [Range(1, 5)]
        public int? AftertasteIntensity { get; set; }

        [StringLength(2_000)]
        public string ImpressionComment { get; set; } = string.Empty;

        [StringLength(2_000)]
        public string ImpressionWellCombinedWith { get; set; } = string.Empty;

        [Range(1, 5)]
        public int? ImpressionRate{ get; set; }

        public ICollection<TeaTaste> Tastes { get; set; } = null!;
     
        public User User { get; set; } = null!;
    }
}
