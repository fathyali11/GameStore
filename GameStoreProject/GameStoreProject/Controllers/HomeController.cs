using GameStoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameStoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameHelper gameHelper;

        public HomeController(IGameHelper _gameHelper)
        {
            this.gameHelper = _gameHelper;
        }

        public IActionResult Index()
        {
            var games = gameHelper.GetAll();
            return View(games);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
