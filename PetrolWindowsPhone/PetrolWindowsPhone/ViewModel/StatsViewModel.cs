using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PetrolWindowsPhone.Model;

namespace PetrolWindowsPhone.ViewModel
{
    class StatsViewModel : ViewModelBase
    {
        private RemoteDataManager manager;
        private List<Station> topTen;
        private List<Station> franchiseRank;
        public StatsViewModel()
        {
            manager = new RemoteDataManager();
            this.franchiseRank = new List<Station>();
            this.FranchiseRank.Add(new Station("Lukoil",null,null,34,45));
            this.getTopStations();
        }

        private async void getTopStations()
        {
           List<Station> stations =  await manager.GetTopStations(10, true);
           foreach (Station station in stations)
           {
               station.Consumption = (station.Liters / station.Kilometers) * 100;
           }
           this.TopTenStations = stations;
        }

        public List<Station> FranchiseRank
        {
            get
            {
                return this.franchiseRank;
            }
            set
            {
                this.franchiseRank = value;
                RaisePropertyChanged("FranchiseRank");
            }
        }

        public List<Station> TopTenStations
        {
            get
            {
                return this.topTen;
            }
            set
            {
                this.topTen = value;
                RaisePropertyChanged("TopTenStations");
            }
        }
    }
}
