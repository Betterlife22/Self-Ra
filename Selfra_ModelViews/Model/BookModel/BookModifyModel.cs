using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_ModelViews.Model.BookModel
{
    public class BookModifyModel
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public IFormFile? Filepath { get; set; }
        public string? Format { get; set; }
        public TimeSpan TotalAudioDuration { get; set; }

    }
}
