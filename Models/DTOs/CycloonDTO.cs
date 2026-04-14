using Logic.Enums;

namespace Models.DTOs
{
    public class CycloonDto
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public CategorieType Categorie { get; set; }
        public BassinType Bassin { get; set; }
        public StatusType Status { get; set; }

        public CycloonDto(int id, string naam, CategorieType categorie, BassinType bassin, StatusType status)
        {
            Id = id;
            Naam = naam;
            Categorie = categorie;
            Bassin = bassin;
            Status = status;
        }
    }
}
