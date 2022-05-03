using Microsoft.AspNet.Identity;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.AunthenticationService
{
    public class AunthenticationService : IAunthenticationService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IAccountDataService _accountService;

        public AunthenticationService(IPasswordHasher hasher, IAccountDataService accountService)
        {
            _hasher = hasher;
            _accountService = accountService;
        }

        public async Task<User> Login(string username, string password, string confirmPassword)
        {
            User user = new User();
            if(password == confirmPassword)
            {
                user = await _accountService.GetByUsername(username);
                PasswordVerificationResult result = _hasher.VerifyHashedPassword(user.Password, password);
                if(result == PasswordVerificationResult.Success)
                {
                    throw new Exception();
                }
            }
            return user;

        }

        public async Task<bool> Register(string username, string password, string confirmPassword, string email, string telnumber)
        {
            bool succes = false;

            if(password == confirmPassword)
            {
               
                string HashedPassword = _hasher.HashPassword(password);
                User user = new User
                {
                    Login = username,
                    Password = HashedPassword,
                    Person = new Person
                    {
                        Email = email,
                        TelNumber = telnumber
                    }
                };
                await _accountService.Create(user);
            }
            return succes;


        }
    }
}
