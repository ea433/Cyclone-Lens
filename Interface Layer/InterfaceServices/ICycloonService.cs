using CycloneLens.Models;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.InterfaceServices
{
    public interface ICycloonService
    {
        List<CycloonOverzichtNATL> GetActiveCyclonenNATL();
        void UpdateCycloon(Cycloon cycloon, CycloonData? metadata, Gebruiker gebruiker);
        Cycloon? GetById(int id);
    }
}
