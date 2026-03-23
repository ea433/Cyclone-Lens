using Microsoft.Extensions.Localization;
using System.Runtime.InteropServices.Swift;

namespace CycloneLens.Models
{
    public class ViewModel
    {
        public required string TitleText { get; set; }
        public required string ImageUrl { get; set; }
        public required string SourceName { get; set; }
        public required string StormName { get; set; }
        public required string DateText { get; set; }
        public required string RegionName { get; set; }

        public ViewModel(string titleText, string imageURL, string sourceName, string stormName, string dateText, string regionName)
        {
            this.TitleText = titleText;
            this.ImageUrl = imageURL;
            this.SourceName = sourceName;
            this.StormName = stormName;
            this.DateText = dateText;
            this.RegionName = regionName;
        }
    }
}
