using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.ForumModel
{
    public class UpdateForumModel
    {
        public string? ForumCommentId { get; set; }
        public string? PostId { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
    }
}
