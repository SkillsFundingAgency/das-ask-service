using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ASK.Data.Entities
{
    public class PostcodeRegion
    {
        [Key]
        public string PostcodePrefix { get; set; }
        public string Region { get; set; }
        public int DeliveryAreaId { get; set; }
    }
}