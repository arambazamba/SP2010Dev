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

namespace DemoWebParts.SPDataboundWebPart
{
    [ToolboxItemAttribute(false)]
    public class SPDataboundWebPart : WebPart
    {
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true), WebDisplayName("Conn String"), WebDescription("DB Verbindung")]
        public string DBConnString { get; set; }

        [Personalizable(PersonalizationScope.User), WebBrowsable(true), WebDisplayName("SQL String"), WebDescription("SQL Statement")]
        public string SQLString { get; set; }
        
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true), WebDisplayName("Table Width"), WebDescription("The width of the table")]
        public int TableWidth { get; set; }

        // define controls used in your webpart
        protected Table tbl;
        protected Label lblDescr;
        protected Label lblSelection;
        protected SPGridView spgv;

        public SPDataboundWebPart()
        {
            TableWidth = 500;
            SQLString = "Select TOP 10 ProductID, Name, ProductNumber, Color from Production.Product";
            DBConnString = "Data Source=Chiron;Initial Catalog=AdventureWorks;Integrated Security=True";
        }
        
        // populate your webpart
        protected void CreateChildControls(EventArgs e)
        {
            tbl = new Table {ID = "tblControls", Width = new Unit(TableWidth)};

            // lable showing the sql
            lblDescr = new Label
                           {
                               ID = "lblDescr",
                               Text = "Sharepoint Data Webpart using SQL Statement:<p/>" + SQLString + "<p/>"
                           };
            TableCell cell = new TableCell {ID = "tcDescr1"};
            cell.Controls.Add(lblDescr);

            TableRow row = new TableRow();
            row.Cells.Add(cell);
            tbl.Rows.Add(row);

            // the gridview
            spgv = new SPGridView {ID = "gvProducts"};

            // bind the data
            DataTable dt = GetProductsTable();
            spgv.DataSource = dt;

            // SPGridView does not support auto generate columns
            spgv.AutoGenerateColumns = false;
            foreach (DataColumn dc in dt.Columns)
            {
                spgv.Columns.Add(new BoundField {DataField = dc.ColumnName, HeaderText = dc.ColumnName, HtmlEncode = false});
            }

            // SPGridView supports grouping
            spgv.AllowGrouping = true;
            spgv.AllowGroupCollapse = true;
            spgv.GroupField = "Color";
            spgv.GroupFieldDisplayName = "Color";

            // Sorting
            spgv.AllowSorting = true;            
            spgv.DataBind();

            cell = new TableCell();
            cell.Controls.Add(spgv);
            row = new TableRow();
            row.Cells.Add(cell);
            tbl.Rows.Add(row);

            // lable showing the selection from the gridview
            lblSelection = new Label();
            cell = new TableCell();
            cell.Controls.Add(lblSelection);
            row = new TableRow();
            row.Cells.Add(cell);
            tbl.Rows.Add(row);


            Controls.Add(tbl);
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
                GridViewRow row = spgv.Rows[e.NewSelectedIndex];
                lblSelection.Text = string.Format("</p>You selected {0} with ID {1}", row.Cells[2].Text,
                                                  row.Cells[1].Text);
            }
        }
    }
}
