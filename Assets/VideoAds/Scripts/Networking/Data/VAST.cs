using System.Xml;
using System.Xml.Serialization;

namespace Didenko.Sayollo.VideoAds.Networking.Data
{
    [XmlRoot("VAST")]
    public class VAST
    {
        public Ad Ad;

        [XmlAttribute(AttributeName = "version")]
        public string Version;
    }
}