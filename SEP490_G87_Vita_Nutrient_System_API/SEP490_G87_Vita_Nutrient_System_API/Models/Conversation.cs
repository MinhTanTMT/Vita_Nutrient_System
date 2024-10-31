using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            ConversationParticipants = new HashSet<ConversationParticipant>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsGroup { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
