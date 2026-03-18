namespace CycloneLens.Models
{
    public class Metadata
    {
        public int id { get; }
        public float windsnelheid { get; private set; }
        public float luchtdruk { get; private set; } 
    
    public Metadata()
        {
            this.id = id;
            this.windsnelheid = windsnelheid;
            this.luchtdruk = luchtdruk;
        }
    }
}
