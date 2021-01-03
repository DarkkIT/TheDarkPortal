namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.User;

    public class CombinedMyCardsViewModel : BaseUserStatsViewModel
    {
        public CardListViewModel Cards { get; set; }

        public FuseCardListViewModel FuseCards { get; set; }
    }
}
