using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServiceWCF
{
    public class SecurityInfo
    {
        public string Ressource { get; set; }
        public string SecurityPrincipal { get; set; }
        public string PermissionLevel { get; set; }
    }
}
