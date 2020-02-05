using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class OrganisationAddressController : Controller
    {
        [HttpGet("organisation-address")]
        public IActionResult Index()
        {
            var vm = new OrganisationAddressViewModel();

            return View("~/Views/RequestSupport/OrganisationAddress.cshtml", vm);
        }

        [HttpPost("organisation-address")]
        public IActionResult Index(OrganisationAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/RequestSupport/OrganisationAddress.cshtml", viewModel);    
            }

            return RedirectToAction("Index", "OtherDetails");
        }
    }

    public class OrganisationAddressViewModel
    {
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
    }
}