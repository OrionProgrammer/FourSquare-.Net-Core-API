using AutoMapper;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Models.Users;
using SharpSqaureEntities = FourSquare.SharpSquare.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();

            CreateMap<SharpSqaureEntities.Venue, VenueModel>();
            CreateMap<VenueModel, Venue>();

            CreateMap<PhotoModel, Photo>();

        }
    }
}