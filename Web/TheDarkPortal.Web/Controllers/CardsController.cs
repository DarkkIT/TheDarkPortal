namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.Cards;
    using TheDarkPortal.Services.Data.User;
    using TheDarkPortal.Web.ViewModels.Card;

    [Authorize]
    public class CardsController : Controller
    {
        private readonly ICardService cardService;
        private readonly IUserService userService;

        public CardsController(ICardService cardService, IUserService userService)
        {
            this.cardService = cardService;
            this.userService = userService;
        }

        public IActionResult Index(string searchType, string searchString, int id = 1)
        {
            this.ViewData["CurrentFilter"] = searchString;
            this.ViewData["TypeFilter"] = searchType;

            IEnumerable<CardViewModel> userCards = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                userCards = this.cardService.GetAllSearchedCards<CardViewModel>(id, 3, searchString);
            }
            else
            {
                userCards = this.cardService.GetAllCards<CardViewModel>(id, 3);
            }

            var viewModel = new CardListViewModel { Cards = userCards, PageNumber = id, CardCount = this.cardService.GetCount(), ItemsPerPage = 3 };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddCardToMyCards(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.cardService.AddCardToMyCards(id, userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult MyCards(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userCadrs = this.cardService.GetUserCardsCollection<CardViewModel>(id, 2, userId);

            var userCrdsList = new CardListViewModel { Cards = userCadrs, PageNumber = id, CardCount = this.cardService.GetUserCardCount(userId), ItemsPerPage = 2 };

            var currencis = this.userService.GetUserCurrencis(userId);

            var viewModel = new CombinedViewModel
            {
                Cards = userCrdsList,
                Currency = currencis,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteCard(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.cardService.DeleteCard(id, userId);

            return this.RedirectToAction(nameof(this.MyCards));
        }

        public IActionResult CardDetails(int id)
        {
            var viewModel = this.cardService.CardDetails(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> LevelUp(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.cardService.LevelUp(id, userId);

            return this.RedirectToAction(nameof(this.CardDetails), new { id = id });
        }
    }
}
