using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class OtherDetailsViewModel
    {
        public string AdditionalComments { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms....")]
        
        public bool Agree { get; set; }
    }
}