using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using VideoShell.Extension.Abstraction;
using VideoShell.Extension.Abstraction.Models;

namespace VideoShell.Extensions.Abstraction
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebMetadataAttribute : ExportAttribute, IWebMetadata
    {
        public WebMetadataAttribute(string name, string defaultUrl) : base(typeof(IDataSource<Video>))
        {
            Name = name;
            DefaultUrl = defaultUrl;
        }
        public string Name { get; set; }

        public string DefaultUrl { get; set; }
    }
}
