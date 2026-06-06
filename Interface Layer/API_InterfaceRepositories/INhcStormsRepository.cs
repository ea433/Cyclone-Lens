using Interface_Layer.API_DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.API_InterfaceRepositories
{
    public interface INhcStormRepository
    {
        void Insert(NhcStormDTO storm);
        List<NhcStormDTO> GetAll();
    }
}
