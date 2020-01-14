using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoShell.Models
{
    public class VideoSourceItem
    {
        [PrimaryKey]
        public string Url { get; set; }
        public string Name { get; set; }
        public bool IsDefaultUrl { get; set; }
    }
}
