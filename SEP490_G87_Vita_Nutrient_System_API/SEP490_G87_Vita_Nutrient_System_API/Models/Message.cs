using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int ConversationsId { get; set; }
        public int SenderId { get; set; }
        public string? Content { get; set; }
        public DateTime? SentAt { get; set; }
        public bool? IsRead { get; set; }

        public virtual Conversation Conversations { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
