using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class Document
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string DocumentInternalId { get; set; }
        public string DocumentId { get; set; }
        public Guid ExternalId { get; set; }
        public string ExternalProjectId { get; set; }
        public string Version { get; set; }
        public string TypeId { get; set; }
        public string Language { get; set; }
        public string Path { get; set; }
        public string Revision { get; set; }
        public string SheetNo { get; set; }
        public string FilePositionNo { get; set; }
        public string PermanentKey { get; set; }
        public bool IsEarlyExtract { get; set; }

        public virtual Product Parent { get; set; }
    }
}
