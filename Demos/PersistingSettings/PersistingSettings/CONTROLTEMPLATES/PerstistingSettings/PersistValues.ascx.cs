using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;
using Microsoft.SharePoint;

namespace Demo.CONTROLTEMPLATES
{
    public partial class PersistValues : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetTextboxValue(txtUser);
            GetTextboxValue(txtPassword);
        }

        protected void GetTextboxValue(TextBox txtBox)
        {
            if (SPContext.Current.Web.Properties[txtBox.ID] != null)
            {
                txtBox.Text = SPContext.Current.Web.Properties[txtBox.ID];
            }
        }

        protected void SaveTextboxValue(object sender, EventArgs e)
        {
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            SaveValueToPropertyBag(txtUser);
            SaveValueToPropertyBag(txtPassword);
        }

        protected void SaveValueToPropertyBag(TextBox txtBox)
        {
            if (SPContext.Current.Web.Properties.ContainsKey(txtBox.ID))
            {
                SPContext.Current.Web.Properties.Remove(txtBox.ID);
            }
            
            SPContext.Current.Web.Properties.Add(txtBox.ID, txtBox.Text);
            SPContext.Current.Web.Properties.Update();
            SPContext.Current.Web.Update();
        }
    }
}
