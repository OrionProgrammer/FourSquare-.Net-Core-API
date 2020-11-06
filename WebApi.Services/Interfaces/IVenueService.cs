using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IVenueService
    {
        void CreateMultiple(List<Venue> venues, int locationId);
    }
}
