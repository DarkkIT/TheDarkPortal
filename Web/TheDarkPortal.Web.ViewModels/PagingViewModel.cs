namespace TheDarkPortal.Web.ViewModels
{
    using System;

    public class PagingViewModel : BaseUserStatsViewModel
    {
        public int PageNumber { get; set; }

        public int CardCount { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviosPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviosPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.CardCount / this.ItemsPerPage);
    }
}
