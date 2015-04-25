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
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using PetrolWindowsPhone.Common;

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
            RemoteDataManager manager = new RemoteDataManager();
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
          //  manager.AddStation("treewurywhflw7364r729q", "Sofia", "Okolovrusten put", 234.45, 2500);
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async Task<MapAddress> GeoLocation(Geoposition position)
        {
           // Geolocator geolocator = new Geolocator();
           // geolocator.DesiredAccuracyInMeters = 1;
           // Geoposition position = await geolocator.GetGeopositionAsync();
            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = position.Coordinate.Point.Position.Latitude;
            pos.Longitude = position.Coordinate.Point.Position.Longitude;
            Geopoint pointToReverseGeocode = new Geopoint(pos);
            
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

            // here also it should be checked if there result isn't null and what to do in such a case
            MapAddress address = result.Locations[0].Address;
            return address;
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
            stopButton.IsEnabled = true;
            restartButton.IsEnabled = true;
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
