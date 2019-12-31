using System;
using System.Collections.Generic;
using System.Text;

namespace VideoShell.Models
{
    public enum MenuItemType
    {
        Videos,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
