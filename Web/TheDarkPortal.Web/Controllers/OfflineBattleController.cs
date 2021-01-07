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
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.offlineBattleService.DeleteTempCards(attackerId);

            await this.offlineBattleService.SaveAttackerCards(attackerId);
            await this.offlineBattleService.SaveDefenderCards(defenderId, attackerId);

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>(attackerId);
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>(attackerId);
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> OfflineBattleRoom(string defenderId)
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.offlineBattleService.HaveAttackerTutns(attackerId);

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>(attackerId);
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>(attackerId);
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> OfflineBattleRoomOut(string defenderId)
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.offlineBattleService.HaveAttackerTutns(attackerId);

            ////Implement Defender attack to atacker

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>(attackerId);
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>(attackerId);
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AttackerSelectCard(int id)
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.offlineBattleService.AttackerSelectCard(id, attackerId);

            return this.RedirectToAction(nameof(this.OfflineBattleRoom));
        }

        public async Task<IActionResult> AttackDefenderCard(int id)
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.offlineBattleService.AttackDefenderCard(id, attackerId);

            return this.RedirectToAction(nameof(this.OfflineBattleRoomOut));
        }
    }
}
