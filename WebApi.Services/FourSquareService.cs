using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using System.Collections.Generic;
using WebApi.Services.Helpers;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class FourSquareService : FourSquareServiceAbstract<FourSquareAuthModel>, IFourSquareService
    {
        private SharpSquare sharpSquare;

        public FourSquareService(FourSquareAuthModel fourSquareAuthModel) : base(fourSquareAuthModel)
        {
            sharpSquare = new SharpSquare(fourSquareAuthModel.ClientId, 
                                          fourSquareAuthModel.ClientSecret);
        }

        public List<Venue> SearchVenues(string location, string query, bool searchByCoOrds)
        {
            if (searchByCoOrds)
            {
                var venues = sharpSquare.SearchVenues(new Dictionary<string, string>
                {
                    { "ll", location },
                    { "query", query }
                });

                return venues;
            }
            else
            {
                var venues = sharpSquare.SearchVenues(new Dictionary<string, string>
                {
                    { "near", location },
                    { "query", query }
                });

                return venues;
            }
        }

        public List<Photo> GetPhotosByVenue(string venueId)
        {
            var photos = sharpSquare.GetVenuePhotos(venueId,
                new Dictionary<string, string>
                {
                    { "group", "venue" },
                    { "limit", "1" }
                });

            return photos;
        }
    }
}
