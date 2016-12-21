using System;
using System.Data;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ECMASample.Layouts.ECMASample
{
    public partial class ECMASample : LayoutsPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AddScriptManager();
        }

        private void AddScriptManager()
        {
            ScriptManager sm = ScriptManager.GetCurrent(Page);
            if (sm == null && Page.Form != null)
            {
                sm = new ScriptManager {ID = Page.Form.ID + "_ScriptManager"};
                Page.Form.Controls.Add(sm);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Needed if you want to update using ECMA
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            if (IsPostBack==false)
            {
                BindGrid();
            }            
        }

        protected void BindGrid()
        {
            gvContacts.DataSource = GridSource.BindGrid();
            gvContacts.DataBind();
        }

    }  
}
