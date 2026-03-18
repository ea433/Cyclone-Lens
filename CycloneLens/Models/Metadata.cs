namespace CycloneLens.Models
{
    public class Metadata
    {
        public int Id { get; }
        public float Windsnelheid { get; private set; }
        public float Luchtdruk { get; private set; } 
    
    public Metadata(int id, float windsnelheid, float luchtdruk)
        {
            this.Id = id;
            this.Windsnelheid = windsnelheid;
            this.Luchtdruk = luchtdruk;
        }
    }
}
