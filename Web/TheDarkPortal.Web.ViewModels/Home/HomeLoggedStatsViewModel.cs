namespace TheDarkPortal.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.User;

   public class HomeLoggedStatsViewModel : BaseUserStatsViewModel
    {
        public int CountTotalUsers { get; set; }

        public int UsersOnline { get; set; }

    }
}
