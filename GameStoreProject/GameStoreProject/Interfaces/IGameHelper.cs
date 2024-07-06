using GameStoreProject.Models;
using GameStoreProject.ViewModels;

namespace GameStoreProject.Interfaces
{
    public interface IGameHelper
    {
        //Done
        Task Add(GameViewModel model);
        void Remove(int id);
        void Update(GameViewModel model, int id);
        List<Game> GetAll();    
        Game GetById(int id);
        Game GetByName(string name);
        GameViewModel GetFullGameViewModel(GameViewModel model);

    }
}
