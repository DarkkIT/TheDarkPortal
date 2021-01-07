namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.OfflineBattle;
    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    [Authorize]
    public class OfflineBattleController : Controller
    {
        private readonly IOfflineBattleService offlineBattleService;

        public OfflineBattleController(IOfflineBattleService offlineBattleService)
        {
            this.offlineBattleService = offlineBattleService;
        }

        public async Task<IActionResult> NewOfflineBattleRoom(string defenderId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.offlineBattleService.DeleteTempCards();

            await this.offlineBattleService.SaveAttackerCards(userId);
            await this.offlineBattleService.SaveDefenderCards(defenderId);

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>();
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>();
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
            };

            return this.View(viewModel);
        }

        public IActionResult OfflineBattleRoom(string defenderId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>();
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>();
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> BattleStart(int id)
        {
            await this.offlineBattleService.AttackerSelectCard(id);

            return this.RedirectToAction(nameof(this.OfflineBattleRoom));
        }
    }
}
