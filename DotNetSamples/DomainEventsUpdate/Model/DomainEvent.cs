using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class DomainEvent
    {
        public int Id { get; set; }
        public int EventType { get; set; }
        public DateTime TimestampUtc { get; set; }
        public Guid CommitId { get; set; }
        public int ItemType { get; set; }
        public Guid ItemId { get; set; }
        public string CorrelationId { get; set; }
        public string System { get; set; }
        public string Username { get; set; }
        public string Payload { get; set; }
        public string ExternalId { get; set; }
        public string EntityVersion { get; set; }
        public bool IsEarlyExtract { get; set; }
        public string BrandId { get; set; }
    }
}
