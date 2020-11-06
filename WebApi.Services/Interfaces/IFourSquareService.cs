
using FourSquare.SharpSquare.Entities;
using System.Collections.Generic;

namespace WebApi.Services.Interfaces
{
    public interface IFourSquareService
    {
        public List<Venue> SearchVenues(string location, string query, bool searchByCoOrds);

        List<Photo> GetPhotosByVenue(string venueId);
    }
}
