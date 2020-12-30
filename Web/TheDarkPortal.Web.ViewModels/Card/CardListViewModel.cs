namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels;

    public class CardListViewModel : PagingViewModel
    {
        public IEnumerable<CardViewModel> Cards { get; set; }
    }
}
