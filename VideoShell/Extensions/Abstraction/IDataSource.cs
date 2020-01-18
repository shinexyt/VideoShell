using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoShell.Extensions.Abstraction
{
    public interface IDataSource<T>
    {
        Task<IEnumerable<T>> GetItemsAsync();
        Task<string> GetTrueVideoUrl(string url);
    }
}
