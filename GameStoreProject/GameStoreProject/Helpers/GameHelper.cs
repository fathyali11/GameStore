using GameStoreProject.Data;
using GameStoreProject.Interfaces;
using GameStoreProject.Models;
using GameStoreProject.ViewModels;
using Microsoft.AspNetCore.Hosting;

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
            Game game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices?.Select(x => new GameDevice { DeviceId = x }).ToList() ?? new List<GameDevice>()
            };

            if (model.Cover is not null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Games");
                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Cover.CopyToAsync(fileStream);
                }
                game.Cover = uniqueFileName;
            }

            context.Games.Add(game);
            context.SaveChanges();
        }



        public List<Game> GetAll()
        {
            return context.Games.ToList();
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
    }
}
