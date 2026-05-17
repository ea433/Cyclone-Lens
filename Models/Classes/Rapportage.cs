namespace Models.Classes
{
    public class Rapportage
    {
        public int Id { get; private set; }
        public DateTime Tijdstip { get; private set; }
        public Gebruiker Gebruiker { get; private set; }
        public Observatie Observatie { get; private set; }

        public Rapportage(int Id, DateTime Tijdstip, Gebruiker Gebruiker, Observatie Observatie)
        {
            this.Id = Id;
            this.Tijdstip = Tijdstip;
            this.Gebruiker = Gebruiker;
            this.Observatie = Observatie;
        }
    }
}
