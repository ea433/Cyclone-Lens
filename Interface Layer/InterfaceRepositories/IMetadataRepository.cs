using CycloneLens.Models;
using Data_Access_Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IMetadataRepository
    {
        List<MetadataDTO> GetMetadata();
        void AddMetadata(MetadataDTO metadata);
    }
}
