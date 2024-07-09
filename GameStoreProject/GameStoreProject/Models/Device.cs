using System.ComponentModel.DataAnnotations;

namespace GameStoreProject.Models
{
    public class Device:BaseEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
        public ICollection<GameDevice> Games { get; set; } = default!;
    }
}
