﻿namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.Cards;
    using TheDarkPortal.Services.Data.Fuse;
    using TheDarkPortal.Services.Data.User;
    using TheDarkPortal.Web.ViewModels.Card;

    [Authorize]
    public class CardsController : Controller
    {
        private readonly ICardService cardService;
        private readonly IUserService userService;
        private readonly IFuseService fuseService;

        public CardsController(ICardService cardService, IUserService userService, IFuseService fuseService)
        {
            this.cardService = cardService;
            this.userService = userService;
            this.fuseService = fuseService;
        }

        public IActionResult Index(string searchType, string searchString, int id = 1)
        {
            this.ViewData["CurrentFilter"] = searchString;
            this.ViewData["TypeFilter"] = searchType;

            IEnumerable<CardViewModel> userCards = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                userCards = this.cardService.GetAllSearchedCards<CardViewModel>(id, 3, searchString);
            }
            else
            {
                userCards = this.cardService.GetAllCards<CardViewModel>(id, 3);
            }

            var viewModel = new CardListViewModel { Cards = userCards, PageNumber = id, CardCount = this.cardService.GetCount(), ItemsPerPage = 3 };

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currencies = this.userService.GetUserCurrencies(userId);
            viewModel.Currencies = currencies;

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

            var currencies = this.userService.GetUserCurrencies(userId);

            var fuseCards = this.fuseService.GetUserFuseCards<FuseCardViewModel>(userId);
            var userFuseCards = new FuseCardListViewModel { Cards = fuseCards, PageNumber = id, CardCount = 2, ItemsPerPage = 2 };

            var viewModel = new CombinedMyCardsViewModel
            {
                Cards = userCrdsList,
                Currencies = currencies,
                FuseCards = userFuseCards,
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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currencies = this.userService.GetUserCurrencies(userId);

            var viewModel = this.cardService.CardDetails(id);
            viewModel.Currencies = currencies;

            return this.View(viewModel);
        }

        public async Task<IActionResult> LevelUp(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.cardService.LevelUp(id, userId);

            return this.RedirectToAction(nameof(this.CardDetails), new { id = id });
        }

        public async Task<IActionResult> ToFuse(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.fuseService.AddToFuse(id, userId);

            return this.RedirectToAction(nameof(this.MyCards));
        }

        public async Task<IActionResult> RemoveFromFuse(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.fuseService.RemoveFromFuse(id, userId);

            return this.RedirectToAction(nameof(this.MyCards));
        }

        public async Task<IActionResult> Fuse()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.fuseService.Fuse(userId);

            return this.RedirectToAction(nameof(this.MyCards));
        }

        public async Task<IActionResult> AddCardToBattleSet(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.cardService.AddCardToBattleCardsSet(id, userId);

            return this.RedirectToAction(nameof(this.MyCards));
        }

        public async Task<IActionResult> RemoveCardFromBattleSet(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.cardService.RemoveCardFromBattleCardsSet(id, userId);

            return this.RedirectToAction(nameof(this.MyCards));
        }
    }
}
