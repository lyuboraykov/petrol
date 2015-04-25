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
        public StatsViewModel()
        {
            manager = new RemoteDataManager();
           // this.TopTenStations = new List<Station>();
           // TopTenStations.Add(new Station("dfafaf","fafs","fd",34,45));
            this.getTopStations();
        }

        private async void getTopStations()
        {
           List<Station> stations =  await manager.GetTopStations(2, false);
           this.TopTenStations = stations;
           int a = 10;
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
