using PetrolWindowsPhone.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PetrolWindowsPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
		private NavigationHelper navigationHelper;

        public StartPage()
        {
			this.InitializeComponent();

			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
			this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void StopSession(object sender, RoutedEventArgs e)
        {

        }

		private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
		}

		private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
		{
		}

        private void RestartSession(object sender, RoutedEventArgs e)
        {
            sessionData.Visibility = Visibility.Collapsed;
            startButton.Visibility = Visibility.Visible;
            stopButton.IsEnabled = false;
            restartButton.IsEnabled = false;
        }

        private async void StartSessionButton_Click(object sender, RoutedEventArgs e)
        {
			//buttons visibility
            sessionData.Visibility = Visibility.Visible;
            startButton.Visibility = Visibility.Collapsed;
            stopButton.IsEnabled = true;
            restartButton.IsEnabled = true;

			//get location
        }
    }
}
