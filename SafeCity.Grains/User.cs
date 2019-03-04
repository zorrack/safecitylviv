using System;
using System.Threading.Tasks;
using Orleans.Providers;
using SafeCity.GrainInterfaces;
using SafeCity.Model;

namespace SafeCity.Grains
{
    [StorageProvider(ProviderName = "Azure")]
    public class User : Orleans.Grain<UserItem>, IUser
    {
        public async Task<UserItem> GetItem()
        {
            return await Task.FromResult(State);
        }

        public async Task SetItem(UserItem item)
        {
            State = item;
            await WriteStateAsync();
        }
    }
}
