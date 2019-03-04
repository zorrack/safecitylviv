using System;
using System.Threading.Tasks;

namespace SafeCity.GrainInterfaces
{
    public interface IGrainWithItem<TItem> : Orleans.IGrainWithGuidKey
    {
        Task<TItem> GetItem();
        Task SetItem(TItem item);
    }
}
