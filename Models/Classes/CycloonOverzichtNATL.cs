using Logic.Enums;

namespace Models.Classes
{
    public class CycloonOverzichtNATL
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public CategorieType Categorie { get; set; }
        public BassinType Bassin { get; set; }
        public StatusType Status { get; set; }


        public CycloonOverzichtNATL(int id, string naam, CategorieType categorie, BassinType bassin, StatusType status)
        {
            this.Id = id;
            this.Naam = naam;
            this.Categorie = categorie;
            this.Bassin = bassin;
            this.Status = status;
        }
    }
}
