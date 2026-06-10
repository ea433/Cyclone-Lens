/*
using Interface_Layer.DTOs; // Add this if not already present
using Interface_Layer.InterfaceRepositories;
using Models.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class StormApiService
    {

        private readonly IStormApi _repository;

        public StormApiService(IStormApi repository)
        {
            _repository = repository;
        
        }

        public async Task<List<Cycloon>> GetStorm()
        {
            var cycloonDtos = await _repository.GetActiveCyclonen();
            var cyclonen = new List<Cycloon>();
            foreach (var dto in cycloonDtos)
            {
                cyclonen.Add(new Cycloon
                {
                    // Map properties from CycloonDTO to Cycloon here
                    // Example:
                    // Id = dto.Id,
                    // Name = dto.Name,
                    // etc.
                });
            }
            return cyclonen;
        }
    }
}
*/