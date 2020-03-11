using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Services.Email
{
    public interface IEmailService
    {
        Task SendFeedbackSubmitted(string email, string name);
    }
}