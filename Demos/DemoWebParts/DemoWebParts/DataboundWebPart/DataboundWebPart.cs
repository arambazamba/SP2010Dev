using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace DemoWebParts.DataboundWebPart
{
    [ToolboxItemAttribute(false)]
    public class DataboundWebPart : WebPart
    {
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true), WebDisplayName("Conn String"), WebDescription("DB Verbindung"), Category("Databinding")]
        public string DBConnString { get; set; }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true), WebDisplayName("SQL String"), WebDescription("SQL Statement"), Category("Databinding")]
        public string SQLString { get; set; }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true), WebDisplayName("Table Width"), WebDescription("The width of the table")]
        public int TableWidth { get; set; }

        // define controls used in your webpart
        protected Table layoutTable;
        protected Label lblDescr;
        protected Label lblSelection;
        protected GridView gv;

        public DataboundWebPart()
        {
            TableWidth = 500;
            SQLString = "Select TOP 5 ProductID, Name, ProductNumber from Production.Product";
            DBConnString = "Data Source=Chiron;Initial Catalog=AdventureWorks;Integrated Security=True";
        }

        // populate your webpart
        protected void CreateChildControls(EventArgs e)
        {
            layoutTable = new Table {ID = "tblControls", Width = new Unit(TableWidth)};

            // lable showing the sql
            lblDescr = new Label
                           {
                               ID = "lblDescr",
                               Text = "Sharepoint Data Webpart using SQL Statement:<p/>" + SQLString + "<p/>"
                           };

            TableCell cell = new TableCell();
            cell.Controls.Add(lblDescr);
            TableRow row = new TableRow();
            row.Cells.Add(cell);
            layoutTable.Rows.Add(row);

            // the gridview
            gv = new GridView {ID = "gvProducts", AutoGenerateSelectButton = true};

            // hook the event handler
            gv.SelectedIndexChanging += RowSelected;

            // bind the data
            gv.DataSource = GetProductsTable();
            gv.DataBind();

            cell = new TableCell();
            cell.Controls.Add(gv);
            row = new TableRow();
            row.Cells.Add(cell);
            layoutTable.Rows.Add(row);

            // lable showing the selection from the gridview
            lblSelection = new Label();

            cell = new TableCell();
            cell.Controls.Add(lblSelection);
            row = new TableRow();
            row.Cells.Add(cell);
            layoutTable.Rows.Add(row);

            Controls.Add(layoutTable);
        }

        protected DataTable GetProductsTable()
        {
            SqlConnection con = new SqlConnection(DBConnString);
            SqlCommand cmd = new SqlCommand(SQLString, con);
            DataTable dt = new DataTable("ProductsTable");
            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        protected void RowSelected(object sender, GridViewSelectEventArgs e)
        {
            if (e.NewSelectedIndex >= 0)
            {
                GridViewRow row = gv.Rows[e.NewSelectedIndex];
                lblSelection.Text = string.Format("</p>You selected {0} with ID {1}", row.Cells[2].Text,
                                                  row.Cells[1].Text);
            }
        }
    }
}
