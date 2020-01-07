using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using VideoShell.Extension.Abstraction;
using VideoShell.Extension.Abstraction.Models;

namespace VideoShell.Extensions.Abstraction
{

    public class WebMetadataModel : IWebMetadata
    {
        public string Name { get; set; }

        public string DefaultUrl { get; set; }
    }
}
