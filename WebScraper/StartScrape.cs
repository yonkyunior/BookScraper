using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraper
{
    public enum PageTypes
    {
        WuxiaIndex, WuxiaBook
    }
    public class StartScrape
    {
        Dictionary<PageTypes, Websites.HtmlScraper> websites;
        public Dictionary<string, string> bookList;
        public string BookContent { get; set; }

        public string HTML { get; set; }

        public string NextChapter { get; set; }

        public string CurrentChapterName { get; set; }


        /// <summary>
        /// scrapes the desired data from a Page
        /// </summary>
        /// <param name="webType">Determines whether to scrape an Index or a book</param>
        /// <param name="doc">The HTML Document that will be scraped</param>
        public StartScrape(PageTypes webType, HtmlDocument doc)
        {
            this.websites = new Dictionary<PageTypes, Websites.HtmlScraper>();
            WebList();

            if (webType == PageTypes.WuxiaIndex)
            {
                IndexScrape(doc, webType);
            }
            else
            {
                BookScrape(doc, webType);
            }
            

        }

        private void BookScrape(HtmlDocument doc, PageTypes webType)
        {
            Websites.HtmlBookScraper Book = this.websites[webType] as Websites.HtmlBookScraper;
            Book.OriginalDoc = doc;
            this.NextChapter = Book.GetNextChapter();
            Book.ParseHtml();
            this.BookContent = Book.Contents;
            this.HTML = Book.HTML;
            this.CurrentChapterName = Book.GetCurrentChapterName();
        }

        private void IndexScrape(HtmlDocument doc, PageTypes webType)
        {
            this.bookList = new Dictionary<string, string>();
            Websites.HtmlIndexScraper Index = this.websites[webType] as Websites.HtmlIndexScraper;
            Index.OriginalDoc = doc;
            Index.ParseHtml();
            this.bookList = Index.Books;
        }
        
        private void WebList()
        {
            this.websites.Add(PageTypes.WuxiaIndex, new Websites.WuxiaWorld.WuxiaWorldIndexScraper());
            this.websites.Add(PageTypes.WuxiaBook, new Websites.WuxiaWorld.WuxiaWorldBookScraper());
        }
    }
}
