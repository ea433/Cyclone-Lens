using Interface_Layer.DTOs;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IMetadataRepository
    {
        List<MetadataDTO> GetMetadata();
        void AddMetadata(MetadataDTO metadata);
    }
}
