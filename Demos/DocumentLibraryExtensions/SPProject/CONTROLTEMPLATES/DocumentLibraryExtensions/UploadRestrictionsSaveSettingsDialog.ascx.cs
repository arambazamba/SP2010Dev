using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace Integrations.DocumentLibraryExtensions.CONTROLTEMPLATES.DocumentLibraryExtensions
{
    public partial class UploadRestrictionsSaveSettingsDialog : UserControl
    {
        protected const string key = "AllowedFileTypes";
        protected const string evtclass = "Integrations.DocumentLibraryExtensions.UploadRestrictionEventReceiver";
        protected bool internalupdate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false && Request.QueryString["ListId"] != null)
            {
                SPList list = SPContext.Current.Web.Lists[new Guid(Request.QueryString["ListId"])];

                if (list != null)
                {
                    lblList.Text = String.Format("Enable Filetype Restrictions on {0}:", list.Title);
                    var receiver = list.EventReceivers.Cast<SPEventReceiverDefinition>().FirstOrDefault(er => er.Class == evtclass);
                    internalupdate = true;
                    chkFiletypes.Checked = true;
                    pEdit.Visible = receiver != null;
                    internalupdate = false;

                    var filetypes = list.RootFolder.Properties[key];
                    if (filetypes != null)
                    {
                        txtFiletypes.Text = filetypes.ToString();
                    }
                }
            }
        }

        protected void SaveSettings(object sender, EventArgs e)
        {
            if (Request.QueryString["ListId"] != null)
            {
                SPList list = SPContext.Current.Web.Lists[new Guid(Request.QueryString["ListId"])];

                if (list != null)
                {
                    string val = String.IsNullOrEmpty(txtFiletypes.Text) ? String.Empty : txtFiletypes.Text;

                    if (Request.QueryString["ListId"] != null)
                    {
                        if (list.RootFolder.Properties.ContainsKey(key))
                        {
                            list.RootFolder.Properties.Remove(key);
                        }
                        list.RootFolder.Properties.Add(key, val);
                        list.RootFolder.Update();
                        list.Update();
                    }
                }
            }
        }

        protected void EvalRestriction(object sender, EventArgs e)
        {
            if (Request.QueryString["ListId"] != null)
            {
                SPList list = SPContext.Current.Web.Lists[new Guid(Request.QueryString["ListId"])];

                if (list != null)
                {
                    if (chkFiletypes.Checked)
                    {
                        if (internalupdate == false)
                        {
                            pEdit.Visible = true;
                            list.EventReceivers.Add(SPEventReceiverType.ItemAdding, Assembly.GetExecutingAssembly().FullName, evtclass);

                        }
                    }
                    else
                    {
                        pEdit.Visible = false;
                        var receiver = list.EventReceivers.Cast<SPEventReceiverDefinition>().FirstOrDefault(er => er.Class == evtclass);
                        if (receiver != null)
                        {
                            receiver.Delete();
                        }

                        if (list.RootFolder.Properties.ContainsKey(key))
                        {
                            list.RootFolder.Properties.Remove(key);
                        }
                    }
                }
            }
        }
    }
}
