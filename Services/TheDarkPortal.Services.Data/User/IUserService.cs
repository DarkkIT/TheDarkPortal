namespace TheDarkPortal.Services.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.User;

    public interface IUserService
    {
        UserCurrencyViewModel GetUserCurrencis(string userId);
    }
}
