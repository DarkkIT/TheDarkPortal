namespace TheDarkPortal.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using TheDarkPortal.Services.Data.User;
    using TheDarkPortal.Web.ViewModels;
    using TheDarkPortal.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = new HomeLoggedStatsViewModel();
            var currencies = this.userService.GetUserCurrencis(userId);

            model.Currencies = currencies;

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
