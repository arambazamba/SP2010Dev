﻿using System.ServiceModel;

namespace PermissionServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPermissionService" in both code and config file together.
    [ServiceContract]
    public interface IPermissionService
    {
        [OperationContract]
        SecurityInfo[] GetPermissions(string SiteURL, string Web);
    }
}

