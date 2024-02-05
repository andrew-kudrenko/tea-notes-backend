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

        public static TeaNoteDto FromTeaNote(TeaNote note)
        {
            return new() 
            { 
                Id = note.Id,
                General = new()
                {
                    TastingDate = note.TastingDate,
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
