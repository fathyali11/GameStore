using GameStoreProject.Data;
using GameStoreProject.Interfaces;
using GameStoreProject.Models;
using GameStoreProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace GameStoreProject.Helpers
{
    public class GameHelper : IGameHelper
    {
        public ApplicationDbContext context;
        private readonly IDeviceHelper deviceHelper;
        private readonly ICategoryHelper categoryHelper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GameHelper(ApplicationDbContext _context,IDeviceHelper deviceHelper,ICategoryHelper categoryHelper,IWebHostEnvironment webHostEnvironment)
        {
            this.context = _context;
            this.deviceHelper = deviceHelper;
            this.categoryHelper = categoryHelper;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task Add(GameViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };

            context.Add(game);
            context.SaveChanges();
        }



        public List<Game> GetAll()
        {
            return context.Games.Include(a=>a.Category).Include(a=>a.Devices).ThenInclude(a=>a.Device).AsNoTracking().ToList();
        }

        public Game GetById(int id)
        {
            return context.Games.Find(id);
        }

        public Game GetByName(string name)
        {
            return context.Games.FirstOrDefault(x => x.Name == name);
        }

        public void Remove(int id)
        {
            context.Games.Remove(GetById(id));
        }

        public void Update(GameViewModel model, int id)
        {
            Game OldGame=GetById(id);
            OldGame.Name = model.Name;
            OldGame.Description = model.Description;

            context.SaveChanges(true);
        }

        GameViewModel IGameHelper.GetFullGameViewModel(GameViewModel model)
        {
            GameViewModel game = new GameViewModel();
            game.Name = model.Name==null?null:model.Name;
            game.Description = model.Description==null?null:model.Description;
            game.Cover = model.Cover==null?null:model.Cover;
            game.Categories = categoryHelper.CategoriesListToselectListItems();
            game.Devices = deviceHelper.DevicesListToselectListItems();
            return game;
        }


        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "games");
            var path = Path.Combine(imagesPath, coverName);

            // Ensure the directory exists
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            using var stream = new FileStream(path, FileMode.Create);
            await cover.CopyToAsync(stream);

            return coverName;
        }


    }
}
