using Logic.Enums;

namespace CycloneLens.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public CategorieType Categorie { get; set; }
        public BassinType Bassin { get; set; }
        public StatusType Status { get; set; }

        public ViewModel(int id, string naam, CategorieType categorie, BassinType bassin, StatusType status)
        {
            this.Id = id;
            this.Naam = naam;
            this.Categorie = categorie;
            this.Bassin = bassin;
            this.Status = status;
        }
    }
}
