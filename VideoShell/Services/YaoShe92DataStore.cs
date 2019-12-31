using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;
using VideoShell.Models;

namespace VideoShell.Services
{
    public class YaoShe92DataStore : IDataSource<Video>
    {
        List<Video> videos;
        public static HtmlWeb HtmlWeb { get; } = new HtmlWeb();
        public YaoShe92DataStore()
        {

        }
        public async Task<IEnumerable<Video>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || videos == null)
                videos = await GetDataAsync("https://yaoshe92.com/latest-updates/");
            return await Task.FromResult(videos);
        }
        private async Task<List<Video>> GetDataAsync(string uri)
        {
            var list = new List<Video>();
            try
            {
                var doc = await HtmlWeb.LoadFromWebAsync(uri);
                var nodes = doc.DocumentNode.SelectNodes("//div[@class='list-videos']//div[@class='item  ']/a");
                foreach (var item in nodes)
                {
                    list.Add(new Video { Title = item.Attributes["title"].Value, ImageUrl = item.SelectSingleNode(".//img").Attributes["data-original"].Value, VideoUrl = item.Attributes["href"].Value, ExtraInformation = item.SelectSingleNode(".//div[@class='duration']").InnerText + " " + item.SelectSingleNode(".//div[@class='added']/em").InnerText });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return list;
        }
    }
}