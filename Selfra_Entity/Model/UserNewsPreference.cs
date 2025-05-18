using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Entity.Model
{
    public class UserNewsPreference :BaseEntity
    {
        public string UserId { get; set; }
        public int NewTagId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
