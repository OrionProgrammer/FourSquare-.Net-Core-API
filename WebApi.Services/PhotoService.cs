using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Entities.Helpers;
using WebApi.Services.Helpers;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class PhotoService : IPhotoService
    {
        private DataContext _context;

        public PhotoService(DataContext context)
        {
            _context = context;
        }

        public void CreateMultiple(List<Photo> photos)
        {
            foreach (var photo in photos)
            {
                var existing = _context.Photo.FirstOrDefault(x => x.Id == photo.Id);

                if (existing == null)
                {
                    _context.Photo.Add(photo);
                }
            }

            _context.SaveChanges();
        }
    }
}
