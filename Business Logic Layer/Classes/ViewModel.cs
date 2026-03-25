using Logic.Enums;

namespace CycloneLens.Models
{
    public class ViewModel
    {
        public string Naam { get; set; }
        public CategorieType Categorie { get; set; }
        public BassinType Bassin { get; set; }
        public StatusType Status { get; set; }

        public ViewModel(string naam, CategorieType categorie, BassinType bassin, StatusType status)
        {
            this.Naam = naam;
            this.Categorie = categorie;
            this.Bassin = bassin;
            this.Status = status;
        }
    }
}
