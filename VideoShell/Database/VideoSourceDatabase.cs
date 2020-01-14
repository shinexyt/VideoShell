using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using VideoShell.Models;

namespace VideoShell.Database
{
    public class VideoSourceDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public VideoSourceDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(VideoSourceItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(VideoSourceItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<VideoSourceItem>> GetItemsAsync()
        {
            return Database.Table<VideoSourceItem>().ToListAsync();
        }
        public Task<int> GetItemsCountAsync()
        {
            return Database.Table<VideoSourceItem>().CountAsync();
        }
        //public Task<List<VideoSourceItem>> GetItemsNotDoneAsync()
        //{
        //    return Database.QueryAsync<VideoSourceItem>("SELECT * FROM [VideoSource] WHERE [Done] = 0");
        //}

        public Task<VideoSourceItem> GetItemAsync(string url)
        {
            return Database.Table<VideoSourceItem>().Where(i => i.Url == url).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(VideoSourceItem item)
        {
            var videoSourceItem =await Database.Table<VideoSourceItem>().FirstOrDefaultAsync(i => i.Url == item.Url);
            if (videoSourceItem != null)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(VideoSourceItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}

