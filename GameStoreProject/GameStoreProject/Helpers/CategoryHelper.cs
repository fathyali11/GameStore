using GameStoreProject.Interfaces;
using GameStoreProject.Models;
using System.ComponentModel;

namespace GameStoreProject.Helpers
{
    public class CategoryHelper : ICategoryHelper
    {
        private readonly ApplicationDbContext context;

        public CategoryHelper(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories.Find(id);
        }

        public Category GetByName(string name)
        {
            return context.Categories.FirstOrDefault(x => x.Name == name);
        }

        public void Remove(int id)
        {
            Category category = GetById(id);
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void Update(Category category, int id)
        {
            Category OldCategory=GetById(id);
            OldCategory.Name= category.Name;
            context.SaveChanges();
        }
        public IEnumerable<SelectListItem> CategoriesListToselectListItems()
        {
            return GetAll().Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }).OrderBy(x => x.Text)
            .ToList();
        }
    }
}
