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
    }
}
