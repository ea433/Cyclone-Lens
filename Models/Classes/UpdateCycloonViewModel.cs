using CycloneLens.Models;
using Logic.Enums;

namespace Models.Classes
{
    public class UpdateCycloonViewModel
    {
        public int Id { get; set; }
        public string? Naam { get; set; }
        public StatusType Status { get; set; }
        public BassinType Bassin { get; set; }
        public CategorieType Categorie { get; set; }
        public double Windsnelheid { get; set; }
        public double Luchtdruk { get; set; }
        public double Coordinaten { get; set; }
    }
}
