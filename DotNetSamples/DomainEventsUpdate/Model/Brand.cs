using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class Brand
    {
        public Brand()
        {
            Families = new HashSet<Family>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Logo { get; set; }
        public Guid ExternalId { get; set; }
        public string ExternalProjectId { get; set; }
        public string PermanentKey { get; set; }

        public virtual ICollection<Family> Families { get; set; }
    }
}
