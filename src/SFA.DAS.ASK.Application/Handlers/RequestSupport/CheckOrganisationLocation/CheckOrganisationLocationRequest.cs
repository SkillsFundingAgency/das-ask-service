using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.CheckOrganisationLocation
{
    public class CheckOrganisationLocationRequest : IRequest<bool>
    {
        public string Postcode { get; }

        public CheckOrganisationLocationRequest(string postcode)
        {
            Postcode = postcode;
        }
    }
}