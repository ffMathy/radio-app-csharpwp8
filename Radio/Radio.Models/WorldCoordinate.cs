namespace Radio.Models
{
    public class WorldCoordinate
    {

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public WorldCoordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

    }
}
