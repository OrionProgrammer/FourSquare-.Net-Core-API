using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
        User GetById(int id);


    }
}
