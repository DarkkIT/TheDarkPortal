namespace TheDarkPortal.Web.ViewModels.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CombinedOfflineBattleViewModel : BaseUserStatsViewModel
    {
        public AttackerCardListViewModel AttackerCards { get; set; }

        public DefenderCardListViewModel DefenderCards { get; set; }
    }
}