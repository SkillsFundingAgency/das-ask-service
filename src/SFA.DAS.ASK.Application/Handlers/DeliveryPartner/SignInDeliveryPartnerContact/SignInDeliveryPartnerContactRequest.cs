using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact
{
    public class SignInDeliveryPartnerContactRequest : IRequest<SignInDeliveryPartnerContactResponse>
    {
        public Guid SignInId { get; }
        public string Name { get; }
        public string EmailAddress { get; }

        public SignInDeliveryPartnerContactRequest(Guid signInId, string name, string emailAddress)
        {
            SignInId = signInId;
            Name = name;
            EmailAddress = emailAddress;
        }
    }

    public class SignInDeliveryPartnerContactResponse
    {
        public bool Success { get; set; }
    }
}