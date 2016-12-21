using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
//Add using
using Microsoft.SharePoint.Utilities;

namespace Lab03.TaskView
{
    [ToolboxItemAttribute(false)]
    public class TaskView : WebPart
    {
        public TaskView()
        {
        }
        //Add Class Vars
        DateTimeControl filterDate;
        ListViewByQuery MyCustomView;
        SPQuery query;

        protected override void CreateChildControls()
        {
            LiteralControl myMessage = new LiteralControl("<H5>People and Tasks</H5>");
            this.Controls.Add(myMessage);
            this.Controls.Add(new LiteralControl("<br />"));
            //Create, configure and add the DateTimeControl to the controls collection
            filterDate = new DateTimeControl();
            filterDate.DateOnly = true;
            filterDate.AutoPostBack = true;
            filterDate.DateChanged += new EventHandler(filterDate_DateChanged);
            SPWeb thisWeb = SPContext.Current.Web;

            //Create, configure and add the ListViewByQuery to the controls collection
            MyCustomView = new ListViewByQuery();
            MyCustomView.List = thisWeb.Lists["Interviews"];
            query = new SPQuery(MyCustomView.List.DefaultView);
            query.ViewFields = "<FieldRef Name='Title' /><FieldRef Name='AssignedTo' /><FieldRef Name='DueDate' />";
            MyCustomView.Query = query;
            LiteralControl filterMessage = new LiteralControl("Tasks due on or before:");
            this.Controls.Add(filterMessage);
            this.Controls.Add(new LiteralControl("<br />"));
            this.Controls.Add(filterDate);
            this.Controls.Add(new LiteralControl("<br />"));
            this.Controls.Add(MyCustomView);            
        }
        
        void filterDate_DateChanged(object sender, EventArgs e)
        {

            //Apply CAML Query for selecting those tasks due on or before 
            //the selected date in theDateTimeControl.
            //Note that there must be a using statement for 
            //Micoroft.SharePoint.Utilities to use SPUtility class as shown
            string camlQuery = "<Where><Leq><FieldRef Name='DueDate' />"
                   + "<Value Type='DateTime'>"
                   + SPUtility.CreateISO8601DateTimeFromSystemDateTime(filterDate.SelectedDate)
                   + "</Value></Leq></Where>";
            query.Query = camlQuery;
            MyCustomView.Query = query;

        }


        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
