namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.OfflineBattle;
    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public class OfflineBattleController : Controller
    {
        private readonly IOfflineBattleService offlineBattleService;

        public OfflineBattleController(IOfflineBattleService offlineBattleService)
        {
            this.offlineBattleService = offlineBattleService;
        }

        public IActionResult OfflineBattleRoom(string defenderId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var attakerCards = this.offlineBattleService.GetUserCards<BattleCardViewModel>(userId);
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetUserCards<BattleCardViewModel>(defenderId);
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
            };

            return this.View(viewModel);
        }
    }
}
