namespace TheDarkPortal.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserListViewModel : BaseUserStatsViewModel
    {
        public IEnumerable<UserViewModel> ArenaUsers { get; set; }
    }
}
