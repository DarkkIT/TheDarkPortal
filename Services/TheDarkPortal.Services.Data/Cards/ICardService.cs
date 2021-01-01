namespace TheDarkPortal.Services.Data.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Web.ViewModels.Card;

    public interface ICardService
    {
        int GetCount();

        int GetUserCardCount(string userId);

        IEnumerable<CardViewModel> GetAllCards<T>(int page, int itemsPerPage);

        IEnumerable<CardViewModel> GetUserCardsCollection<T>(int page, int itemsPerPage, string userId);

        IEnumerable<CardViewModel> GetAllSearchedCards<T>(int page, int itemsPerPage, string searchString);

        Task AddCardToMyCards(int cardId, string userId);

        Task DeleteCard(int id, string userId);

        CardViewModel CardDetails(int id);

        Task LevelUp(int id, string userId);
    }
}
