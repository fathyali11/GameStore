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
            
            return View(GameHelper.GetFullGameViewModel(new CreateGameViewModel()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel gameViewModel)
        {

            if(!ModelState.IsValid)
            {
                CreateGameViewModel game=GameHelper.GetFullGameViewModel(gameViewModel);
                return View(game);
            }
            await GameHelper.Add(gameViewModel);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var games = GameHelper.GetAll();
            return View(games);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var IsDeleted=GameHelper.Remove(id);
            if (IsDeleted)
                return Ok();
            return BadRequest();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = GameHelper.GetEditGameViewModel(id);
            return View(game);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameViewModel gameViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(GameHelper.GetFullGameViewModelEdit(gameViewModel));
            }
            var game=await GameHelper.Update(gameViewModel);
            if(game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var game=GameHelper.GetById(id);
            if (game == null)
                return NotFound();
            return View(game);
        }
    }
}
