namespace TheDarkPortal.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.Arena;
    using TheDarkPortal.Web.ViewModels.User;

    public class ArenaController : Controller
    {
        private readonly IArenaService arenaService;

        public ArenaController(IArenaService arenaService)
        {
            this.arenaService = arenaService;
        }

        public IActionResult Index()
        {
            var users = this.arenaService.GetAllArenaUsers<UserViewModel>();

            var viewModel = new UserListViewModel { ArenaUsers = users };

            return this.View(viewModel);
        }
    }
}
