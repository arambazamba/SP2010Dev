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

//6.
using Microsoft.SharePoint.Client;
using System.Threading;
using System.Windows.Media.Imaging;
namespace Lab10
{
    public partial class MainPage : UserControl
    {
        //7.
        ClientContext clientCtx;
        Microsoft.SharePoint.Client.List mediaLib;
        int fileTracker = 0;
        int rowTracker = 0;
        StackPanel row = new StackPanel();
        public MainPage()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //9.
            Loader.Maximum = 2;
            Loader.Value = 0;
            Status.Text = "Connecting to Web...";
            clientCtx = new ClientContext(ApplicationContext.Current.Url);
            clientCtx.Load(clientCtx.Web);
            clientCtx.ExecuteQueryAsync(updateConnectionStatus, errUpdateConnectionStatus);
        }
        //10.
        void updateConnectionStatus(Object sender, ClientRequestSucceededEventArgs e)
        {
            //This method starts on a background thread, but needs to update the progress bar and label in the UI
            //Therefore, it calls Dispatcher.BeginInvoke() to perform the UI updating on the main thread
            Dispatcher.BeginInvoke(makeProgressWebConnection);
        }
        //11.
        void errUpdateConnectionStatus(Object sender, ClientRequestFailedEventArgs e)
        {
            Status.Text = e.Message;
        }
        //12.
        void makeProgressWebConnection()
        {
            //Called by Dispatcher.BeginInvoke() in the function above
            //The code is now running back on the main UI thread, and so can update the UI
            Loader.Value++;
            Status.Text = "Web Connected. Connecting to media stores...";
            clientCtx.Load(clientCtx.Web.Lists, lists => lists
                .Include(list => list.Title, list => list.Id)
                .Where(list => list.BaseType == BaseType.DocumentLibrary && !list.Hidden)
                );


            clientCtx.ExecuteQueryAsync(updateLists, errUpdateLists);
        }
        //13.
        void updateLists(Object sender, ClientRequestSucceededEventArgs e)
        {
            //This method starts on a background thread, but needs to update the progress bar and label in the UI
            //Therefore, it calls Dispatcher.BeginInvoke() to perform the UI updating on the main thread
            Dispatcher.BeginInvoke(makeProgressListConnection);
        }
        //14.
        void errUpdateLists(Object sender, ClientRequestFailedEventArgs e)
        {
            Status.Text = e.Message;
        }
        //15.
        void makeProgressListConnection()
        {
            //Called by Dispatcher.BeginInvoke() in the function above
            //The code is now running back on the main UI thread, and so can update the UI
            Loader.Value++;
            Status.Text = "Now showing all media stores";
            foreach (List mediaStore in clientCtx.Web.Lists)
            {
                ListBoxItem mediaLibPicker = new ListBoxItem();
                mediaLibPicker.Content = mediaStore.Title;
                mediaLibPicker.Tag = mediaStore.Id;
                mediaLibPicker.MouseLeftButtonUp += new MouseButtonEventHandler(mediaLibPicker_MouseLeftButtonUp);
                LibraryPicker.Items.Add(mediaLibPicker);
            }
        }

        void mediaLibPicker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem src = (ListBoxItem)sender;
            string libID = src.Tag.ToString();
            mediaLib = clientCtx.Web.Lists.GetById(new Guid(libID));
            clientCtx.Load(mediaLib);
            clientCtx.Load(mediaLib.RootFolder);
            clientCtx.Load(mediaLib.RootFolder.Files);
            clientCtx.ExecuteQueryAsync(getListData, errGetListData);
        }
        //16.
        void getListData(Object sender, ClientRequestSucceededEventArgs e)
        {
            //This method starts on a background thread, but needs to update the progress bar and label in the UI
            //Therefore, it calls Dispatcher.BeginInvoke() to perform the UI updating on the main thread
            Dispatcher.BeginInvoke(loadFiles);
        }
        //17.
        void errGetListData(Object sender, ClientRequestFailedEventArgs e)
        {
            Status.Text = e.Message;
        }
        //18.
        private void loadFiles()
        {
            //Called by Dispatcher.BeginInvoke() in the function above
            //The code is now running back on the main UI thread, and so can update the UI 
            myContainer.Children.Clear();
            Loader.Maximum = mediaLib.RootFolder.Files.Count;
            if (Loader.Maximum != 0)
            {
                Loader.Value = 0;
                Status.Text = "Loading Files...";
                fileTracker = 0;
                foreach (File fle in mediaLib.RootFolder.Files)
                {
                    clientCtx.Load(fle);
                    clientCtx.ExecuteQueryAsync(addFileToUI, errAddFileToUI);
                }
                return;
            }
            Status.Text = "There are no files to show from this library";
        }
        void addFileToUI(Object sender, ClientRequestSucceededEventArgs e)
        {
            //This method starts on a background thread, but needs to update the progress bar and label in the UI
            //It can now also render the files, which is another UI operation
            //Therefore, it calls Dispatcher.BeginInvoke() to perform the UI updating on the main thread
            Dispatcher.BeginInvoke(addFile);
        }
        void errAddFileToUI(Object sender, ClientRequestFailedEventArgs e)
        {
            Status.Text = e.Message;
        }

        void addFile()
        {
            //Called by Dispatcher.BeginInvoke() in the function above
            //The code is now running back on the main UI thread, and so can update the UI

            string fName = mediaLib.RootFolder.Files[fileTracker].Name;
            string fUrl = mediaLib.RootFolder.Files[fileTracker].ServerRelativeUrl;
            fUrl = clientCtx.Url + fUrl;
            //Simple logic to lay out the files in a two-column configuration
            if ((rowTracker % 2) == 0)
            {
                row = new StackPanel();
                row.Orientation = Orientation.Horizontal;
                myContainer.Children.Add(row);
            }
            if ((fName.ToLower().EndsWith(".png")) || (fName.ToLower().EndsWith(".jpeg")) || (fName.ToLower().EndsWith(".jpg")))
            {
                Image img = new Image();
                img.MaxWidth = 100;
                img.MaxHeight = 50;
                img.Stretch = Stretch.Uniform;
                BitmapImage bitMap = new BitmapImage(new Uri(fUrl, UriKind.Absolute));
                img.Source = bitMap;
                ContentControl mediaButton = new ContentControl();
                mediaButton.Margin = new Thickness(2.5);
                mediaButton.Tag = fUrl;
                ToolTipService.SetToolTip(mediaButton, fName);
                mediaButton.Cursor = Cursors.Hand;
                mediaButton.MouseLeftButtonUp += new MouseButtonEventHandler(mediaButton_MouseLeftButtonUp);
                mediaButton.Content = img;
                row.Children.Add(mediaButton);
                rowTracker++;
            }
            if (fName.ToLower().EndsWith(".wmv"))
            {
                MediaElement media = new MediaElement();
                media.MaxWidth = 100;
                media.MaxHeight = 50;
                media.Stretch = Stretch.Uniform;
                media.Source = new Uri(fUrl, UriKind.Absolute);
                media.AutoPlay = false;
                ContentControl mediaButton = new ContentControl();
                mediaButton.Margin = new Thickness(2.5);
                mediaButton.Tag = fUrl;
                ToolTipService.SetToolTip(mediaButton, fName);
                mediaButton.Cursor = Cursors.Hand;
                mediaButton.MouseLeftButtonUp += new MouseButtonEventHandler(mediaButton_MouseLeftButtonUp);
                mediaButton.Content = media;
                row.Children.Add(mediaButton);
                rowTracker++;
            }
            Loader.Value++;
            fileTracker++;
            if (fileTracker >= mediaLib.RootFolder.Files.Count)
            {
                Status.Text = "All files have now been loaded";
            }
        }

        void mediaButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myMedia.Children.Clear();
            ContentControl src = (ContentControl)sender;
            Uri fUri = new Uri(src.Tag.ToString(), UriKind.Absolute);
            if ((fUri.OriginalString.EndsWith(".png")) || (fUri.OriginalString.EndsWith(".jpg")))
            {
                Image img = new Image();
                img.MaxWidth = 340;
                img.MaxHeight = 310;
                img.Stretch = Stretch.Uniform;
                BitmapImage bitMap = new BitmapImage(fUri);
                img.Source = bitMap;
                myMedia.Children.Add(img);
                return;
            }
            if (fUri.OriginalString.EndsWith(".wmv"))
            {
                MediaElement media = new MediaElement();
                media.MaxWidth = 340;
                media.MaxHeight = 310;
                media.Stretch = Stretch.Uniform;
                media.Source = fUri;
                media.AutoPlay = true;
                myMedia.Children.Add(media);
            }
        }
    }
}
