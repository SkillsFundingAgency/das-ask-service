using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact
{
    public class SignInDeliveryPartnerContactHandler : IRequestHandler<SignInDeliveryPartnerContactRequest, SignInDeliveryPartnerContactResponse>
    {
        private readonly AskContext _dbContext;
        private readonly ISessionService _sessionService;
        private readonly IDfeSignInApiClient _dfeSignInApiClient;

        public SignInDeliveryPartnerContactHandler(AskContext dbContext, ISessionService sessionService, IDfeSignInApiClient dfeSignInApiClient)
        {
            _dbContext = dbContext;
            _sessionService = sessionService;
            _dfeSignInApiClient = dfeSignInApiClient;
        }
        
        public async Task<SignInDeliveryPartnerContactResponse> Handle(SignInDeliveryPartnerContactRequest request, CancellationToken cancellationToken)
        {
            var contact = await _dbContext.DeliveryPartnerContacts
                .Include(dpc => dpc.DeliveryPartner)
                .SingleOrDefaultAsync(dpc => dpc.SignInId == request.SignInId, cancellationToken: cancellationToken);
            
            // if contact is null
            //     call dfe signIn to get all orgs for user
            //     if one of those orgs ukprn matches our DPs, create new contact and return true.
            //    else, return false.
            // else
            //     set session and return true

            _sessionService.Set("SignedInContact", new SignedInContact()
            {
                DisplayName = contact.DisplayName,
                DeliveryPartnerName = contact.DeliveryPartner.Name
            });

            return new SignInDeliveryPartnerContactResponse {Success = true};
        }
    }

    public class SignedInContact
    {
        public string DisplayName { get; set; }
        public string DeliveryPartnerName { get; set; }
    }
    
}