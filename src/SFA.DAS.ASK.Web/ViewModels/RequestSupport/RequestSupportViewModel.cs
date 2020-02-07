using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class RequestSupportViewModel
    {
        [Required(ErrorMessage = "Select yes if you have access to a DfE Sign-in account")]
        public bool? HasSignInAccount { get; set; }
    }
}