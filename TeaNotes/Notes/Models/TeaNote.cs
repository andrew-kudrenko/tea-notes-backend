using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeaNotes.Common;
using TeaNotes.Users.Models;

namespace TeaNotes.Notes.Models
{
    public class TeaNote
    {
        public int? Id { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateOnly? TastingDate { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = ModelErrorMessages.StringLength)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public string Title { get; set; } = string.Empty;
        
        [RegularExpression(@"^(green|white|yellow|oolong|red|black|shen-puer|shu-puer)$", ErrorMessage = ModelErrorMessages.RegEx)]
        public string? Kind { get; set; }

        [StringLength(100, ErrorMessage = ModelErrorMessages.StringLength)]
        public string Region { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = ModelErrorMessages.StringLength)]
        public string Manufacturer { get; set; } = string.Empty;

        [Range(1970, 2024, ErrorMessage = ModelErrorMessages.Range)]
        public int? ManufacturingYear { get; set; }

        [Range(0, 1_000, ErrorMessage = ModelErrorMessages.Range)]
        public int? PricePerGram { get; set; }

        [RegularExpression(@"^(spill|infuse|boil|other)$", ErrorMessage = ModelErrorMessages.RegEx)]
        public string? BrewingMethod { get; set; }

        [RegularExpression(@"^(teapot|gaiwan|cup|thermos|other)$", ErrorMessage = ModelErrorMessages.RegEx)]
        public string? BrewingDishware { get; set; }
        
        [Range(1, 10_000, ErrorMessage = ModelErrorMessages.Range)]
        public int? BrewingVolume { get; set; }

        [Range(0, 100, ErrorMessage = ModelErrorMessages.Range)]
        public int? BrewingTemperature { get; set; }

        [Range(1, 100, ErrorMessage = ModelErrorMessages.Range)]
        public int? BrewingQuantity { get; set; }

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string DryLeafAppearance { get; set; } = string.Empty;

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string DryLeafAroma { get; set; } = string.Empty;

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string InfusionAppearance { get; set; } = string.Empty;

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string InfusionAroma { get; set; } = string.Empty;

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string InfusionTaste { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? InfusionBalance { get; set; }

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? InfusionBouquet { get; set; }

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? InfusionExtractivity { get; set; }

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? InfusionTartness { get; set; }

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? InfusionViscosity { get; set; }

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? InfusionDensity { get; set; }

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string AftertasteComment { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? AftertasteDuration { get; set; }

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? AftertasteIntensity { get; set; }

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string ImpressionComment { get; set; } = string.Empty;

        [StringLength(2_000, ErrorMessage = ModelErrorMessages.StringLength)]
        public string ImpressionWellCombinedWith { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = ModelErrorMessages.Range)]
        public int? ImpressionRate{ get; set; }

        public ICollection<TeaTaste> Tastes { get; set; } = null!;
     
        public User User { get; set; } = null!;
    }
}
