namespace PetrolWindowsPhone.Model
{
    class Station
    {
        public Station(string name, string city, string address, double liters, double distance)
        {
            this.Name = name;
            this.City = city;
            this.Address = address;
            this.Liters = liters;
            this.Kilometers = distance;
            this.Consumption = (liters / distance) * 100;
        }

        public Station()
        {

        }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public double Liters { get; set; }

        public double Kilometers { get; set; }

        public double Consumption { get; set; }
    }
}
