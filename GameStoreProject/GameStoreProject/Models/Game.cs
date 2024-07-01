using System.ComponentModel.DataAnnotations;

namespace GameStoreProject.Models
{
    public class Game:BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; }=string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;

        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }
        public IEnumerable<GameDevice> Devices { get; set; }
    }
}
