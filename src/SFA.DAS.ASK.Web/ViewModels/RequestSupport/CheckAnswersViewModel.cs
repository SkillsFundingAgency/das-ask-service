using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.ViewModels.RequestSupport
{
    public class CheckAnswersViewModel
    {
        public string FirstName { get; set; }

        public CheckAnswersViewModel(TempSupportRequest supportRequest)
        {
            FirstName = supportRequest.FirstName;
        }
    }
}
