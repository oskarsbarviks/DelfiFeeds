using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FrontendLogic.Interfaces
{
    public interface IAuthorizationLogic
    {
        Task AuthorizeUser(string id, string name, string email, HttpContext httpContext);
    }
}