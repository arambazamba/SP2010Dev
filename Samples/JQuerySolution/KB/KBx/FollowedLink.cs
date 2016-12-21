using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBx
{
    public class FollowedLink
    {
        public string URL { get; set; }
        public string ToolTip { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string LastAccess { get; set; }
        public int Reads { get; set; }
    }
}
