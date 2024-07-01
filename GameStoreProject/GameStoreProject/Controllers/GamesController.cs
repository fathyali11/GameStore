using GameStoreProject.Interfaces;
using GameStoreProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreProject.Controllers
{
    public class GamesController : Controller
    {
        public IGameHelper GameHelper;
        public GamesController(IGameHelper gameHelper)
        {
            GameHelper = gameHelper;
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View(GameHelper.GetFullGameViewModel(new GameViewModel()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel gameViewModel)
        {

            if(!ModelState.IsValid)
            {
                GameViewModel game=GameHelper.GetFullGameViewModel(gameViewModel);
                return View(game);
            }
            await GameHelper.Add(gameViewModel);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
