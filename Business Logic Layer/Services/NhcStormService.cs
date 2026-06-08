using Business_Logic_Layer.Mappers;
using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;
using Models.Enums;

namespace Business_Logic_Layer.API_Services
{
    public class NhcStormService
    {
        private readonly INhcStormRepository _repository;

        public NhcStormService(INhcStormRepository repository)
        {
            _repository = repository;
        }

        public void SyncFromNhc(List<NhcStorm> storms)
        {
            foreach (NhcStorm storm in storms)
            {
                NhcStormDTO dto = NhcStormMapper.ToDTO(storm);
                _repository.Insert(dto);
            }
        }
    }
}