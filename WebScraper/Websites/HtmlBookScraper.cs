using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Websites
{
    abstract class HtmlBookScraper : HtmlScraper
    {
        /// <summary>
        /// Stores a Book's Chapter {ChapterNumber, ChapterAddress}
        /// </summary>
        //public KeyValuePair<int, string> ChapterInfo { get; set; }
        ///// <summary>
        ///// stores the address to chapter
        ///// </summary>
        //public string ChapterAddress { get; set; }
        public string ChapterAddress { get; set; }
        public string  HTML { get; set; }
        public string  Contents { get; set; }

        /// <summary>
        /// Gets the address of the next chapter to be parsed
        /// </summary>
        /// <returns>Returns the Address of the next Chapter, if no Chapter returns 'Page Not Found'</returns>
        public abstract string GetNextChapter();

        ///// <summary>
        ///// stores the chapter number
        ///// </summary>
        //public int ChapterNumber { get; set; }

        /// <summary>
        /// Gets the Name of the current Chapter Being Parsed
        /// </summary>
        /// <returns>Returns The Current Chapter Name</returns>
        public abstract string GetCurrentChapterName();
    }
}
