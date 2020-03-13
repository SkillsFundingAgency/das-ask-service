using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact
{
    public class SignInDeliveryPartnerContactRequest : IRequest<SignInDeliveryPartnerContactResponse>
    {
        public Guid SignInId { get; }

        public SignInDeliveryPartnerContactRequest(Guid signInId)
        {
            SignInId = signInId;
        }
    }

    public class SignInDeliveryPartnerContactResponse
    {
        public bool Success { get; set; }
    }
}