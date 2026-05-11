using Microsoft.SqlServer.Types;

namespace Models.Classes
{
    public class ForecastPoint
    {
        public SqlGeography Coordinaten { get; set; } 

        public int VoorspellingUur { get; set; }

        public int Windsnelheid { get; set; }

        public ForecastPoint(SqlGeography coordinaten, int voorspellingUur, int windsnelheid)
        {
            this.Coordinaten = coordinaten;
            this.VoorspellingUur = voorspellingUur;
            this.Windsnelheid = windsnelheid;
        }
    }
}
