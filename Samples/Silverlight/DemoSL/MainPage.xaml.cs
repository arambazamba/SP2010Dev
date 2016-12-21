using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.Threading;
using System.Windows.Data;


namespace DemoSL
{
    public partial class MainPage : UserControl
    {
        protected ClientContext ctx;
        protected Web web;
        protected List list;
        protected ListItemCollection listitems;
        protected Contract CurrentContact;
        protected ClientRequestFailedEventArgs ErrorArgs;
        
        public MainPage()
        {
            InitializeComponent();
        }

        protected void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ctx = new ClientContext("http://chiron/");
            web = ctx.Web;
            ctx.Load(web);
            list = web.Lists.GetByTitle("Contracts");
            ctx.Load(list);
            
            CamlQuery query = new CamlQuery
                                  {
                                      ViewXml = "<View><ViewFields><FieldRef Name=\"ID\" /><FieldRef Name=\"Title\" /><FieldRef Name=\"FileLeafRef\" /><FieldRef Name=\"Lieferant\" /></ViewFields></View>"
                                  };

            listitems = list.GetItems(query);
            ctx.Load(listitems);
            ctx.ExecuteQueryAsync(OnBindQuerySucceeded, OnRequestFailed);
        }

        protected void OnBindQuerySucceeded(Object sender, ClientRequestSucceededEventArgs args)
        {
            Dispatcher.BeginInvoke(BindData);
        }

        protected void OnRequestFailed(Object sender, ClientRequestFailedEventArgs args)
        {
            ErrorArgs = args;
            Dispatcher.BeginInvoke(WriteError);
        }

        protected void BindData()
        {
            List<Contract> projects = new List<Contract>();
            foreach (ListItem li in listitems)
            {
                projects.Add(new Contract
                                 {
                                     ID=li["ID"].ToString(),
                                     Title = li["Title"].ToString(),
                                     Name = li["FileLeafRef"].ToString(),
                                     Lieferant = li["Lieferant"].ToString()
                });
            }

            PagedCollectionView source = new PagedCollectionView(projects);
            source.SortDescriptions.Add(new System.ComponentModel.SortDescription("ID", System.ComponentModel.ListSortDirection.Ascending));
            dgContracts.ItemsSource = source;
            lblStatus.Content = "Grid filled";
        }

        protected void WriteError()
        {
            lblStatus.Content = "Status: Request Failed - Error: " +  ErrorArgs.ErrorDetails;
        }

        private void SaveRow(object sender, RoutedEventArgs e)
        {
            CurrentContact = (Contract)dgContracts.SelectedItem;
            CamlQuery camlQuery = new CamlQuery
                                      {
                                          ViewXml =
                                              @"<View><Query><Where><Eq><FieldRef Name='ID'/><Value Type='Text'>" + CurrentContact.ID +
                                              "</Value></Eq></Where></Query></View>"
                                      };
            listitems = list.GetItems(camlQuery);
            ctx.Load(listitems);
            ctx.ExecuteQueryAsync(OnGetItemByIDSuccess,OnRequestFailed);     

        }

        protected void OnGetItemByIDSuccess(Object sender, ClientRequestSucceededEventArgs args)
        {
            Dispatcher.BeginInvoke(UpdateItem);
        }

        protected void UpdateItem()
        {
            foreach (ListItem li in listitems)
            {
                if (li["ID"].ToString() == CurrentContact.ID)
                {
                    li["Title"] = CurrentContact.Title;
                    li["Lieferant"] = CurrentContact.Lieferant;
                    li.Update();                    
                }
            }
            ctx.ExecuteQueryAsync(OnBindQuerySucceeded, OnRequestFailed);
        }
    }
}
