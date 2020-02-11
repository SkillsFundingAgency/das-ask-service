using System;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OrganisationAddressViewModel
    {
        public OrganisationAddressViewModel() { }
        
        public OrganisationAddressViewModel(SupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            BuildingAndStreet1 = supportRequest.Organisation.BuildingAndStreet1;
            BuildingAndStreet2 = supportRequest.Organisation.BuildingAndStreet2;
            TownOrCity = supportRequest.Organisation.TownOrCity;
            County = supportRequest.Organisation.County;
            Postcode = supportRequest.Organisation.Postcode;
        }

        [Required]
        public string BuildingAndStreet1 { get; set; }
        [Required]
        public string BuildingAndStreet2 { get; set; }
        [Required]
        public string TownOrCity { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        public string Postcode { get; set; }

        public Guid RequestId { get; set; }

        public SupportRequest ToSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.Organisation.BuildingAndStreet1 = BuildingAndStreet1;
            supportRequest.Organisation.BuildingAndStreet2 = BuildingAndStreet2;
            supportRequest.Organisation.TownOrCity = TownOrCity;
            supportRequest.Organisation.County = County;
            supportRequest.Organisation.Postcode = Postcode;

            return supportRequest;
        }
    }
}