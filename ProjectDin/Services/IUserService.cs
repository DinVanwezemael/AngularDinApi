using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectDin.Models;


namespace ProjectDin.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

    }
}
