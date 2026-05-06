using System;
using System.Collections.Generic;
using System.Text;
using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;

namespace CycloneLensTestProject.FakeRepositories
{
    public class FakeMetadataRepository : IMetadataRepository
    {
        public List<MetadataDTO> Metadata { get; set; } = new();
        public MetadataDTO? AddedMetadata { get; private set; }

        public List<MetadataDTO> GetMetadata()
        {
            return Metadata;
        }

        public void AddMetadata(MetadataDTO metadata)
        {
            AddedMetadata = metadata;
        }
    }
}
