namespace Data_Access_Layer.DTOs
{
    public class CycloonDTO
    {
        public int Id { get; set; }
        public required string Naam { get; set; }
        public required string Status { get; set; }
        public required string Bassin { get; set; }
    }
}
