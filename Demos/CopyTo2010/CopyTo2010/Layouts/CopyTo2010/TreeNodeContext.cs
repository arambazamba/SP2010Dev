using System;

/// <summary>
/// Summary description for TreeNodeContext
/// </summary>
public class TreeNodeContext
{
    public string SiteCollection { get; set; }

    public Guid WebID { get; set; }

    public Guid ListID { get; set; }

    public TreeNodeContext(string SiteCollectionURL, Guid WebGUID)
    {
        ListID = Guid.Empty;
        SiteCollection = SiteCollectionURL;
        WebID = WebGUID;
    }

    public TreeNodeContext(string SiteCollectionURL, Guid WebGUID, Guid ListGUID)
    {
        SiteCollection = SiteCollectionURL;
        WebID = WebGUID;
        ListID = ListGUID;
    }

    public TreeNodeContext(string Context)
    {
        string sep = ";";
        string[] ctx = Context.Split(sep.ToCharArray());
        SiteCollection = ctx[0];
        WebID = new Guid(ctx[1]);
        ListID = new Guid(ctx[2]);
    }

    public bool IsList
    {
        get
        {
            if (ListID == Guid.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public override string ToString()
    {
        return SiteCollection + ";" + WebID + ";" + ListID;
    }
}
