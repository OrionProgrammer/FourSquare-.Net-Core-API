using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Services.Interfaces;
using WebApi.Services;
using WebApi.Services.Helpers;
using WebApi.Models;
using AutoMapper;
using FourSquare.SharpSquare.Entities;
using System.Net;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LandmarksController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private IFourSquareService _fourSquareService;
        private IMapper _mapper;
        private ILocationService _locationService;
        private IVenueService _venueService;
        private IPhotoService _photoService;
        private IHubContext<LandmarksHub> _hub;

        public LandmarksController(
            IOptions<AppSettings> appSettings, 
            IMapper mapper,
            ILocationService locationService,
            IVenueService venueService,
            IPhotoService photoService,
            IHubContext<LandmarksHub> hub)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _locationService = locationService;
            _venueService = venueService;
            _photoService = photoService;
            _hub = hub;

            FourSquareAuthModel fourSquareAuthModel = new FourSquareAuthModel();
            fourSquareAuthModel.ClientId = _appSettings.FourSquareClienId;
            fourSquareAuthModel.ClientSecret = _appSettings.FourSquareClientSecret;

            _fourSquareService = new FourSquareService(fourSquareAuthModel);
        }

        [HttpGet("search/{query}/{location}")]
        public async Task<IActionResult> Search(string query, string location)
        {
            if (string.IsNullOrEmpty(location))
                return BadRequest(new { message = "Location is required" });
            
            //check if gps co-ordinates supplied
            bool isCoOrds = Validation.IsGPSCoOrdinates(query);

            //save location asynchronously so save time
            var locationId = await Task.Run(() => SaveLocation(location, isCoOrds));

            //search for venues. I left the query parm blank as I was experiencing poor results with 'landmark' query for both search and explore.
            //I decided not to spend too much time tweaking the FourSquare results and rather focus more on developing the API
            var venues = _fourSquareService.SearchVenues(location, "", isCoOrds);

            //prepare venue photos for client
            var photos = PrepareVenuePhotoModelTest(venues, locationId);

            var venueList = _mapper.Map<List<Venue>, List<VenueModel>>(venues);
            var venueDBList = _mapper.Map<List<VenueModel>, List<WebApi.Entities.Venue>>(venueList);

            //save venues for location
            var taskSaveVenues = await Task.Run(() => SaveVenues(venueDBList, locationId));

            //map to entities list
            var photosDbList = _mapper.Map<List<PhotoModel>, List<WebApi.Entities.Photo>>(photos);
            //save photos
             _photoService.CreateMultiple(photosDbList);

            return Ok();
        }

        private List<PhotoModel> PrepareVenuePhotoModel(List<Venue> venues, int locationId)
        {
            List<PhotoModel> photoModels = new List<PhotoModel>();

            foreach (var venue in venues)
            {
                //fecth photos for each venue
                var photos = _fourSquareService.GetPhotosByVenue(venue.id);

                PhotoModel photoModel = new PhotoModel();

                photoModel.VenueName = venue.name;
                photoModel.VenueId = venue.id;
                photoModel.LocationId = locationId;

                try
                {
                    //try catch to prevent index out of range error crashing the app. If the error occurs, 
                    //simply move on to next venue

                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(string.Format("{0}{1}x{2}{3}",
                            photos[0].prefix,
                            photos[0].width,
                            photos[0].height,
                            photos[0].suffix));

                        photoModel.Image = imageBytes;
                    }

                    photoModel.Id = photos[0].id;
                    photoModel.ImageCredit = photos[0].user.firstName + " " + photos[0].user.lastName;

                    _hub.Clients.All.SendAsync("transferphotodata", photoModel);

                    photoModels.Add(photoModel);
                }
                catch { }
            }

            return photoModels;
        }


        private List<PhotoModel> PrepareVenuePhotoModelTest(List<Venue> venues, int locationId)
        {
            List<PhotoModel> photoModels = new List<PhotoModel>();

            foreach (var venue in venues)
            {
                PhotoModel photoModel = new PhotoModel();

                photoModel.VenueName = venue.name;
                photoModel.VenueId = venue.id;
                photoModel.LocationId = locationId;

                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData("https://fastly.4sqi.net/img/general/1440x1920/412105192_eiNJVudpwkVtZn9rtu8M1dCSq_4UwE-xvey-ErOh7i0.jpg");
                    photoModel.Image = imageBytes;
                }

                photoModel.Id = Guid.NewGuid().ToString();
                photoModel.ImageCredit = "Asheen Singh";

                photoModels.Add(photoModel);

                _hub.Clients.All.SendAsync("transferphotodata", photoModel);
            }

            return photoModels;
        }


        private int SaveLocation(string location, bool isCoOrds)
        {
            Entities.Location _location = new Entities.Location();
            _location.Value = location;
            _location.IsCorOrdinates = isCoOrds;

            _location = _locationService.Create(_location);

            return _location.Id;
        }

        private bool SaveVenues(List<Entities.Venue> venues, int locationId)
        {
            _venueService.CreateMultiple(venues, locationId);
           return true;
        }
    }
}
