using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.API_Mappers
{
    public class NhcStormMapper
    {
        public static BassinType ParseBassin(string nhcId)
        {
            if (nhcId.StartsWith("al"))
                return BassinType.Noord_Atlantisch;
            else if (nhcId.StartsWith("cp"))
                return BassinType.Centraal_Pacifisch;
            else
                return BassinType.Oost_Pacifisch;
        }
    }
}
