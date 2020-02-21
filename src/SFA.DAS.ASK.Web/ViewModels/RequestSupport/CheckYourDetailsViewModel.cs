using System;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck;
using SFA.DAS.ASK.Application.Utils;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class CheckYourDetailsViewModel
    {
        public CheckYourDetailsViewModel(TempSupportRequest tempSupportRequest, string numberOfOrgs)
        {
            RequestId = tempSupportRequest.Id;
            Name = $"{tempSupportRequest.FirstName} {tempSupportRequest.LastName}";
            JobRole = tempSupportRequest.JobRole;
            PhoneNumber = tempSupportRequest.PhoneNumber;
            Email = tempSupportRequest.Email;
            OrganisationType = EnumHelper.GetEnumDescription(tempSupportRequest.OrganisationType);
            OrganisationName = tempSupportRequest.OrganisationName;
            BuildingAndStreet1 = tempSupportRequest.BuildingAndStreet1;
            BuildingAndStreet2 = tempSupportRequest.BuildingAndStreet2;
            TownOrCity = tempSupportRequest.TownOrCity;
            County = tempSupportRequest.County;
            Postcode = tempSupportRequest.Postcode;
            SupportRequestType = tempSupportRequest.SupportRequestType;
            if (numberOfOrgs != null)
            {
                NumberOfOrgs = (DfeOrganisationsStatus) int.Parse(numberOfOrgs);
            }
        }

        public Guid RequestId { get; set; }
        public string Name { get; set; }
        public string JobRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OrganisationType { get; set; }
        public string OrganisationName { get; set; }
        public string BuildingAndStreet1 { get; set; }
        public string BuildingAndStreet2 { get; set; }
        public string TownOrCity { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public SupportRequestType SupportRequestType { get; set; }
        public DfeOrganisationsStatus NumberOfOrgs { get; set; }
    }
}