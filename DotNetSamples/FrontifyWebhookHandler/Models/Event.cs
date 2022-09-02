using System;
using System.Collections.Generic;

namespace FrontifyWebhookHandler.Models
{
    public class Root
    {
        public Event @event { get; set; }
        public Meta meta { get; set; }
    }

    public class Event
    {
        public string action { get; set; }
        public DateTime occurredAt { get; set; }
        public DateTime processedAt { get; set; }
        public Sender sender { get; set; }
        public List<Payload> payload { get; set; }
    }

    public class Meta
    {
        public string accountId { get; set; }
        public string webhookId { get; set; }
        public bool isSampleEvent { get; set; }
    }

    public class Payload
    {
        public string assetId { get; set; }
    }

    public class Sender
    {
        public string id { get; set; }
    }


}
