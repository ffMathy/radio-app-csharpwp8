using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using RadioV2.Helpers;
using RadioV2.Resources;
using RadioV2.Storage;
using RadioV2.ViewModels;
using RadioV2.ViewModels.Helpers;

namespace RadioV2
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = RadioListViewModel.Instance;

            var tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
            {
                var cycleData = new CycleTileData();
                cycleData.CycleImages =
                    RadioListViewModel.Instance.LatestChannels.Select(c => new Uri(c.LogoUri, UriKind.Relative)).Take(9);

                tile.Update(cycleData);
            }

            if (!StorageHelper.GetSetting<bool>("HasLaunchedBefore"))
            {
                StorageHelper.StoreSetting("HasLaunchedBefore", true);

                MessageBox.Show(
                    "Der er lige nogle få ting du skal vide før du går i gang.\n\nAppen er designet sådan at den altid først prøver at afspille den bedste kvalitet. Hvis der så opstår en afbrydelse, nedsætter den kvaliteten af lyden og prøver igen.\n\nDette kan forsage at du (når du får dårligt signal) lige bliver koblet fra lyden et sekunds tid, mens vi skifter kvalitet.\n\nTil gengæld vil denne app fungere over din dataforbindelse. Du skal dog desuden bemærke, at appen fungerer bedst over WiFi, og at telefonens WiFi slukker når telefonen går i dvale.", "Hej! Lige et par småting ...", MessageBoxButton.OK);
            }

            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                MessageBox.Show("Du skal være forbundet til Internettet gennem WiFi eller via dit data-abonnement for at bruge app'en. Hvis du ikke havde forventet denne besked, kan det være tegn på at du oplever en dårlig forbindelse i øjeblikket." + Environment.NewLine + Environment.NewLine + "Prøv igen senere.", "Forbind til Internettet", MessageBoxButton.OK);
                Application.Current.Terminate();
            }

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private async void BuyFull_OnClick(object sender, RoutedEventArgs e)
        {
            await AdHelper.Instance.RemoveAds();
        }
    }
}