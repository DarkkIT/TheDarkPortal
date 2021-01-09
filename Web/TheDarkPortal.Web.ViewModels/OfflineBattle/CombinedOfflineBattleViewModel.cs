namespace TheDarkPortal.Web.ViewModels.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.User;

    public class CombinedOfflineBattleViewModel : BaseUserStatsViewModel
    {
        public AttackerCardListViewModel AttackerCards { get; set; }

        public DefenderCardListViewModel DefenderCards { get; set; }

        public UserViewModel UserInfo { get; set; }
    }
}
