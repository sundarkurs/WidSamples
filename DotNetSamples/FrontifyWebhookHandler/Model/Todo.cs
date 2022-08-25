using System;
using System.Collections.Generic;

namespace FrontifyWebhookHandler.Model
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
