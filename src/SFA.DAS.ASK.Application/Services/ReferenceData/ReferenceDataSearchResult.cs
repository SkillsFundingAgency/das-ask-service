namespace SFA.DAS.ASK.Application.Services.ReferenceData
{
    public class ReferenceDataSearchResult
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public string Code { get; set; }
        public ReferenceDataAddress Address { get; set; }
        public string Sector { get; set; }
    }
}