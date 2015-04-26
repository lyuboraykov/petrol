using PetrolWindowsPhone.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PetrolWindowsPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            this.Toast();

          //  ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
          //  XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
          //
          //  XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
          //  toastTextElements[0].AppendChild(toastXml.CreateTextNode("aefafa"));
          //  toastTextElements[1].AppendChild(toastXml.CreateTextNode("wferteratgrege"));
          //
          //  IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
          //  ((XmlElement)toastNode).SetAttribute("duration", "long");
          //
          //  ToastNotification toast = new ToastNotification(toastXml);
          //  toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(3600);
          //
          //  ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

		private void FacebookLoginButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(StartPage));
		}

		private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(StatsPage));
		}

		private void RefuelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(RefuelPage));
		}

        private void Toast()
        {
            DateTime now = DateTime.Now;
            DateTime date = now.AddMinutes(1);
            DateTimeOffset toastOffset = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
            ToastManager manager = new ToastManager();
            string toastHeader = "TOASTOAT";
            string toastContent = "TESTESTESTES";
            manager.SendScheduledToast(toastHeader, toastContent, toastOffset);
           
        }
		private void LogoutButton_Click(object sender, RoutedEventArgs e)
		{

		}
    }
}
