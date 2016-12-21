using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Integrations;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace DemoWebParts.SearchWP
{
    [ToolboxItemAttribute(false)]
    public class SearchWP : WebPart
    {
        public SearchWP()
        {
            ListName = "Customers";
        }

        public string ListName { get; set; }

        protected Table tbl;
        protected DropDownList ddTitleSelection;
        protected GridView gvResult;
        protected Label lblMessage;
        
        public override EditorPartCollection CreateEditorParts()
        {
            ArrayList partsArray = new ArrayList();
            CustomEditorPart ce = new CustomEditorPart { ID = ID + "_editorPart1", Title = "List" };
            partsArray.Add(ce);
            EditorPartCollection parts = new EditorPartCollection(partsArray);
            return parts;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try // sorry - no other way to check if the list exists
            {
                tbl = new Table();

                ddTitleSelection = new DropDownList {AutoPostBack = true};
                BindList();
                AddControl(ddTitleSelection);
                Controls.Add(tbl);
            }
            catch (Exception)
            {
                lblMessage = new Label {Text = "Please configure this webpart to display a valid list"};
                Controls.Add(lblMessage);
            }
        }
        
        public void BindList()
        {
            if (ddTitleSelection != null)
            {
                ddTitleSelection.DataSource = GetFilterValues();
                ddTitleSelection.SelectedIndexChanged += CustomerSelected;
                ddTitleSelection.DataBind();
            }
        }

        private void CustomerSelected(object sender, EventArgs e)
        {
            string titleVal = ddTitleSelection.SelectedValue;
            string camlQuery = @"<Where><Eq><FieldRef Name=""Title"" /><Value Type=""Text"">" + titleVal + "</Value></Eq></Where>";

            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists[ListName];

            if (list != null)
            {
                SPQuery query = new SPQuery {Query = camlQuery};
                SPListItemCollection filteredItems = list.GetItems(query);
                DataTable dt = filteredItems.GetDataTable();
                gvResult = new GridView {DataSource = dt};
                gvResult.DataBind();
                AddControl(gvResult);
            }
        }

        protected List<string> GetFilterValues()
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists[ListName];
            List<string> result = null;

            if (list != null)
            {
                result = new List<string>();
                foreach (SPListItem c in list.Items)
                {
                    if (result.Contains(c.Title) == false)
                    {
                        result.Add(c.Title);
                    }
                }
            }

            return result;
        }
        
        protected void AddControl(Control C)
        {
            var cell = new TableCell();
            cell.Controls.Add(C);
            var row = new TableRow();
            row.Controls.Add(cell);
            if (tbl != null)
            {
                tbl.Rows.Add(row);
            }
        }

    }
}
