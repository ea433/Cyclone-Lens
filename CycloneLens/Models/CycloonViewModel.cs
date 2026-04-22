using Logic.Enums;

namespace Presentation.Models
{
    public class CycloonViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public CategorieType Categorie { get; set; }
        public BassinType Bassin { get; set; }
        public StatusType Status { get; set; }

        public CycloonViewModel(int id, string naam, CategorieType categorie, BassinType bassin, StatusType status)
        {
            this.Id = id;
            this.Naam = naam;
            this.Categorie = categorie;
            this.Bassin = bassin;
            this.Status = status;
        }
    }
}
