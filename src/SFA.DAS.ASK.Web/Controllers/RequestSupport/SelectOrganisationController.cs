using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetDfeOrganisations;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.Controllers.RequestSupport
{
    public class SelectOrganisationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SelectOrganisationController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet("select-organisation/{requestId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid requestId)
        {
            //var dfeSignInId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dfeSignInId = Guid.NewGuid();
            
            var dfeOrganisations = await _mediator.Send(new GetDfeOrganisationsRequest(dfeSignInId));
            
            var viewModel = new SelectOrganisationViewModel(dfeOrganisations, requestId);

            return View("~/Views/RequestSupport/SelectOrganisation.cshtml", viewModel);
        }
        
        [HttpPost("select-organisation/{requestId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid requestId, SelectOrganisationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new {requestId});
            }
            
            //var email = User.FindFirst(ClaimTypes.Email);
            var email = "davegouge@myschool.org.uk.edu.com";
            //var dfeSignInId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dfeSignInId = Guid.NewGuid();
            string name = "David Gouge";
            
            await _mediator.Send(new AddDfESignInInformationCommand(dfeSignInId, viewModel.SelectedUrn, email, name, requestId));
            
            return RedirectToAction("SignedIn", "OtherDetails", new {requestId = requestId});
        }
    }
}