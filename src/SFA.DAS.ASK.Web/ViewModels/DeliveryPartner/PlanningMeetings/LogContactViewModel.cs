using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    [AtLeastOneProperty("Email","Telephone",ErrorMessage = "Select how you made contact")]
    public class LogContactViewModel
    {
        public Guid RequestId { get; set; }
        public String EstablishmentName { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
       
        public bool SelectedContactMethod { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool SchedulePlanningMeeting { get; set; }
        public Status Status { get; set; }
        public DateTime ContactedDate { get; set; }

        public LogContactViewModel()
        {

        }
        public LogContactViewModel(SupportRequest supportRequest)
        {
            RequestId = supportRequest.Id;
            Status = supportRequest.CurrentStatus;
            EstablishmentName = supportRequest.Organisation.OrganisationName;

        }

        public SupportRequest UpdateSupportRequest(SupportRequest supportRequest)
        {
            supportRequest.CurrentStatus = Status.ContactConfirmed;
            supportRequest.ContactedDate = new DateTime(Year, Month, Day);

            return supportRequest;
        }

        

        
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AtLeastOnePropertyAttribute : ValidationAttribute
    {
        private string[] PropertyList { get; set; }

        public AtLeastOnePropertyAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        public override bool IsValid(object value)
        {
            PropertyInfo propertyInfo;
            foreach (string propertyName in PropertyList)
            {
                propertyInfo = value.GetType().GetProperty(propertyName);

                if (propertyInfo.GetValue(value).Equals(true))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
