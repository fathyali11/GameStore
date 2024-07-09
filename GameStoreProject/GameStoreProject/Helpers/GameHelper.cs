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
        public async Task Add(CreateGameViewModel model)
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
            return context.Games
                .Include(a=>a.Category)
                .Include(a=>a.Devices)
                .ThenInclude(a=>a.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return context
                .Games
                .Include(x=>x.Category)
                .Include(x=>x.Devices)
                .ThenInclude(x=>x.Device)
                .FirstOrDefault(x=>x.Id==id);
        }

        public Game GetByName(string name)
        {
            return context.Games.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(int id)
        {
            bool isDeleted = false;

            var game= context.Games.FirstOrDefault(x=> x.Id==id);
            if (game is null)
                return isDeleted;
            context.Games.Remove(game);
            var numberOfUpdates = context.SaveChanges();
            if (numberOfUpdates > 0)
            {
                isDeleted = true;
                var path = Path.Combine(webHostEnvironment.ContentRootPath,FileSettings.ImagePath, game.Cover);
                File.Delete(path);
                return isDeleted;
            }
            else
                return isDeleted;
        }


        public async Task<Game?> Update(EditGameViewModel model)
        {
            Game ?OldGame=GetById(model.Id);
            if (OldGame is null)
                return null;

            OldGame.Name = model.Name;
            OldGame.Description = model.Description;
            OldGame.CategoryId = model.CategoryId;
            OldGame.Devices = model.SelectedDevices.Select(x => new GameDevice { DeviceId = x }).ToList();
            var OldCover = OldGame.Cover;
            var HasNewCover = model.Cover is not null;
            if(HasNewCover)
            {
                OldGame.Cover= await SaveCover(model.Cover);
            }

            var NumberOfUpdates= context.SaveChanges(true);
            if(NumberOfUpdates>0)
            {
                if (HasNewCover)
                {
                    var path = Path.Combine(FileSettings.ImagePath, OldCover);
                    File.Delete(path);
                }
                return OldGame;
            }
            else
            {
                var cover = Path.Combine(FileSettings.ImagePath, OldGame.Cover);
                File.Delete(cover);
                return null;
            }
        }

        CreateGameViewModel IGameHelper.GetFullGameViewModel(CreateGameViewModel model)
        {
            CreateGameViewModel game = new CreateGameViewModel();
            game.Name = model.Name==null?null:model.Name;
            game.Description = model.Description==null?null:model.Description;
            game.Cover = model.Cover==null?null:model.Cover;
            game.Categories = categoryHelper.CategoriesListToselectListItems();
            game.Devices = deviceHelper.DevicesListToselectListItems();
            return game;
        }


        public EditGameViewModel GetEditGameViewModel(int id)
        {
            var game=GetById(id);
            var model = new EditGameViewModel
            {
                Id=game.Id,
                Name = game.Name,
                Description = game.Description,
                CoverName = game.Cover,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(x => x.DeviceId).ToList(),
                Categories= categoryHelper.CategoriesListToselectListItems(),
                Devices=deviceHelper.DevicesListToselectListItems()
        };
            return model;
        }

        public EditGameViewModel GetFullGameViewModelEdit(EditGameViewModel model)
        {
            EditGameViewModel game = new EditGameViewModel();
            game.Id = model.Id==null?0:model.Id;
            game.Name = model.Name == null ? null : model.Name;
            game.Description = model.Description == null ? null : model.Description;
            game.Cover = model.Cover == null ? null : model.Cover;
            game.Categories = categoryHelper.CategoriesListToselectListItems();
            game.Devices = deviceHelper.DevicesListToselectListItems();
            return game;
        }
        private async Task<string?> SaveCover(IFormFile cover)
        {
            if(cover is not null)
            {
                var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
                var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "games");
                var path = Path.Combine(imagesPath, coverName);

                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                using var stream = new FileStream(path, FileMode.Create);
                await cover.CopyToAsync(stream);

                return coverName;
            }
            else
            {
                return null;
            }
            
        }
    }
}
