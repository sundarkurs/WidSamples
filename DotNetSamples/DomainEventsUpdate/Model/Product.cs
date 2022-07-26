using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class Product
    {
        public Product()
        {
            Documents = new HashSet<Document>();
            ProductEndItems = new HashSet<ProductEndItem>();
            ProductSpareParts = new HashSet<ProductSparePart>();
        }

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Platform { get; set; }
        public string Pricepoint { get; set; }
        public string Segment { get; set; }
        public string Subsegment { get; set; }
        public string ProductFamilyId { get; set; }
        public DateTime? LaunchDate { get; set; }
        public Guid ExternalId { get; set; }
        public string ExternalProjectId { get; set; }
        public string PermanentKey { get; set; }
        public bool IsEarlyExtract { get; set; }

        public virtual Family Parent { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<ProductEndItem> ProductEndItems { get; set; }
        public virtual ICollection<ProductSparePart> ProductSpareParts { get; set; }
    }
}
