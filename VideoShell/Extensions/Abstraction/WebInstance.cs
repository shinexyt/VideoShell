using System;
using System.Collections.Generic;
using System.Text;
using VideoShell.Extension.Abstraction;
using VideoShell.Extension.Abstraction.Models;

namespace VideoShell.Extensions.Abstraction
{
    public static class WebInstance
    {
        public static IDataSource<Video> DataSource { get; set; }
        public static string Url { get; set; }
    }
}
