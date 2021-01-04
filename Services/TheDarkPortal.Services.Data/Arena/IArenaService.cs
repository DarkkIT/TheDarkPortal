namespace TheDarkPortal.Services.Data.Arena
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TheDarkPortal.Web.ViewModels.User;

    public interface IArenaService
    {
        IEnumerable<UserViewModel> GetAllArenaUsers<T>();
    }
}
