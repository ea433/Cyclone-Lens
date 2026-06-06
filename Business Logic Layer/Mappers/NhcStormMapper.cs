using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Mappers
{
    public class NhcStormMapper
    {
        public static string ParseBassin(string nhcId)
        {
            if (nhcId.StartsWith("al"))
                return "Noord-Atlantisch";
            else if (nhcId.StartsWith("ep"))
                return "Oostelijk-Pacifisch";
            else
                return "Centraal-Pacifisch";
        }
    }
}
