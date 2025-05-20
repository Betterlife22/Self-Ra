using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Conversation:BaseEntity
    {
        
        public bool IsGroup { get; set; }
        public DateTime? LastMessageAt { get; set; }
    }
}
