using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoShell.Caching;
using VideoShell.Extensions.Abstraction;
using VideoShell.Extensions.Abstraction.Models;

namespace VideoShell.Extensions.YaoShe92
{
    [ExportWithMetadata("YaoShe92", "https://ppx144.com/latest-updates/")]
    public class YaoShe92DataSource : IDataSource<Video>
    {
        List<Video> videos;
        public async Task<IEnumerable<Video>> GetItemsAsync()
        {
            videos = await GetDataAsync(WebInstance.Url);
            return await Task.FromResult(videos);
        }
        public async Task<string> GetTrueVideoUrl(string url)
        {
            var cache = MemoryCache.Default;
            var trueVideoUrl = cache[url] as string;
            if (trueVideoUrl == null)
            {
                var doc = await App.HtmlWeb.LoadFromWebAsync(url.Replace("videos", "embed"));
                trueVideoUrl = Regex.Match(doc.DocumentNode.InnerHtml, @"https.*embed=true").Value;
                cache[url] = trueVideoUrl;
            }
            return trueVideoUrl;
        }
        private async Task<List<Video>> GetDataAsync(string uri)
        {
            var list = new List<Video>();
            try
            {
                var doc = await App.HtmlWeb.LoadFromWebAsync(uri);
                var nodes = doc.DocumentNode.SelectNodes("//div[@class='list-videos']//div[@class='item  ']/a");
                if (nodes == null)
                {
                    var newUrl = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div[4]/div[2]/a").Attributes["href"].Value;
                    doc = await App.HtmlWeb.LoadFromWebAsync(newUrl);
                    nodes = doc.DocumentNode.SelectNodes("//div[@class='list-videos']//div[@class='item  ']/a");
                }
                foreach (var item in nodes)
                {
                    list.Add(new Video { Title = item.Attributes["title"].Value, ImageUrl = item.SelectSingleNode(".//img").Attributes["data-original"].Value, VideoUrl = item.Attributes["href"].Value, ExtraInformation = item.SelectSingleNode(".//div[@class='duration']").InnerText + " " + item.SelectSingleNode(".//div[@class='added']/em").InnerText });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex);
            }

            return list;
        }
    }
}