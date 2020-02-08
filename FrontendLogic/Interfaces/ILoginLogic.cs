using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FrontendLogic.Interfaces
{
    public interface ILoginLogic
    {
        Task RegisterUser(string accessToken, string userID, HttpContext httpContext);
    }
}