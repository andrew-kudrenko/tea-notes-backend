using Newtonsoft.Json;
using TeaNotes.Notes.Models;

namespace TeaNotes.Notes.Controllers.Dto
{
    public record TeaNoteDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }
        public required TeaNoteGeneralInfo General { get; set; }
        public required TeaNoteBrewing Brewing { get; set; }
        public required TeaNoteDryLeaf DryLeaf { get; set; }
        public required TeaNoteInfusion Infusion { get; set; }
        public required TeaNoteAftertaste Aftertaste { get; set; }
        public required TeaNoteImpression Impression { get; set; }

        public TeaNote ToTeaNote()
        {
            return new()
            {
                Title = General.Title,
                Kind = General.Kind,
                Region = General.Region,
                Manufacturer = General.Manufacturer,
                ManufacturingYear = General.ManufacturingYear,
                PricePerGram = General.PricePerGram,

                BrewingDishware = Brewing.Dishware,
                BrewingMethod = Brewing.Method,
                BrewingQuantity = Brewing.Quantity,
                BrewingTemperature = Brewing.Temperature,
                BrewingVolume = Brewing.Volume,

                DryLeafAppearance = DryLeaf.Appearance,
                DryLeafAroma = DryLeaf.Aroma,

                InfusionAppearance = Infusion.Appearance,
                InfusionAroma = Infusion.Aroma,
                InfusionBalance = Infusion.Balance,
                InfusionBouquet = Infusion.Bouquet,
                InfusionDensity = Infusion.Density,
                InfusionExtractivity = Infusion.Extractivity,
                InfusionTartness = Infusion.Tartness,
                InfusionTaste = Infusion.Taste,
                InfusionViscosity = Infusion.Viscosity,

                AftertasteComment = Aftertaste.Comment,
                AftertasteDuration = Aftertaste.Duration,
                AftertasteIntensity = Aftertaste.Intensity,

                ImpressionComment = Impression.Comment,
                ImpressionRate = Impression.Rate,
                ImpressionWellCombinedWith = Impression.WellCombinedWith,
            };
        }

        public static TeaNoteDto FromTeaNote(TeaNote note)
        {
            return new() 
            { 
                Id = note.Id,
                General = new()
                {
                    Title = note.Title,
                    Kind = note.Kind,
                    Manufacturer = note.Manufacturer,
                    PricePerGram = note.PricePerGram,
                    ManufacturingYear = note.ManufacturingYear,
                    Region = note.Region,
                },
                Brewing = new()
                {
                    Dishware = note.BrewingDishware,
                    Method = note.BrewingMethod,
                    Quantity = note.BrewingQuantity,
                    Temperature = note.BrewingTemperature,
                    Volume = note.BrewingVolume,
                },
                DryLeaf = new() 
                {
                    Appearance = note.DryLeafAppearance,
                    Aroma = note.DryLeafAroma,
                },
                Infusion = new() 
                {
                    Appearance = note.InfusionAppearance,
                    Aroma = note.InfusionAroma,
                    Balance = note.InfusionBalance,
                    Bouquet = note.InfusionBouquet,
                    Density = note.InfusionDensity,
                    Extractivity = note.InfusionExtractivity,
                    Tartness = note.InfusionTartness,
                    Taste = note.InfusionTaste,
                    Tastes = note.Tastes.Select(t => t.Kind).ToList(),
                    Viscosity = note.InfusionViscosity,
                },
                Aftertaste = new()
                { 
                    Comment = note.AftertasteComment,
                    Duration = note.AftertasteDuration,
                    Intensity = note.AftertasteIntensity,
                },
                Impression = new()
                {
                    Comment = note.ImpressionComment,
                    Rate = note.ImpressionRate,
                    WellCombinedWith = note.ImpressionWellCombinedWith,
                },
            };
        }
    }
}
