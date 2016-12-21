using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace KBx.Layouts.RibbonOM
{
    public static class PageExtensions
    {
        public static void RegisterGlobalScript(this Page obj, string Script)
        {
            if (string.IsNullOrEmpty(Script) == false)
            {
                ClientScriptManager cs = obj.ClientScript;
                if (!cs.IsStartupScriptRegistered(obj.GetType(), Script))
                {
                    string x = obj.ResolveClientUrl("/JavaScripts/" + Script);
                    cs.RegisterClientScriptInclude(obj.GetType(), Script, x);
                }
            }
        }

        public static void RegisterScript(this UserControl obj, string Script)
        {
            if (string.IsNullOrEmpty(Script) == false)
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
            if (string.IsNullOrEmpty(Script) == false)
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
            if (string.IsNullOrEmpty(Function) == false)
            {
                obj.ClientScript.RegisterStartupScript(obj.GetType(), "Startup" + Function, string.Format(@"javascript:{0}", Function), true);
            }
        }

        public static void RegisterCSS(this Page obj, string CSSPath)
        {
            if (string.IsNullOrEmpty(CSSPath) == false)
            {
                var css = new HtmlLink { Href = CSSPath };
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
            return string.Empty;
        }
    }
}
