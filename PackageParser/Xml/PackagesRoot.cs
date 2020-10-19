using System;
using System.Xml.Serialization;

namespace PackageParser.Xml
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "packages", IsNullable = false)]
    public class PackagesRoot
    {
        /// <remarks />
        [XmlElement("package")]
        public PackageNode[] Package { get; set; }
    }
}