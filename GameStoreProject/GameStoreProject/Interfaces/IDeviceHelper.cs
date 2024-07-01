using GameStoreProject.Models;

namespace GameStoreProject.Interfaces
{
    public interface IDeviceHelper
    {
        void Add(Device device);
        void Remove(int id);
        void Update(Device device, int id);
        List<Device> GetAll();
        Device GetById(int id);
        Device GetByName(string name);
        IEnumerable<SelectListItem> DevicesListToselectListItems();
        IEnumerable<GameDevice> selectListItemsToDevicesList(IEnumerable<SelectListItem> devices);
    }
}
