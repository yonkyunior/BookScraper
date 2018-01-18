using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookScraperCA.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public int SourceID { get; set; }
        public string BookName { get; set; }
        public string BookLastChapterAddress { get; set; }
        public string BookIndexAddress { get; set; }
        public string NextChapterAddress { get; set; }
        public string PreviousChapterAddress { get; set; }
        public List<Chapter> Chapters { get; set; }
        public Book()
        {
            Chapters = new List<Chapter>();
        }
        public override string ToString()
        {
            return BookName;
        }
    }
}
