using Logic.Enums;

namespace Models.Classes
{
    public class CycloonOverzicht
    {
        public int Id { get; }
        public string Naam { get; private set; }
        public CategorieType Categorie { get; private set; }
        public BassinType Bassin { get; private set; }
        public StatusType Status { get; private set; }

        public CycloonOverzicht(int id, string naam, CategorieType categorie, BassinType bassin, StatusType status)
        {
            this.Id = id;
            this.Naam = naam;
            this.Categorie = categorie;
            this.Bassin = bassin;
            this.Status = status;
        }
    }
}
