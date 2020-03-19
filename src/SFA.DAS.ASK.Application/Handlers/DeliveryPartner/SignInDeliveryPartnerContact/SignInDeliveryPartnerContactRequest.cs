using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact
{
    public class SignInDeliveryPartnerContactRequest : IRequest<SignInDeliveryPartnerContactResponse>
    {
        public Guid SignInId { get; }
        public string Name { get; set; }

        public SignInDeliveryPartnerContactRequest(Guid signInId, string name)
        {
            SignInId = signInId;
            Name = name;
        }
    }

    public class SignInDeliveryPartnerContactResponse
    {
        public bool Success { get; set; }
    }
}