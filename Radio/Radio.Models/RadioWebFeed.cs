namespace Radio.Models
{
    public class RadioWebFeed
    {

        public string AreaName { get; set; }

        public string HighQualityStreamUri { get; set; }

        public string LowQualityStreamUri { get; set; }

        public RadioWebFeed()
        {
            
        }

        public RadioWebFeed(string highQualityStreamUri, string lowQualityStreamUri) : this(null, highQualityStreamUri, lowQualityStreamUri)
        {
            
        }

        public RadioWebFeed(string areaName, string highQualityStreamUri, string lowQualityStreamUri)
        {
            AreaName = areaName;
            HighQualityStreamUri = highQualityStreamUri;
            LowQualityStreamUri = lowQualityStreamUri;
        }

    }
}
