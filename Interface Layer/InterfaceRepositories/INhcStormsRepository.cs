using Interface_Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.InterfaceRepositories
{
    public interface INhcStormRepository
    {
        void Insert(NhcStormDTO storm);
        List<NhcStormDTO> GetAll();
    }
}
