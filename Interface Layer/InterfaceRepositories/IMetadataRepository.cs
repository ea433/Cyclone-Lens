using CycloneLens.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IMetadataRepository
    {
        List<Metadata> GetMetadata();
        void AddMetadata(Metadata metadata);
    }
}
