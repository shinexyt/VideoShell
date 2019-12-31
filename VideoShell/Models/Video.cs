using System;
using System.Collections.Generic;
using System.Text;

namespace VideoShell.Models
{
    public class Video
    {
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Duration { get; set; }
        public string UploadTime { get; set; }
        public int ViewCount { get; set; }
        public string ExtraInformation { get; set; }
    }
}
