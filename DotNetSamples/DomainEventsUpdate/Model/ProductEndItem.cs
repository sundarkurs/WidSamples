using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class ProductEndItem
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Skunumber { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string ProductType { get; set; }
        public int? ProductCode { get; set; }
        public string ExternalCode { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string ColorHexCode { get; set; }
        public string BatterySize { get; set; }
        public string Alnumber { get; set; }
        public string Eccn { get; set; }
        public string Type { get; set; }
        public string ImageName { get; set; }
        public string Erpname { get; set; }
        public string State { get; set; }
        public string ProductId { get; set; }
        public string PrivateLabel { get; set; }
        public string Class { get; set; }
        public string Weight { get; set; }
        public string Rmstatus { get; set; }
        public Guid ExternalId { get; set; }
        public string ExternalProjectId { get; set; }
        public string PermanentKey { get; set; }
        public bool IsEarlyExtract { get; set; }

        public virtual Product Parent { get; set; }
    }
}
