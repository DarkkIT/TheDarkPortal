namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.Cards;
    using TheDarkPortal.Services.Data.PvPBattleService;
    using TheDarkPortal.Services.Data.User;
    using TheDarkPortal.Web.ViewModels.Card;
    using TheDarkPortal.Web.ViewModels.PvPBattle;

    public class PvPBattleController : BaseController
    {
        private readonly IPvPBattleService pvpBattleService;
        private readonly ICardService cardService;
        private readonly IUserService userService;

        public PvPBattleController(IPvPBattleService pvpBattleService, ICardService cardService, IUserService userService)
        {
            this.pvpBattleService = pvpBattleService;
            this.cardService = cardService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Room(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new PvPBattleRoomViewModel();

            viewModel.BattleRoom = this.pvpBattleService.GetBattleRoomData(id);

            viewModel.FirstPlayerBattleCards = this.pvpBattleService.GetUserCardsCollection<CardViewModel>(viewModel.BattleRoom.FirstUserId);

            viewModel.SecondPlayerBattleCards = this.pvpBattleService.GetUserCardsCollection<CardViewModel>(viewModel.BattleRoom.SecondUserId);

            var currencies = this.userService.GetUserCurrencis(userId);
            viewModel.Currencies = currencies;

            return this.View(viewModel);
        }

        public async Task<IActionResult> SetUpNewBattle(string userTwoId)
        {
            var userIdOne = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var roomId = await this.pvpBattleService.SetUpBattleRoom(userIdOne, userTwoId);

            return this.RedirectToAction("Room", new { id = roomId });
        }
    }
}
