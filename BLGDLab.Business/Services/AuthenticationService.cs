using BLGDLab.Business.IServices;
using BLGDLab.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authentication;

        public AuthenticationService(IAuthenticationRepository authentication)
        {
            _authentication = authentication;
        }

        public async Task<dynamic> Login(string userName, string password)
        {
            return await _authentication.Login(userName, password);
        }
    }
}
