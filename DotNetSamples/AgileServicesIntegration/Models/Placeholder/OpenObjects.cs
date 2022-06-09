using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AgileServicesIntegration.Models.Placeholder
{
    internal class OpenObjects
    {
    }

    [XmlRoot(ElementName = "GetOpenDocumentsResponse")]
    public class GetOpenDocumentsResponse
    {
        [XmlElement(ElementName = "CreationDateTime")]
        public string CreationDateTime { get; set; }

        [XmlElement(ElementName = "Environment")]
        public string Environment { get; set; }

        [XmlElement(ElementName = "NumberOfDocuments")]
        public string NumberOfDocuments { get; set; }

        [XmlElement(ElementName = "Documents")]
        public Documents Documents { get; set; }
    }

    [XmlRoot(ElementName = "Documents")]
    public class Documents
    {
        [XmlElement(ElementName = "Document")]
        public List<Document> Document { get; set; }
    }

    [XmlRoot(ElementName = "Document")]
    public class Document
    {
        [XmlElement(ElementName = "DocumentIdInternal")]
        public string DocumentIdInternal { get; set; }
        [XmlElement(ElementName = "DocumentId")]
        public string DocumentId { get; set; }
        [XmlElement(ElementName = "Version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "Issue")]
        public string Issue { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Language")]
        public string Language { get; set; }
        [XmlElement(ElementName = "Class")]
        public string Class { get; set; }
        [XmlElement(ElementName = "ProjectHType")]
        public string ProjectHType { get; set; }
        [XmlElement(ElementName = "ClassDescription")]
        public string ClassDescription { get; set; }
        [XmlElement(ElementName = "OEM_PrivatLabel")]
        public string OEM_PrivatLabel { get; set; }
        [XmlElement(ElementName = "LZFNumber")]
        public string LZFNumber { get; set; }
        [XmlElement(ElementName = "WSA")]
        public string WSA { get; set; }
        [XmlElement(ElementName = "Products")]
        public Products Products { get; set; }
    }

    [XmlRoot(ElementName = "Products")]
    public class Products
    {
        [XmlElement(ElementName = "Product")]
        public List<Product> Product { get; set; }
    }

    [XmlRoot(ElementName = "Product")]
    public class Product
    {
        [XmlElement(ElementName = "ProjId")]
        public string ProjId { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Segment")]
        public string Segment { get; set; }

        [XmlElement(ElementName = "ProductLine")]
        public string ProductLine { get; set; }

        [XmlElement(ElementName = "Platform")]
        public string Platform { get; set; }
    }






}
