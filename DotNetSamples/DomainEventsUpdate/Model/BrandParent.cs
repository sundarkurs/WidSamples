using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class BrandParent
    {
        public Guid Id { get; set; }
        public string BrandId { get; set; }
        public string ParentBrandId { get; set; }
    }
}
