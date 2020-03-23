using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Services.Email
{
    public interface IEmailService
    {
        Task SendSupportRequestSubmitted(string email, string name);
        Task SendFeedbackSubmitted(string email, string name, string organisationName);
    }
}