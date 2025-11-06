using BLGDLab.Business.IServices;
using BLGDLab.Business.JWTHelper;
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
            var data = await _authentication.Login(userName, password);
            string token="";
            if (data)
                token = JWTTokenGenerator.GenerateToken(data);
            var result = new
            {
                userData = data,
                token = token
            };

            return result;
        }
    }
}
