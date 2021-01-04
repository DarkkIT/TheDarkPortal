namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.PvPBattleService;

    public class PvPBattleController : BaseController
    {
        private readonly IPvPBattleService pvpBattleService;

        public PvPBattleController(IPvPBattleService pvpBattleService)
        {
            this.pvpBattleService = pvpBattleService;
        }

        public IActionResult Room(int roomId)
        {
            return this.View();
        }

        public async Task<IActionResult> SetUpNewBattle(string userIdOne, string userIdTwo)
        {
           var roomId = await this.pvpBattleService.SetUpBattleRoom(userIdOne, userIdTwo);
           return this.RedirectToAction("Room", roomId);
        }
    }
}
