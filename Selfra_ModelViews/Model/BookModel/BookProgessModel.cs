using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.BookModel
{
    public class BookProgessModel
    {
        public int CurrentPage { get; set; }
        public TimeSpan CurrentPosition { get; set; }
        public bool IsFinished { get; set; }
    }
}
