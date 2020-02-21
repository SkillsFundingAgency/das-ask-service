using System;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class CancelSupportRequestViewModel
    {
        public Guid RequestId { get; }
        public string ReturnAction { get; set; }
        public string ReturnController { get; set; }

        [Required(ErrorMessage = "Select whether you would like to cancel your support request")]
        public bool? ConfirmedCancel { get; set; }
        public CancelSupportRequestViewModel(){}
        
        public CancelSupportRequestViewModel(Guid requestId, string returnAction, string returnController)
        {
            RequestId = requestId;
            ReturnAction = returnAction;
            ReturnController = returnController;
        }
    }
}