using System;
using System.Collections.Generic;

namespace DomainEventsUpdate.Model
{
    public partial class AgileUploadRequest
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Status { get; set; }
        public string StatusMessage { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}
