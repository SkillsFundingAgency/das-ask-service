using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.GetSupportRequests;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner
{
    [Authorize]
    public class SupportRequestsController : Controller
    {
        private readonly IMediator _mediator;

        public SupportRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("delivery-partner/support-requests/")]
        public async Task<IActionResult> Index(SortBy sortBy)
        {
            var supportRequests = await _mediator.Send(new GetSupportRequestsRequest());

            var supportRequestsViewModel = new SupportRequestsViewModel(supportRequests, sortBy);
            
            return View("~/Views/DeliveryPartner/SupportRequests.cshtml", supportRequestsViewModel);
        }
    }

    public class SupportRequestsViewModel
    {
        public SortBy SortBy { get; }

        public SupportRequestsViewModel(GetSupportRequestsResponse supportRequestResponse, SortBy sortBy)
        {
            SortBy = sortBy;
            NewSupportRequests = supportRequestResponse.NewSupportRequests.Select(sr => new SupportRequestViewModel(sr)).ToList();
            ContactedSupportRequests = supportRequestResponse.ContactedSupportRequests.Select(sr => new SupportRequestViewModel(sr)).ToList();

            NewSupportRequests = Sort(NewSupportRequests);
            ContactedSupportRequests = Sort(ContactedSupportRequests);
        }

        private List<SupportRequestViewModel> Sort(List<SupportRequestViewModel> supportRequestViewModels)
        {
            var sorted = supportRequestViewModels;
            if (SortBy == SortBy.RecentlyPublished)
            {
                 sorted = supportRequestViewModels.OrderBy(sr => sr.StatusDate).ToList();   
            }

            return sorted;
        }

        public List<SupportRequestViewModel> NewSupportRequests { get; set; }
        public List<SupportRequestViewModel> ContactedSupportRequests { get; set; }
    }

    public class SupportRequestViewModel
    {
        public SupportRequestViewModel(SupportRequest sr)
        {
            OrganisationName = sr.Organisation.OrganisationName;
            OrganisationAddress = sr.Organisation.BuildingAndStreet1; // TODO: parse full address
            ContactName = sr.OrganisationContact.FirstName + " " + sr.OrganisationContact.LastName;
            ContactTelephone = sr.OrganisationContact.PhoneNumber;
            ContactEmail = sr.OrganisationContact.Email;

            if (sr.CurrentStatus == Status.NewRequest)
            {
                StatusDate = sr.EventLogs.Single(el => el.Status == Status.NewRequest).EventDate;    
            }
            else
            {
                StatusDate = sr.EventLogs.SingleOrDefault(el => el.Status == Status.Contacted)?.EventDate;    
            }
        }

        public string OrganisationName { get; set; }
        public string OrganisationAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactTelephone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? StatusDate { get; set; }
    }
}