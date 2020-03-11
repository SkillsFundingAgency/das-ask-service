using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.ASK.Web.Controllers
{
    public class EmailTestController : Controller
    {
        private readonly IMessageSession _messageSession;

        public EmailTestController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }
        
        [HttpGet("emailtest")]
        public async Task<IActionResult> SendEmail()
        {
            await _messageSession.Send(
                new SendEmailCommand(
                    "e9383b85-c378-47f5-bc8c-269d44c586e9", 
                    "davegouge@gmail.com", 
                    new Dictionary<string, string>(){{"Name", "Dave"}}));
            return Ok();
        }
    }
}