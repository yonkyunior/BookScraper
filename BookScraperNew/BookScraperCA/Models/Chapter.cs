using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookScraperCA.Models
{
    public class Chapter
    {
        public int ChapterID { get; set; }
        public int PreviousChapterID { get; set; }
        public string ChapterText { get; set; }
        public string ChapterHTML { get; set; }
        public string ChapterAddress { get; set; }
        public string ChapterName { get; set; }
        public int BookID { get; set; }

        public override string ToString()
        {
            return ChapterName;
        }
    }
}
