using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Services.ReferenceData
{
    public interface IReferenceDataApiClient
    {
        Task<IEnumerable<ReferenceDataSearchResult>> Search(string searchTerm);
    }
}