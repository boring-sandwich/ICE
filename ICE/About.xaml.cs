using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace ICE
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = false;

            }
        }
        private void txbMoreApps_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowAllApps();
        }

        private static void ShowAllApps()
        {
            MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();

            marketplaceSearchTask.SearchTerms = "geekypanda";
            marketplaceSearchTask.Show();
        }

        private void txbRateApp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private void txbShareApp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask();

            shareLinkTask.Title = "In Case of Emergency";
            shareLinkTask.LinkUri = new Uri("http://www.windowsphone.com/en-us/store/app/ice-in-case-of-emergency/1e6c4bf3-1600-4fb7-af6b-c21a46c21bec", UriKind.Absolute);
            shareLinkTask.Message = "Here is a great emergency contact app for your Windows Phone.";

            shareLinkTask.Show();
        }

        private void imgLogo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowAllApps();
        }

        private void imgAppIcon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();

            marketplaceDetailTask.ContentIdentifier = "1e6c4bf3-1600-4fb7-af6b-c21a46c21bec";
            marketplaceDetailTask.Show();
        }

        private void txbAgreementMessage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("Privacy policy: all information entered is stored locally on the app. No information is automatically collected. User controls the use, sharing and access of data. No data (personal or otherwise) is transmitted to the developer or third parties. Security: to safeguard your personal information against loss, theft and unauthorized access, use and modification; ensure you password protect your device and regularly revise information shown via Kids Corner.", "Information", MessageBoxButton.OK);
        }
    }
}