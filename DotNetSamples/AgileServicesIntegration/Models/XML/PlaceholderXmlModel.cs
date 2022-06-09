using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AgileServicesIntegration.Models.XML
{
    public class PlaceholderXmlModel
    {
        [XmlRoot("XPlm")]
        public class XPlm
        {
            [XmlElement("FieldCollection")]
            public FieldCollection FieldCollection { get; set; }

            [XmlElement("TableCollection")]
            public TableCollection TableCollection { get; set; }
        }

        [XmlRoot("FieldCollection")]
        public class FieldCollection
        {
            [XmlElement("Field")]
            public List<Field> FieldList { get; set; }
        }

        [XmlRoot("Field")]
        public class Field
        {
            [XmlElement("Name")]
            public string Name { get; set; }

            [XmlElement("Value")]
            public string Value { get; set; }
        }

        [XmlRoot("TableCollection")]
        public class TableCollection
        {
            [XmlElement("Table")]
            public List<Table> TableList { get; set; }
        }

        [XmlRoot("Table")]
        public class Table
        {
            [XmlElement("Name")]
            public string Name { get; set; }

            [XmlElement("FieldCollectionTable")]
            public FieldCollectionTable FieldCollectionTable { get; set; }
        }


        [XmlRoot("FieldCollectionTable")]
        public class FieldCollectionTable
        {
            [XmlElement("FieldCollection")]
            public FieldCollectionTableFieldCollection FieldCollectionTableFieldCollection { get; set; }
        }

        [XmlRoot("FieldCollection")]
        public class FieldCollectionTableFieldCollection
        {
            [XmlElement("Field")]
            public List<FieldCollectionTableFieldCollectionField> FieldCollectionTableFieldCollectionFieldList { get; set; }
        }

        [XmlRoot("Field")]
        public class FieldCollectionTableFieldCollectionField
        {
            [XmlElement("Name")]
            public string Name { get; set; }

            [XmlElement("Value")]
            public string Value { get; set; }
        }
    }
}
