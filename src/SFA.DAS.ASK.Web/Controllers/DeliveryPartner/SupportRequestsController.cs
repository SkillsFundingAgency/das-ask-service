using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.GetSupportRequests;

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

        [HttpGet("delivery-partner/support-requests/all")]
        public async Task<IActionResult> Index(SortBy sortBy)
        {
            return View("~/Views/DeliveryPartner/SupportRequests/SupportRequests.cshtml", await GetSupportRequests(sortBy));
        }

        [HttpGet("delivery-partner/support-requests/new")]
        public async Task<IActionResult> NewSupportRequests(SortBy sortBy)
        {
            return View("~/Views/DeliveryPartner/SupportRequests/NewSupportRequests.cshtml", await GetSupportRequests(sortBy));
        }
        
        [HttpGet("delivery-partner/support-requests/contacted")]
        public async Task<IActionResult> ContactedSupportRequests(SortBy sortBy)
        {   
            return View("~/Views/DeliveryPartner/SupportRequests/ContactedSupportRequests.cshtml", await GetSupportRequests(sortBy));
        }
        
        [HttpGet("delivery-partner/support-requests/contacted")]
        public async Task<IActionResult> RejectedSupportRequests(SortBy sortBy)
        {
            return View("~/Views/DeliveryPartner/SupportRequests/RejectedSupportRequests.cshtml", await GetSupportRequests(sortBy));
        }
        
        private async Task<SupportRequestsViewModel> GetSupportRequests(SortBy sortBy)
        {
            var supportRequests = await _mediator.Send(new GetSupportRequestsRequest());

            var supportRequestsViewModel = new SupportRequestsViewModel(supportRequests, sortBy);
            return supportRequestsViewModel;
        }
    }
}