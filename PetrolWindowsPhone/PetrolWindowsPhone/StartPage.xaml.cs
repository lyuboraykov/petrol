using PetrolWindowsPhone.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Core;
using Windows.UI.Popups;
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
	class SpeedCoef
	{
		private double kmPerHour;
		double coefficient;

		public double KmPerHour
		{
			get
			{
				return this.kmPerHour;
			}
			private set
			{
				this.kmPerHour = value;
			}
		}
		public double Coefficient
		{
			get
			{
				return this.coefficient;
			}
			private set
			{
				this.coefficient = value;
			}
		}

		public SpeedCoef() { }

		public SpeedCoef(double kmPerHour, double coefficient)
		{
			this.kmPerHour = kmPerHour;
			this.coefficient = coefficient;
		}
	}
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class StartPage : Page
	{
		private NavigationHelper navigationHelper;
		private Geolocator geolocator;
		private static DateTime lastDateTime;
		private static DateTime currentDateTime;
		private static int totalCalls;
		private static double totalDistance;

		SpeedCoef[] speedCoefList = new SpeedCoef[]
		{
			new SpeedCoef(8.0, 3.2),
			new SpeedCoef(16.0, 2.133333333),
			new SpeedCoef(24.0, 1.6),
			new SpeedCoef(32.0, 1.454545455),
			new SpeedCoef(40.0, 1.230769231),
			new SpeedCoef(48.0, 1.142857143),
			new SpeedCoef(56.0, 1.103448276),
			new SpeedCoef(64.0, 1.066666667),
			new SpeedCoef(72.0, 1.066666667),
			new SpeedCoef(80.0, 1.032258065),
			new SpeedCoef(88.0, 1.0),
			new SpeedCoef(96.0, 1.103448276),
			new SpeedCoef(104.0, 1.185185185),
			new SpeedCoef(112.0, 1.28),
			new SpeedCoef(120.0, 1.391304348)
		};

		public StartPage()
		{
			this.InitializeComponent();

			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
			this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
		}

		private void InitializeGeolocator()
		{
			geolocator = new Geolocator();
			geolocator.DesiredAccuracyInMeters = 50;
			geolocator.MovementThreshold = 50;
			geolocator.PositionChanged += geolocator_PositionChanged;
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
			PouseButton.IsEnabled = false;
			ResetButton.IsEnabled = false;
		}

		private async void StartSessionButton_Click(object sender, RoutedEventArgs e)
		{
			//buttons visibility
			sessionData.Visibility = Visibility.Visible;
			startButton.Visibility = Visibility.Collapsed;
			PouseButton.IsEnabled = true;
			ResetButton.IsEnabled = true;

			//get location
			InitializeGeolocator();
			currentDateTime = DateTime.Now;

			try
			{
				Geoposition geoposition = await geolocator.GetGeopositionAsync(
					maximumAge: TimeSpan.FromMinutes(5),
					timeout: TimeSpan.FromSeconds(10));

				BasicGeoposition myLocation = new BasicGeoposition
				{
					Longitude = geoposition.Coordinate.Longitude,
					Latitude = geoposition.Coordinate.Latitude
				};

				Geopoint p = new Geopoint(myLocation);
				MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(p);//(("OMV", p, 1);
				//MapLocationFinder.FindLocationsAsync("omv", p, 10);

				foreach (MapLocation mapLocation in result.Locations)
				{
					//geolocation.Text += mapLocation.DisplayName;

					//if (mapLoc)
				}
				//With this 2 lines of code, the app is able to write on a Text Label the Latitude and the Longitude, given by {{Icode|geoposition}}

				//geolocation.Text = "GPS:" + geoposition.Coordinate.Latitude.ToString("0.00") + ", " + geoposition.Coordinate.Longitude.ToString("0.00") + ", " + geoposition.CivicAddress;
			}
			//If an error is catch 2 are the main causes: the first is that you forgot to include ID_CAP_LOCATION in your app manifest. 
			//The second is that the user doesn't turned on the Location Services
			catch (Exception ex)
			{
			}
		}

		void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
		{
		}

		async void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
		{
			await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
			{
				SpeedTextBlock.Text = args.Position.Coordinate.Speed.ToString();
				//geolocation.Text = args.Position.Coordinate.Speed.ToString();
				lastDateTime = currentDateTime;
				currentDateTime = DateTime.Now;
				//vremeto mejdu za koito sme izminali metrite
				TimeSpan timeInterval = lastDateTime - currentDateTime;

				//v = s/t;
				double metersPerSecond = 50.0 / timeInterval.Ticks;

				double kmPerHour = metersPerSecond * 3.6;

				//pazi kolko puti se vika towa neshto
				totalCalls++;

				double distanceWithCoef = 0.0;
				//pazi razstoqnieto (razgledai koefcienti) vnimaavai s km/h, m/s

				for (int i = speedCoefList.Length - 1; i >= 0; i--)
				{
					SpeedCoef speedCoef = speedCoefList[i];
					if (kmPerHour <= speedCoef.KmPerHour)
					{
						distanceWithCoef = 50.0 * speedCoef.Coefficient;
						break;
					}
				}

				totalDistance += distanceWithCoef;

				DistanceTextBlock.Text = totalDistance.ToString("0.00");
				ElapsedTimeTextBlock.Text = string.Format("{0}:{1}:{2}", timeInterval.Hours, timeInterval.Minutes, timeInterval.Seconds);
			});
		}
	}
}
