using System;
using System.Collections.Generic;
using System.Text;
using Interface_Layer.InterfaceRepositories;

namespace CycloneLensTestProject.FakeRepositories
{
    public class FakeLoggingRepository : ILoggingRepository
    {
        public int? LoggedCycloonId { get; private set; }
        public string? LoggedBericht { get; private set; }
        public int? LoggedGebruikerId { get; private set; }

        public void LogWijziging(int cycloonId, string bericht, int gebruikerId)
        {
            LoggedCycloonId = cycloonId;
            LoggedBericht = bericht;
            LoggedGebruikerId = gebruikerId;
        }
    }
}
