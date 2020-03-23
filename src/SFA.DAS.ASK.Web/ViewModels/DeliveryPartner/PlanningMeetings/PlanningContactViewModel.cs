﻿using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class PlanningContactViewModel
    {
        public Guid SelectedContact { get; set; }

        public string OrganisationName { get; set; }
        public Guid OrganisationId { get; set; }
        public List<OrganisationContact> Contacts { get; set; }

        public string NewFirstName{ get; set; }
        public string NewLastName { get; set; }
        public string NewPhoneNumber { get; set; }
        public string NewEmail { get; set; }

        public bool Edit { get; set; }
        public Guid SupportId { get; set; }

        public PlanningContactViewModel()
        {
               
        }
        public PlanningContactViewModel(SupportRequest supportRequest, PlanningMeeting planningMeeting, List<OrganisationContact> contacts, bool edit)
        {
            OrganisationName = supportRequest.Organisation.OrganisationName;
            OrganisationId = supportRequest.OrganisationId;

            Contacts = contacts;
            SelectedContact = planningMeeting.OrganisationContactId.GetValueOrDefault();

            SupportId = supportRequest.Id;
            Edit = edit;
        }

        public PlanningMeeting UpdatePlanningMeeting(PlanningMeeting planningMeeting)
        {
            planningMeeting.OrganisationContactId = SelectedContact;

            return planningMeeting;
        }

        public ModelStateDictionary ValidateNewContact(PlanningContactViewModel vm, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(vm.NewFirstName))
            {
                modelState.AddModelError("NewFirstName", "Please enter a first name");
            }
            if (string.IsNullOrWhiteSpace(vm.NewLastName))
            {
                modelState.AddModelError("NewLastName", "Please enter a last name");
            }
            if (string.IsNullOrWhiteSpace(vm.NewPhoneNumber))
            {
                modelState.AddModelError("NewPhoneNumber", "Please enter a phone number");
            }
            if (string.IsNullOrWhiteSpace(vm.NewEmail))
            {
                modelState.AddModelError("NewEmail", "Please enter an email");
            }

            return modelState;
        }
    }

}
