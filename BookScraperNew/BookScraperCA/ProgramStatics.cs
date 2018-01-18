using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookScraperCA
{
    public static class ProgramStatics
    {

        public static string Token = "secret";
        public static event Action NextBookEvent;
        public static string PreviousChapter { get; set; }
        public static string ChapterAddress { get; set; }
        private static bool _nextBook;
        public static bool NextBook
        {
            get
            {
                return _nextBook;
            }
            set
            {
                _nextBook = value;
                //NextBookEvent();
            }
        }
        
    }
}
