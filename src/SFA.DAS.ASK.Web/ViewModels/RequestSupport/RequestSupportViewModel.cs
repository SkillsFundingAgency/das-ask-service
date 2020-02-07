using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class RequestSupportViewModel
    {
        [Required]
        public bool? HasSignInAccount { get; set; }
    }
}