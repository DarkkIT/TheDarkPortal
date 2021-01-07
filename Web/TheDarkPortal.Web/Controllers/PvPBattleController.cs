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
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            PvPBattleRoomViewModel viewModel = this.GetBattleRoomModelData(id);

            var currencies = this.userService.GetUserCurrencis(attackerId);
            viewModel.Currencies = currencies;

            return this.View(viewModel);
        }

        public async Task<IActionResult> SetUpNewBattle(string defenderId)
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var roomId = await this.pvpBattleService.SetUpBattleRoom(attackerId, defenderId);

            return this.RedirectToAction("Room", new { id = roomId });
        }

        public async Task<IActionResult> EndOfBattle(int roomId)
        {
           await this.pvpBattleService.RemoveFinishedBattleTempData(roomId);
           return this.RedirectToAction("Index", "Arena");
        }

        public async Task<IActionResult> SelectCard(int roomId, int cardId)
        {

            await this.pvpBattleService.SelectCard(cardId);
            return this.RedirectToAction("Room", new { id = roomId });
        }

        public async Task<IActionResult> Attack(int roomId, int attackingCardId, int defendingCardId)
        {
            var currentPlayerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.pvpBattleService.Attack(attackingCardId, defendingCardId, currentPlayerId, roomId);

            return this.RedirectToAction("Room", new { id = roomId });
        }


        private PvPBattleRoomViewModel GetBattleRoomModelData(int id)
        {
            var viewModel = new PvPBattleRoomViewModel();

            viewModel.BattleRoom = this.pvpBattleService.GetBattleRoomData(id);

            viewModel.FirstPlayerBattleCards = this.pvpBattleService.GetUserBattleCards<PvPBattleCardViewModel>(viewModel.BattleRoom.FirstUserId);

            viewModel.SecondPlayerBattleCards = this.pvpBattleService.GetUserBattleCards<PvPBattleCardViewModel>(viewModel.BattleRoom.SecondUserId);
            return viewModel;
        }
    }
}
