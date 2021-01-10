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
    using TheDarkPortal.Services.Data.User;
    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    [Authorize]
    public class OfflineBattleController : Controller
    {
        private readonly IOfflineBattleService offlineBattleService;
        private readonly IUserService userService;

        public OfflineBattleController(
            IOfflineBattleService offlineBattleService,
            IUserService userService)
        {
            this.offlineBattleService = offlineBattleService;
            this.userService = userService;
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

            await this.offlineBattleService.HaveAttackerTurns(attackerId);

            if (this.offlineBattleService.IsBattleEnd(attackerId) == "noOneWin")
            {
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
            else if (this.offlineBattleService.IsBattleEnd(attackerId) == "attackerWin")
            {
                return this.RedirectToAction(nameof(this.AttackerWin));
            }
            else
            {
                return this.RedirectToAction(nameof(this.DefenderWin));
            }
        }

        public async Task<IActionResult> OfflineBattleRoomOut(string defenderId)
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.offlineBattleService.HaveAttackerTurns(attackerId);

            if (this.offlineBattleService.IsBattleEnd(attackerId) == "noOneWin")
            {
                await this.offlineBattleService.DefenderAttack(attackerId);

                var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>(attackerId);
                var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

                var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>(attackerId);
                var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

                var viewModel = new CombinedOfflineBattleViewModel
                {
                    AttackerCards = attacerCardsList,
                    DefenderCards = defenderCardsList,
                };

                if (this.offlineBattleService.IsBattleEnd(attackerId) == "noOneWin")
                {
                    return this.View(viewModel);
                }
                else
                {
                    return this.RedirectToAction(nameof(this.DefenderWin));
                }
            }
            else if (this.offlineBattleService.IsBattleEnd(attackerId) == "attackerWin")
            {
                return this.RedirectToAction(nameof(this.AttackerWin));
            }
            else
            {
                return this.RedirectToAction(nameof(this.DefenderWin));
            }
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

        public IActionResult AttackerWin()
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>(attackerId);
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>(attackerId);
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var user = this.userService.GetUserInfo(attackerId);

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
                UserInfo = user,
            };

            return this.View(viewModel);
        }

        public IActionResult DefenderWin()
        {
            var attackerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var attakerCards = this.offlineBattleService.GetAttackerCards<BattleCardViewModel>(attackerId);
            var attacerCardsList = new AttackerCardListViewModel { Cards = attakerCards };

            var defenderCards = this.offlineBattleService.GetDefenderCards<BattleCardViewModel>(attackerId);
            var defenderCardsList = new DefenderCardListViewModel { Cards = defenderCards };

            var user = this.userService.GetUserInfo(attackerId);

            var viewModel = new CombinedOfflineBattleViewModel
            {
                AttackerCards = attacerCardsList,
                DefenderCards = defenderCardsList,
                UserInfo = user,
            };

            return this.View(viewModel);
        }

        public IActionResult StoryBattle(string enemyName)
        {
            var defenderId = this.offlineBattleService.GetEnemyId(enemyName);

            return this.RedirectToAction(nameof(this.NewOfflineBattleRoom), new { defenderId });
        }
    }
}
