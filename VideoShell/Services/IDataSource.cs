using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoShell.Services
{
    public interface IDataSource<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
