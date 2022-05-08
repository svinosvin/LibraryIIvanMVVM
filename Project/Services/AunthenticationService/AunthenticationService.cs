using Microsoft.AspNet.Identity;
using Models.Models;
using Project.Exceptions;
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
                if(result != PasswordVerificationResult.Success)
                {
                    throw new InvalidPasswordException(username,password);
                }
            }
            return user;

        }   
        public async Task<RegisterResult> Register(string username, string password, string confirmPassword, string email, string telnumber)
        {
            RegisterResult registerResult = RegisterResult.Succes;

            if (password == confirmPassword)
                registerResult = RegisterResult.PasswordNotMatched;

            if (_accountService.GetByUsername(username) != null)
                registerResult = RegisterResult.UsernameExists;

            if (_accountService.GetByEmail(email) != null)
                registerResult = RegisterResult.EmailExists;

            if(registerResult == RegisterResult.Succes)
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
            return registerResult;




        }

    }
}
