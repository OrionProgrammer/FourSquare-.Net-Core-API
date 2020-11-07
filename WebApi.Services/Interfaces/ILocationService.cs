using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface ILocationService
    {
        Location Create(Location location);

        IEnumerable<Location> GetAll();

        void CreateUserLocation(int userId, int locationId);

        IEnumerable<Location> GetLocationsByUser(int userId);
    }
}
