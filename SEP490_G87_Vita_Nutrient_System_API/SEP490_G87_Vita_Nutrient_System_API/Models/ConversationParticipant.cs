using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class ConversationParticipant
    {
        public int Id { get; set; }
        public int ConversationsId { get; set; }
        public int UserId { get; set; }
        public DateTime? AddedAt { get; set; }

        public virtual Conversation Conversations { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
