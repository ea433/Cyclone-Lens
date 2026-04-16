using CycloneLens.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.InterfaceRepositories
{
    public interface ICycloonDataRepository
    {
        List<CycloonData> GetMetadata();
        void AddMetadata(CycloonData metadata);
    }
}
