using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Xml;
using Microsoft.SharePoint.WebControls;

namespace KBx.Layouts.RibbonOM
{
    public class RibbonTab
    {
        public RibbonTab()
        {
            Groups = new List<RibbonTabGroup>();
            Commands = new List<RibbonCommand>();
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public string ImgPath { get; set; }
        public string ScriptFile { get; set; }
        public bool RegisterJQuery { get; set; }

        public static string TabTemplate = @"
                    <GroupTemplate Id=""Ribbon.Templates.DefaultTemmplate"">
                    <Layout
                        Title=""OneLargeTwoMedium"" LayoutTitle=""OneLargeTwoMedium"">
                        <Section Alignment=""Top"" Type=""OneRow"">
                        <Row>
                            <ControlRef DisplayMode=""Large"" TemplateAlias=""LargeTempate"" />
                        </Row>
                        </Section>
                    </Layout>
                    </GroupTemplate>";

        public RibbonTabGroup GetGroup(string GroupID)
        {
            return Groups.FirstOrDefault(g => g.Title == GroupID);
        }

        public List<RibbonCommand> Commands;

        public List<RibbonTabGroup> Groups;

        public IEnumerable<RibbonCommand> GetCommandsForGroup(RibbonTabGroup Group)
        {
            return Commands.Where(c => c.Group == Group);
        }

        public string GetCAML()
        {
            string result = string.Format(@"<Tab Id=""Ribbon.{0}"" Title=""{1}"" Description=""{2}"" Sequence=""{3}""><Scaling Id=""Ribbon.{0}.Scaling"">", ID, Title, Description, Sequence);

            result = Groups.Aggregate(result, (current, grp) => current + string.Format(@"<MaxSize
                        Id=""Ribbon.{0}.MaxSize""
                        GroupId=""Ribbon.{0}.Views""
                        Size=""OneLargeTwoMedium""/>
                        <Scale
                        Id=""Ribbon.{0}.Scaling.CustomTabScaling""
                        GroupId=""Ribbon.{0}.Views""
                        Size=""OneLargeTwoMedium"" />", ID));

            result += string.Format(@"</Scaling><Groups Id=""Ribbon.{0}.Groups"">", ID);

            foreach (RibbonTabGroup grp in Groups)
            {
                result += string.Format(@"<Group Id=""Ribbon.{0}.{1}"" Description=""{2}"" Title=""{1}"" Sequence=""51"" Template=""Ribbon.Templates.DefaultTemmplate""><Controls Id=""Ribbon.{0}.{1}.Controls"">", ID, grp.Title, grp.Description);

                result = GetCommandsForGroup(grp).Aggregate(result, (current, cmd) => current + string.Format(@"<Button Id=""Ribbon.{0}.{1}.{2}"" 
                            Sequence=""13""
                            Image32by32=""{3}{4}""
                            Description=""{5}""
                            Command=""Ribbon.{0}.{1}.{2}.Click""
                            LabelText=""{6}""
                            TemplateAlias=""LargeTempate""/>", ID, grp.Title, cmd.ID, ImgPath, cmd.Image, cmd.Description, cmd.Label));

                result += "</Controls></Group>";
            }

            result += "</Groups></Tab>";

            return result;
        }

        public void Register(bool InitialTab, Page Page)
        {
            // Gets the current instance of the ribbon on the page.
            SPRibbon ribbon = SPRibbon.GetCurrent(Page);

            //Prepares an XmlDocument object used to load the ribbon extensions.
            var ribbonExtensions = new XmlDocument();

            //Load the contextual tab XML and register the ribbon extension.
            ribbonExtensions.LoadXml(GetCAML());

            ribbon.RegisterDataExtension(ribbonExtensions.FirstChild, "Ribbon.Tabs._children");

            //Load the custom templates and register the ribbon extension.
            ribbonExtensions.LoadXml(TabTemplate);
            ribbon.RegisterDataExtension(ribbonExtensions.FirstChild, "Ribbon.Templates._children");

            ribbon.Minimized = false;
            ribbon.CommandUIVisible = true;
            string initialTabId = "Ribbon." + ID;
            if (!ribbon.IsTabAvailable(initialTabId)) ribbon.MakeTabAvailable(initialTabId);
            if (InitialTab)
            {
                ribbon.InitialTabId = initialTabId;
            }

            AddEvents(Page);

            if (RegisterJQuery)
            {
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(Page.GetType(), "jquery-1.6.2.min.js"))
                {
                    cs.RegisterClientScriptInclude(Page.GetType(), "jquery-1.6.2.min.js", Page.ResolveClientUrl("jquery-1.6.2.min.js"));
                }
            }
        }

        public void AddEvents(Page Page)
        {
            var commands = Commands.Select(cmd => new SPRibbonCommand(string.Format("Ribbon.{0}.{1}.{2}.Click", ID, cmd.Group.Title, cmd.ID), cmd.Script, "true")).Cast<IRibbonCommand>().ToList();

            // register the command at the ribbon. Include the callback to the server to generate the xml

            ////ScriptLink.RegisterScriptAfterUI(Page, "SP.Runtime.js", false, true);
            ////ScriptLink.RegisterScriptAfterUI(Page, "SP.js", false, true);

            //Register initialize function
            var manager = new SPRibbonScriptManager();
            var methodInfo = typeof(SPRibbonScriptManager).GetMethod("RegisterInitializeFunction", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(manager, new object[] { Page, "InitPageComponent", ScriptFile, false, "SPSample.PageComponent.initialize()" });

            // Register ribbon scripts
            manager.RegisterGetCommandsFunction(Page, "getGlobalCommands", commands);
            manager.RegisterCommandEnabledFunction(Page, "commandEnabled", commands);
            manager.RegisterHandleCommandFunction(Page, "handleCommand", commands);
        }


        public class RibbonTabGroup
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class RibbonCommand
        {
            public string ID { get; set; }
            public string Image { get; set; }
            public string Label { get; set; }
            public RibbonTabGroup Group { get; set; }
            public string Description { get; set; }
            public string Script { get; set; }
        }
    }  
}
