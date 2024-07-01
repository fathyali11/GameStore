using GameStoreProject.Models;

namespace GameStoreProject.Interfaces
{
    public interface ICategoryHelper
    {
        void Add(Category category);
        void Remove(int id);
        void Update(Category category, int id);
        List<Category> GetAll();
        Category GetById(int id);
        Category GetByName(string name);
        IEnumerable<SelectListItem> CategoriesListToselectListItems();
    }
}
