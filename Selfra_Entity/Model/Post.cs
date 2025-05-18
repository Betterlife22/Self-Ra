using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class Post:BaseEntity
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
      
        public bool IsDeleted { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
