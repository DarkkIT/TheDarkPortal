namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels;

    public class FuseCardListViewModel : PagingViewModel
    {
        public IEnumerable<FuseCardViewModel> Cards { get; set; }
    }
}
