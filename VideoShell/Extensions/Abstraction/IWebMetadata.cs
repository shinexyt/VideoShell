using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using VideoShell.Extension.Abstraction;
using VideoShell.Extension.Abstraction.Models;

namespace VideoShell.Extensions.Abstraction
{

    public interface IWebMetadata
    {
        string Name { get; set; }
        string DefaultUrl { get; set; }
    }
}
