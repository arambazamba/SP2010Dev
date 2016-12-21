using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace KBx.Layouts.KBx
{
    public partial class DocumentLinks : LayoutsPageBase
    {
        private const string mainTab = @"
                    <Tab
                        Id=""Ribbon.ActionsTab""
                        Title=""Actions""
                        Description=""Actions on incoming links""
                        Sequence=""1105"">
                    <Scaling
                        Id=""Ribbon.ActionsTab.Scaling"">
                        <MaxSize
                        Id=""Ribbon.ActionsTab.MaxSize""
                        GroupId=""Ribbon.ActionsTab.DocumentActions""
                        Size=""OneLargeTwoMedium""/>
                        <Scale
                        Id=""Ribbon.ActionsTab.Scaling.CustomTabScaling""
                        GroupId=""Ribbon.ActionsTab.DocumentActions""
                        Size=""OneLargeTwoMedium"" />
                        <MaxSize
                        Id=""Ribbon.ActionsTab.MaxSize""
                        GroupId=""Ribbon.ActionsTab.IncomingLinks""
                        Size=""OneLargeTwoMedium""/>
                        <Scale
                        Id=""Ribbon.ActionsTab.Scaling.CustomTabScaling""
                        GroupId=""Ribbon.ActionsTab.IncomingLinks""
                        Size=""OneLargeTwoMedium"" />
                    </Scaling>
                    <Groups Id=""Ribbon.ActionsTab.Groups"">
                        <Group
                        Id=""Ribbon.ActionsTab.DocumentActions""
                        Description=""All actions on Documents""
                        Title=""Document Actions""
                        Sequence=""51""
                        Template=""Ribbon.Templates.DefaultTemmplate"">
                        <Controls Id=""Ribbon.ActionsTab.DocumentActions.Controls"">
                            <Button Id=""Ribbon.ActionsTab.DocumentActions.TakeOffline"" 
                            Sequence=""13""
                            Image32by32=""/_layouts/kbx/images/offline.png""
                            Description=""Takes the document offline""
                            Command=""Ribbon.ActionsTab.DocumentActions.TakeOffline.Click""
                            LabelText=""Take offline""
                            TemplateAlias=""cust1""/>                           
                            <Button Id=""Ribbon.ActionsTab.DocumentActions.ReviewFeedback""
                            Sequence=""14""
                            Description=""Review feedback""
                            Command=""Ribbon.ActionsTab.DocumentActions.ReviewFeedback.Click""
                            Image32by32=""/_layouts/kbx/images/document_review.png""
                            LabelText=""Review Feedback""
                            TemplateAlias=""cust2""/>
                        </Controls>
                        </Group>
                        <Group
                        Id=""Ribbon.ActionsTab.IncomingLinks""
                        Description=""All actions on Incoming Links""
                        Title=""Incoming Links""
                        Sequence=""52""
                        Template=""Ribbon.Templates.DefaultTemmplate"">
                        <Controls Id=""Ribbon.ActionsTab.IncomingLinks.Controls"">        
                            <Button Id=""Ribbon.ActionsTab.IncomingLinks.RemoveLinksButton"" 
                            Sequence=""17""
                            Image32by32=""/_layouts/kbx/images/DeleteRed.png""
                            Description=""Remove all incoming links""
                            Command=""Ribbon.ActionsTab.IncomingLinks.RemoveLinksButton.Click""
                            LabelText=""Remove Links""
                            TemplateAlias=""cust3""/>                           
                            <Button Id=""Ribbon.ActionsTab.IncomingLinks.ContactOwnerButton""
                            Sequence=""15""
                            Description=""Sends a notification to the owner of the links""
                            Command=""Ribbon.ActionsTab.IncomingLinks.ContactOwnerButton.Click""
                            Image32by32=""/_layouts/kbx/images/Mail.png""
                            LabelText=""Contact Owners""
                            TemplateAlias=""cust4""/>
                        </Controls>
                        </Group>
                    </Groups>
                    </Tab>";


        private const string feedbackTab = @"
                    <Tab
                        Id=""Ribbon.FeedbackTab""
                        Title=""Feedback""
                        Description=""Actions on feedback""
                        Sequence=""1106"">
                        <Scaling
                        Id=""Ribbon.FeedbackTab.Scaling"">
                        <MaxSize
                        Id=""Ribbon.FeedbackTab.MaxSize""
                        GroupId=""Ribbon.FeedbackTab.FeedbackActions""
                        Size=""OneLargeTwoMedium""/>
                        <Scale
                        Id=""Ribbon.FeedbackTab.Scaling.CustomTabScaling""
                        GroupId=""Ribbon.FeedbackTab.FeedbackActions""
                        Size=""OneLargeTwoMedium"" />
                    </Scaling>
                    <Groups Id=""Ribbon.FeedbackTab.Groups"">
                        <Group
                        Id=""Ribbon.FeedbackTab.FeedbackActions""
                        Description=""All actions on Feedback""
                        Title=""Feedback Actions""
                        Sequence=""51""
                        Template=""Ribbon.Templates.DefaultTemmplate"">
                        <Controls Id=""Ribbon.FeedbackTab.FeedbackActions.Controls"">
                           
                        </Controls>
                        </Group>
                    </Groups>
                    </Tab>";

        private const string contextualTabTemplate = @"
                    <GroupTemplate Id=""Ribbon.Templates.DefaultTemmplate"">
                    <Layout
                        Title=""OneLargeTwoMedium"" LayoutTitle=""OneLargeTwoMedium"">
                        <Section Alignment=""Top"" Type=""OneRow"">
                        <Row>
                            <ControlRef DisplayMode=""Large"" TemplateAlias=""cust1"" />
                            <ControlRef DisplayMode=""Large"" TemplateAlias=""cust2"" />
                            <ControlRef DisplayMode=""Large"" TemplateAlias=""cust3"" />
                            <ControlRef DisplayMode=""Large"" TemplateAlias=""cust4"" />
                        </Row>
                        </Section>
                    </Layout>
                    </GroupTemplate>";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateContent();
        }

        private void CreateContent()
        {
            if (Request.QueryString["ItemId"] != null && Request.QueryString["ListId"] != null)
            {
                SPList list = SPContext.Current.Web.Lists[new Guid(Request.QueryString["ListId"])];
                if (list != null)
                {
                    int itemID = Int32.Parse(Request.QueryString["ItemId"]);
                    SPListItem item = list.GetItemById(itemID);
                    if (item != null)
                    {
                        lblTitle.Text = string.Format("Incoming Links to: {0}", (string.IsNullOrEmpty(item.Title) == false ? item.Title : item.Name));
                        lblHeader.Text = string.Format("Incoming Links to: {0}", (string.IsNullOrEmpty(item.Title) == false ? item.Title : item.Name));
                    }

                    var pLinks = new Panel {ID = "pLinks", CssClass = "activeContainer"};
                    pLinks.Attributes.Add("style", "display: block; left: 0px;  width:100%; margin: 0px auto;");
                    pLinks.Controls.Add(Page.LoadControl("~/_ControlTemplates/KBx/LinksList.ascx"));
                    pContainer.Controls.Add(pLinks);

                    var pSendMessage = new Panel {ID = "pOther", CssClass = "inactiveContainer"};
                    pSendMessage.Attributes.Add("style", "display: none;");
                    pSendMessage.Controls.Add(Page.LoadControl("~/_ControlTemplates/KBx/SendMessage.ascx"));
                    pContainer.Controls.Add(pSendMessage);

                    var pFeedback = new Panel { ID = "pFeedback", CssClass = "inactiveContainer" };
                    pFeedback.Attributes.Add("style", "display: none;");
                    pFeedback.Controls.Add(Page.LoadControl("~/_ControlTemplates/KBx/Feedback.ascx"));
                    pContainer.Controls.Add(pFeedback);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            AddRibbonTab("ActionsTab", mainTab, true);
            AddRibbonTab("FeedbackTab", feedbackTab, false);
            AddTabEvents();
        }

        private void AddRibbonTab(string TabID, string TabDefinition, bool InitialTab)
        {
            // Gets the current instance of the ribbon on the page.
            SPRibbon ribbon = SPRibbon.GetCurrent(Page);

            //Prepares an XmlDocument object used to load the ribbon extensions.
            var ribbonExtensions = new XmlDocument();

            //Load the contextual tab XML and register the ribbon extension.
            ribbonExtensions.LoadXml(TabDefinition);

            ribbon.RegisterDataExtension(ribbonExtensions.FirstChild, "Ribbon.Tabs._children");

            //Load the custom templates and register the ribbon extension.
            ribbonExtensions.LoadXml(contextualTabTemplate);
            ribbon.RegisterDataExtension(ribbonExtensions.FirstChild, "Ribbon.Templates._children");

            ribbon.Minimized = false;
            ribbon.CommandUIVisible = true;
            string initialTabId = "Ribbon." + TabID;
            if (!ribbon.IsTabAvailable(initialTabId))ribbon.MakeTabAvailable(initialTabId);
            if (InitialTab)
            {
                ribbon.InitialTabId = initialTabId;                
            }
        }

        private void AddTabEvents()
        {
            var commands = new List<IRibbonCommand>
                               {
                                   new SPRibbonCommand("Ribbon.ActionsTab.DocumentActions.TakeOffline.Click","ShowContactForm()", "true"),
                                   new SPRibbonCommand("Ribbon.ActionsTab.DocumentActions.ReviewFeedback.Click","DeleteLinks()", "true"),
                                   new SPRibbonCommand("Ribbon.ActionsTab.IncomingLinks.ContactOwnerButton.Click","ShowContactForm()", "true"),
                                   new SPRibbonCommand("Ribbon.ActionsTab.IncomingLinks.RemoveLinksButton.Click","DeleteLinks()", "true"),
                                   new SPRibbonCommand("Ribbon.FeedbackTab.Click","DeleteLinks()", "true")
                               };

            // register the command at the ribbon. Include the callback to the server to generate the xml

            ////ScriptLink.RegisterScriptAfterUI(Page, "SP.Runtime.js", false, true);
            ////ScriptLink.RegisterScriptAfterUI(Page, "SP.js", false, true);

            //Register initialize function
            var manager = new SPRibbonScriptManager();
            var methodInfo = typeof(SPRibbonScriptManager).GetMethod("RegisterInitializeFunction",BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(manager, new object[] { Page, "InitPageComponent", "/_layouts/KBx/DocumentLinks.js", false, "SPSample.PageComponent.initialize()" });

            // Register ribbon scripts
            manager.RegisterGetCommandsFunction(Page, "getGlobalCommands", commands);
            manager.RegisterCommandEnabledFunction(Page, "commandEnabled", commands);
            manager.RegisterHandleCommandFunction(Page, "handleCommand", commands);
        }

    }
}
