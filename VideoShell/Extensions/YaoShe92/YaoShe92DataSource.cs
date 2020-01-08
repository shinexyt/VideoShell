﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoShell.Extensions.Abstraction;
using VideoShell.Extensions.Abstraction.Models;

namespace VideoShell.Extensions.YaoShe92
{
    [ExportWithMetadata("YaoShe92", "https://ppx144.com/latest-updates/")]
    public class YaoShe92DataSource : IDataSource<Video>
    {
        List<Video> videos;
        readonly string webUrl;
        public YaoShe92DataSource()
        {
            webUrl = WebInstance.Url;
        }
        public async Task<IEnumerable<Video>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || videos == null)
                videos = await GetDataAsync(webUrl);
            return await Task.FromResult(videos);
        }
        public async Task<string> GetTrueVideoUrl(string url)
        {
            ObjectCache cache = MemoryCache.Default;
            string trueVideoUrl = cache[url] as string;
            if (trueVideoUrl == null)
            {
                var doc = await Utility.HtmlWeb.LoadFromWebAsync(url.Replace("videos", "embed"));
                trueVideoUrl = Regex.Match(doc.DocumentNode.InnerHtml, @"https.*embed=true").Value;
                cache.Set(url, trueVideoUrl, new CacheItemPolicy());
                return trueVideoUrl;
            }
            else
                return trueVideoUrl;
        }
        private async Task<List<Video>> GetDataAsync(string uri)
        {
            var list = new List<Video>();
            try
            {
                var doc = await Utility.HtmlWeb.LoadFromWebAsync(uri);
                var nodes = doc.DocumentNode.SelectNodes("//div[@class='list-videos']//div[@class='item  ']/a");
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