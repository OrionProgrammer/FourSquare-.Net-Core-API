using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Entities.Helpers;
using WebApi.Services.Helpers;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class VenueService : IVenueService
    {
        private DataContext _context;

        public VenueService(DataContext context)
        {
            _context = context;
        }

        public void CreateMultiple(List<Venue> venues, int locationId)
        {
            foreach (var venue in venues)
            {
                var existing = _context.Venue.FirstOrDefault(x => (x.Name == venue.Name)
                && (x.LocationId == locationId));

                if (existing == null)
                {
                    venue.LocationId = locationId;
                    _context.Venue.Add(venue);
                }
            }

            _context.SaveChanges();
        }
    }
}
