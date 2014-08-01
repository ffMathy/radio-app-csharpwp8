namespace Radio.Models
{
    public class RadioBeacon
    {

        public double Frequency { get; private set; }

        public string AntennaLocationName { get; private set; }

        public WorldCoordinate AntennaLocationCoordinate { get; private set; }

        public RadioBeacon()
        {

        }

        public RadioBeacon(string antennaLocationName, double frequency,
            WorldCoordinate antennaLocationCoordinate)
        {
            this.AntennaLocationCoordinate = antennaLocationCoordinate;
            this.AntennaLocationName = antennaLocationName;
            this.Frequency = frequency;
        }

    }
}
