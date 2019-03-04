using System;
using System.Threading.Tasks;
using Orleans.Providers;
using SafeCity.GrainInterfaces;
using SafeCity.Model;

namespace SafeCity.Grains
{
    [StorageProvider(ProviderName = "Azure")]
    public class IssueMessage : Orleans.Grain<IssueMessageItem>, IIssueMessage
    {
        public async Task<IssueMessageItem> GetItem()
        {
            return await Task.FromResult(State);
        }

        public async Task SetItem(IssueMessageItem item)
        {
            State = item;
            await WriteStateAsync();
        }
    }
}
