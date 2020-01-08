using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using VideoShell.Extensions.Abstraction;
using VideoShell.Extensions.Abstraction.Models;

namespace VideoShell.Extensions.Abstraction
{

    public interface IWebMetadata
    {
        string Name { get; set; }
        string DefaultUrl { get; set; }
    }
}
