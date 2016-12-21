namespace DummyWebLib
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    namespace Publisher.WebForms
    {
        public static class PageExtensions
        {

            public static Control Find(this Page Obj, string ControlID)
            {
                return FindControlRecursive(Obj, ControlID);
            }

            public static Control FindControlRecursive(Control ParentControl, string ID)
            {
                if (ParentControl.ID == ID)
                {
                    return ParentControl;
                }

                return (from Control c in ParentControl.Controls select FindControlRecursive(c, ID)).FirstOrDefault(t => t != null);
            }          

            public static void RegisterScript(this UserControl obj, string Script)
            {
                if (String.IsNullOrEmpty(Script) == false)
                {
                    ClientScriptManager cs = obj.Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(obj.GetType(), Script))
                    {
                        cs.RegisterClientScriptInclude(obj.GetType(), Script, obj.ResolveClientUrl(Script));
                    }
                }
            }

            public static void RegisterScript(this Page obj, string Script)
            {
                if (String.IsNullOrEmpty(Script) == false)
                {
                    ClientScriptManager cs = obj.Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(obj.GetType(), Script))
                    {
                        cs.RegisterClientScriptInclude(obj.GetType(), Script, obj.ResolveClientUrl(Script));
                    }
                }
            }

            public static void RegisterLoadFunction(this Page obj, string Function)
            {
                if (String.IsNullOrEmpty(Function) == false)
                {
                    obj.ClientScript.RegisterStartupScript(obj.GetType(), "Startup" + Function, String.Format(@"javascript:{0}", Function), true);
                }
            }

            public static void RegisterCSS(this Page obj, string CSSPath)
            {
                if (String.IsNullOrEmpty(CSSPath) == false)
                {
                    if (obj.Header.Controls.Cast<Control>().Any(c => c is HtmlLink && (c as HtmlLink).Href == CSSPath))
                    {
                        return;
                    }

                    var css = new HtmlLink {Href = CSSPath};
                    css.Attributes["rel"] = "stylesheet";
                    css.Attributes["type"] = "text/css";
                    css.Attributes["media"] = "all";
                    obj.Header.Controls.Add(css);
                }
            }

            public static string GetResourceString(this Page obj, string resourceFile, string resourceKey)
            {
                if (HttpContext.Current == null) return resourceKey;

                var res = HttpContext.GetGlobalResourceObject(resourceFile, resourceKey);
                if (res != null) return res.ToString();
                return String.Empty;
            }

        }
    }

}
