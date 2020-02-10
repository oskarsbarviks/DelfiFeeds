using FrontendLogic.DTO;
using System.Threading.Tasks;

namespace FrontendLogic.Interfaces
{
    public interface IProfileLogic
    {
        Task<ProfileModel> GetProfileDataByID(string userID);
    }
}