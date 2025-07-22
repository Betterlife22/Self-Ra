using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.BookModel
{
    public class BookViewModel
    {
        public string Id { get; set; }

        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Filepath { get; set; }
        public string? Format { get; set; }
        public TimeSpan TotalAudioDuration { get; set; }

    }
}
