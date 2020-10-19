using System;
using System.Xml.Serialization;

namespace PackageParser.Xml
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class PackageNode
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [XmlAttribute(AttributeName = "targetFramework")]
        public string TargetFramework { get; set; }
    }
}