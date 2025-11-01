using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.IServices
{
    public interface IAuthenticationService
    {
        Task<dynamic> Login(string userName, string password);
    }
}
