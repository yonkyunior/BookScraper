using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Websites
{
    abstract class HtmlIndexScraper : HtmlScraper
    {
        /// <summary>
        /// Stores all books associated with website {BookName, IndexAddress}
        /// </summary>
        public Dictionary<string,string> Books { get; set; }

        //public abstract Dictionary<string, string> returnBooks();
    }
}
