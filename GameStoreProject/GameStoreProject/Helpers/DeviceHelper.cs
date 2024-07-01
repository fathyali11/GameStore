using GameStoreProject.Interfaces;
using GameStoreProject.Models;

namespace GameStoreProject.Helpers
{
    public class DeviceHelper : IDeviceHelper
    {
        private readonly ApplicationDbContext context;

        public DeviceHelper(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Device device)
        {
            context.Devices.Add(device);
            context.SaveChanges();
        }

        public List<Device> GetAll()
        {
            return context.Devices.ToList();
        }

        public Device GetById(int id)
        {
            return context.Devices.Find(id);
        }

        public Device GetByName(string name)
        {
            return context.Devices.FirstOrDefault(x => x.Name == name);
        }

        public void Remove(int id)
        {
            Device device = GetById(id);
            context.Devices.Remove(device);
            context.SaveChanges();
        }

        public void Update(Device device, int id)
        {
            Device OldDevice=GetById(id);
            OldDevice.Name = device.Name;
            OldDevice.Icon = device.Icon;
            context.SaveChanges();
        }
        public IEnumerable<SelectListItem> DevicesListToselectListItems()
        {
            return GetAll().Select(device => new SelectListItem
            {
                Text = device.Name,
                Value = device.Id.ToString()
            }).OrderBy(x => x.Text).ToList();
        }
        public IEnumerable<GameDevice> selectListItemsToDevicesList(IEnumerable<SelectListItem> devices)
        {
            var deviceIds = devices.Select(item => int.Parse(item.Value));
            var res = deviceIds.Select(id => new GameDevice
            {
                DeviceId = id
            });
            return res;
        }
    }
}
