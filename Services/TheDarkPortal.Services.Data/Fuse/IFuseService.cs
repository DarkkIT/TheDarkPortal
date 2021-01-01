namespace TheDarkPortal.Services.Data.Fuse
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using System.Threading.Tasks;
    using TheDarkPortal.Web.ViewModels.Card;

    public interface IFuseService
    {
        Task AddToFuse(int id, string userId);

        IEnumerable<FuseCardViewModel> GetUserFuseCards<T>(string userId);

        Task RemoveFromFuse(int id, string userId);
    }
}
