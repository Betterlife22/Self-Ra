using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class ReadingProgress:BaseEntity
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int CurrentPage { get; set; }
        public TimeSpan CurrentPosition { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsFinished { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}
