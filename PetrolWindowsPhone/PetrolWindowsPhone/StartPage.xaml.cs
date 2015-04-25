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
        public StartPage()
        {
            this.InitializeComponent();
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

        private void RestartSession(object sender, RoutedEventArgs e)
        {
            sessionData.Visibility = Visibility.Collapsed;
            startButton.Visibility = Visibility.Visible;
            stopButton.IsEnabled = false;
            restartButton.IsEnabled = false;
        }

        private void StartSession(object sender, RoutedEventArgs e)
        {
            sessionData.Visibility = Visibility.Visible;
            startButton.Visibility = Visibility.Collapsed;
            stopButton.IsEnabled = true;
            restartButton.IsEnabled = true;
        }
    }
}
