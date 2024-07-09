using GameStoreProject.Models;
using GameStoreProject.ViewModels;

namespace GameStoreProject.Interfaces
{
    public interface IGameHelper
    {
        //Done
        Task Add(CreateGameViewModel model);
        bool Remove(int id);
        Task<Game?> Update(EditGameViewModel model);
        List<Game> GetAll();    
        Game? GetById(int id);
        Game GetByName(string name);
        CreateGameViewModel GetFullGameViewModel(CreateGameViewModel model);
        EditGameViewModel GetFullGameViewModelEdit(EditGameViewModel model);
        EditGameViewModel GetEditGameViewModel(int id);

    }
}
