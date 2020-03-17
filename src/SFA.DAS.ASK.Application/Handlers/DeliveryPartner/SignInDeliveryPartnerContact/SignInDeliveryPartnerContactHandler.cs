using System;
using System.Linq;
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

            if (contact!=null)
            {
                _sessionService.Set("SignedInContact", new SignedInContact()
                {
                    DisplayName = contact.DisplayName,
                    DeliveryPartnerName = contact.DeliveryPartner.Name
                });

                return new SignInDeliveryPartnerContactResponse {Success = true};    
            }
            else
            {
                var dfeOrgUkPrns = (await _dfeSignInApiClient.GetOrganisations(request.SignInId)).Select(org => org.UkPrn.GetValueOrDefault());
                var deliveryPartnerOrgs = await _dbContext.DeliveryPartners.ToListAsync(cancellationToken: cancellationToken);
                var deliveryPartnerUkPrns = deliveryPartnerOrgs.Select(org => org.UkPrn);
                var matchingUkPrn = dfeOrgUkPrns.Intersect(deliveryPartnerUkPrns).ToList();
                if (matchingUkPrn.Any())
                {
                    var usersDeliveryPartner = deliveryPartnerOrgs.Single(dp => dp.UkPrn == matchingUkPrn.Single());

                    var deliveryPartnerContact = new DeliveryPartnerContact()
                    {
                        DeliveryPartnerId = usersDeliveryPartner.Id,
                        DisplayName = request.Name,
                        Id = Guid.NewGuid(),
                        SignInId = request.SignInId
                    };

                    await _dbContext.DeliveryPartnerContacts.AddAsync(deliveryPartnerContact, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    
                    _sessionService.Set("SignedInContact", new SignedInContact()
                    {
                        DisplayName = deliveryPartnerContact.DisplayName,
                        DeliveryPartnerName = usersDeliveryPartner.Name
                    });

                    return new SignInDeliveryPartnerContactResponse {Success = true};    
                }
                else
                {
                    return new SignInDeliveryPartnerContactResponse {Success = false};        
                }
            }
            
            return new SignInDeliveryPartnerContactResponse {Success = false};
        }
    }

    public class SignedInContact
    {
        public string DisplayName { get; set; }
        public string DeliveryPartnerName { get; set; }
    }
    
}