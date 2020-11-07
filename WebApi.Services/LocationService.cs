using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Entities.Helpers;
using WebApi.Services.Helpers;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class LocationService : ILocationService
    {
        private DataContext _context;

        public LocationService(DataContext context)
        {
            _context = context;
        }

        public Location Create(Location location)
        {
            var existing = _context.Location.FirstOrDefault(x => x.Value == location.Value);
            if (existing == null)
            {
                _context.Location.Add(location);
                _context.SaveChanges();
            }
            else
                return existing;

            return location;
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Location.Where(l => l.IsCorOrdinates == false);
        }

        /// <summary>
        /// save location for user
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public void CreateUserLocation(int userId, int locationId)
        {
            //check if user exists
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
                return;

            var existing = _context.UserLocation.FirstOrDefault(x => x.UserId == userId && x.LocationId == locationId);
            if (existing == null)
            {
                UserLocation uLocation = new UserLocation();
                uLocation.UserId = userId;
                uLocation.LocationId = locationId;

                _context.UserLocation.Add(uLocation);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Location> GetLocationsByUser(int userId)
        {
            var locations = from l in _context.Location
                            from u in _context.UserLocation
                            where l.Id == u.LocationId && u.UserId == userId
                            select new Location
                            {
                                Id = l.Id,
                                IsCorOrdinates = l.IsCorOrdinates,
                                Value = l.Value
                            };

            return locations;
        }
    }
}
