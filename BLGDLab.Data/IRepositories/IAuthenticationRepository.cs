using System.Data;

namespace BLGDLab.Data.IRepository
{
    public interface IAuthenticationRepository
    {
        Task<dynamic>  Login(string userName, string password);
    }
}
