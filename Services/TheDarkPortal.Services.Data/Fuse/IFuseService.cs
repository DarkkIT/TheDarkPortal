namespace TheDarkPortal.Services.Data.Fuse
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using System.Threading.Tasks;

    public interface IFuseService
    {
        Task AddToFuseCouple(int id, string userId);
    }
}
