namespace GameStoreProject.Models
{
    public class Category
    {
        public ICollection<Game> games { get; set; }=new List<Game>();
    }
}
