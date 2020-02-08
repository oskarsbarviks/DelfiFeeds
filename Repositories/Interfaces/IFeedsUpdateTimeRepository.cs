using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFeedsUpdateTimeRepository
    {
        Task ChangeFeedsCategoryUpdateTime(string category, DateTime updateDate);
    }
}