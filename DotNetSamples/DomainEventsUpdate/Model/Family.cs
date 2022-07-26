using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class Family
    {
        public Family()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrivateLabel { get; set; }
        public string BrandLabelId { get; set; }
        public string BrandLableName { get; set; }
        public Guid ExternalId { get; set; }
        public string ExternalProjectId { get; set; }
        public string PermanentKey { get; set; }
        public bool IsEarlyExtract { get; set; }

        public virtual Brand Parent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
