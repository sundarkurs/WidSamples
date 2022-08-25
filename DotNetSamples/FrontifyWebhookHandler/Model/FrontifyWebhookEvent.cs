using System;
using System.Collections.Generic;

namespace FrontifyWebhookHandler.Model
{
    public partial class FrontifyWebhookEvent
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime? OccurredAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string Payload { get; set; }
    }
}
