using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoShell.Extension.Abstraction
{
    public interface IDataSource<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<string> GetTrueVideoUrl(string url);
    }
}
