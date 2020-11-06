using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IPhotoService
    {
        void CreateMultiple(List<Photo> photos);
    }
}
