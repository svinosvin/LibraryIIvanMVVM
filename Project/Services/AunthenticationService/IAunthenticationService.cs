using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.AunthenticationService
{
    public interface IAunthenticationService
    {
        Task<bool> Register(string username, string password, string confirmPassword, string email, string telnumber);
        Task<User> Login(string username, string password, string confirmPassword);


    }
}
