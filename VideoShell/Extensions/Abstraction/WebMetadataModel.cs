using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using VideoShell.Extensions.Abstraction;
using VideoShell.Extensions.Abstraction.Models;

namespace VideoShell.Extensions.Abstraction
{

    public class WebMetadataModel : IWebMetadata
    {
        public string Name { get; set; }

        public string DefaultUrl { get; set; }
    }
}
