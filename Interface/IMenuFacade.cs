using SampleApp.Models;

namespace SampleApp.Interface
{
    public interface IMenuFacade : IFacadeBase<Menu>
    {
        Task<List<Menu>> GetByUserID(int userId);
    }
}
