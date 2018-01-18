using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookScraper.Models
{
    public class Source
    {
        public int SourceID { get; set; }
        public string SourceName { get; set; }
        public string SourceAddress { get; set; }
        public List<Book> Books { get; set; }
        public Source()
        {
            Books = new List<Book>();
        }
        public override string ToString()
        {
            return SourceName;
        }
    }
}
