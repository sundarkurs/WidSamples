using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class Deltum
    {
        public Guid ItemId { get; set; }
        public int Id { get; set; }
        public int CommandType { get; set; }
        public int ItemType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Executed { get; set; }
        public string Payload { get; set; }
        public string BrandName { get; set; }
        public bool IsEarlyExtract { get; set; }
    }
}
