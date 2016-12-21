using System;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DemoWebParts.SearchWP;
using Microsoft.SharePoint;

namespace Integrations
{
    public class CustomEditorPart : EditorPart
    {
        protected Label lblListMsg;
        protected DropDownList ddList;
        protected Label lblMsg;

        protected override void CreateChildControls()
        {
            lblListMsg = new Label {Text = "Please choose a List to use<br>"};
            Controls.Add(lblListMsg);

            ddList = new DropDownList();
            SPWeb web = SPContext.Current.Web;
            foreach (SPList list in web.Lists)
            {
                if (list.Hidden==false)
                {
                    ddList.Items.Add(list.Title);
                }
            }
            Controls.Add(ddList);

            lblMsg = new Label();
            Controls.Add(lblMsg);
        }

        // is used when the user presses ok in the webpart config menu
        public override bool ApplyChanges()
        {
            try
            {
                if (ddList.SelectedValue != string.Empty)
                {
                    ((SearchWP) WebPartToEdit).ListName = ddList.SelectedValue;
                    ((SearchWP) WebPartToEdit).BindList();
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    ((SearchWP) WebPartToEdit).ListName = "Please select a list";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text += ex.Message;
            }
            return true;
        }

        //reads the current val from the webpart
        public override void SyncChanges()
        {
            try
            {
                ddList.Text = ((SearchWP) WebPartToEdit).ListName;
            }
            catch
            {
            }
        }
    }
}
