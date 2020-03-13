using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.ASK.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IMessageSession _messageSession;

        public EmailService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }
        
        public async Task SendSupportRequestSubmitted(string email, string name)
        {
            await _messageSession.Send(
                    new SendEmailCommand(
                        "e9383b85-c378-47f5-bc8c-269d44c586e9", 
                        email, 
                        new Dictionary<string, string>() {{"Name", name}}))
                .ConfigureAwait(false);
        }

        public async Task SendFeedbackSubmitted(string email, string name, string organisationName)
        {
            await _messageSession.Send(
                    new SendEmailCommand(
                        "29240998-5865-4817-a314-4c7b0b730c3f", 
                        email, 
                        new Dictionary<string, string>()
                        {
                            {"Name", name},
                            {"OrganisationName", organisationName}
                        }))
                .ConfigureAwait(false);
        }
    }
}