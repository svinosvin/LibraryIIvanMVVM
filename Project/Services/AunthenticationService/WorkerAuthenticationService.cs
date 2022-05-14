using Microsoft.AspNet.Identity;
using Models.BaseModels;
using Models.Models;
using Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.AunthenticationService
{
    public class WorkerAuthenticationService : IWorkerAuthenticationService
    {
        private readonly IWorkerDataService _workerDataService;
        private readonly IPasswordHasher _passwordHasher;

        public WorkerAuthenticationService(IWorkerDataService workerDataService, IPasswordHasher passwordHasher)
        {
            _workerDataService = workerDataService;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterResult> AddWorker(string username, string password, string confirmPassword, string email, string telephone,  AccountsVariation variation)
        {
            RegisterResult registerResult = RegisterResult.Succes;
            PositionAtWork positionAtWork = variation == AccountsVariation.Admin ? PositionAtWork.Admin : PositionAtWork.Librarian;

            if (password != confirmPassword)
                registerResult = RegisterResult.PasswordNotMatched;
            if (await _workerDataService.GetByEmail(email) != null) 
                registerResult = RegisterResult.EmailExists;
            if (await _workerDataService.GetByUsername(username) != null)
                registerResult = RegisterResult.UsernameExists;
            if (registerResult == RegisterResult.Succes)
            {

                string HashedPassword = _passwordHasher.HashPassword(password);
                Worker user = new Worker
                {
                    Login = username,
                    Password = HashedPassword,
                    Position = positionAtWork,
                    Person = new Person
                    {
                        Email = email,
                        TelNumber = telephone
                    }
                };
                await _workerDataService.Create(user);

            }
            return registerResult;



        }
        public async Task<Worker> LoginWorker(string username, string password, string confirmPassword)
        {
            Worker worker = await _workerDataService.GetByUsername(username);
            if (password == confirmPassword)
            {
               
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword( worker.Password, password);
                if (result != PasswordVerificationResult.Success)
                    throw new InvalidPasswordException(username, password);
            }

           

            return worker;
        }
    }
}
