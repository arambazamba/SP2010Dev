using System;
using System.Web.UI.WebControls;
using KBx.Layouts.RibbonOM;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace KBx.Layouts
{
    public partial class RibbonUsingObjectModel : LayoutsPageBase
    {
       protected void Page_Load(object sender, EventArgs e)
       {
           Page.RegisterScript("Ribbon.js");
       }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var tab = new RibbonTab
                          {
                              ID = "ActionsTab",
                              Description = "My Actions",
                              ImgPath = "/_layouts/images/RibbonOM/",
                              Title = "Actions",
                              ScriptFile = "/_layouts/RibbonOM/Ribbon.js",
                              RegisterJQuery = true,
                              Sequence = 1550
                          };

            tab.Groups.Add(new RibbonTab.RibbonTabGroup {Title = "Views", Description = "Change your views"});

            tab.Commands.Add(new RibbonTab.RibbonCommand
                                 {
                                     ID = "cmdClickMe",
                                     Description = "A command to be clicked",
                                     Group = tab.GetGroup("Views"),
                                     Image = "package.png",
                                     Label = "Click me",
                                     Script = "DoAlert()"
                                 });

            tab.Register(true, this);
        }
        }
}
