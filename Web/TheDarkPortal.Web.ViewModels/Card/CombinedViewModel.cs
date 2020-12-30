namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.User;

    public class CombinedViewModel
    {
        public CardListViewModel Cards { get; set; }

        public UserCurrencyViewModel Currency { get; set; }
    }
}
