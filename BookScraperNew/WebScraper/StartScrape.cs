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
        None, WuxiaBook, XianXiaWorld, GravityTales, TranslationNation, MoonBunnyCafe, WalkTheJiangHu, TaffyTranslations, NovelSaga, BlueSilver, Default
    }
    public class StartScrape
    {
        Dictionary<PageTypes, Websites.HtmlScraper> websites;
        public Dictionary<string, string> bookList;
        public string BookContent { get; set; }

        public string HTML { get; set; }

        public string NextChapter { get; set; }

        public string PreviousChapter { get; set; }

        public string CurrentChapterName { get; set; }
        public int BookID { get; set; }


        /// <summary>
        /// scrapes the desired data from a Page
        /// </summary>
        /// <param name="webType">Determines whether to scrape an Index or a book</param>
        /// <param name="doc">The HTML Document that will be scraped</param>
        //public StartScrape(PageTypes webType, HtmlDocument doc)
        public StartScrape(int webType, HtmlDocument doc, int bookid)
        {
            this.BookID = bookid;
            this.websites = new Dictionary<PageTypes, Websites.HtmlScraper>();
            WebList();

            if((PageTypes)webType == PageTypes.WuxiaBook)
            {
                BookScrape(doc, PageTypes.WuxiaBook);
            }
            else if((PageTypes)webType == PageTypes.XianXiaWorld)
            {
                BookScrape(doc, PageTypes.XianXiaWorld);
            }
            else if ((PageTypes)webType == PageTypes.GravityTales)
            {
                BookScrape(doc, PageTypes.GravityTales);
            }
            else if ((PageTypes)webType == PageTypes.TranslationNation)
            {
                BookScrape(doc, PageTypes.TranslationNation);
            }
            else if ((PageTypes)webType == PageTypes.MoonBunnyCafe)
            {
                BookScrape(doc, PageTypes.MoonBunnyCafe);
            }
            else if ((PageTypes)webType == PageTypes.WalkTheJiangHu)
            {
                BookScrape(doc, PageTypes.WalkTheJiangHu);
            }
            else if ((PageTypes)webType == PageTypes.TaffyTranslations)
            {
                BookScrape(doc, PageTypes.TaffyTranslations);
            }
            else if ((PageTypes)webType == PageTypes.NovelSaga)
            {
                BookScrape(doc, PageTypes.NovelSaga);
            }
            else if ((PageTypes)webType == PageTypes.BlueSilver)
            {
                BookScrape(doc, PageTypes.BlueSilver);
            }
            else 
            {
                BookScrape(doc, PageTypes.Default);
            }

            /*
            if (webType == PageTypes.WuxiaIndex)
            {
                IndexScrape(doc, webType);
            }
            else
            {
                BookScrape(doc, webType);
            }
            */

        }

        private void BookScrape(HtmlDocument doc, PageTypes webType)
        {
            Websites.HtmlBookScraper Book = this.websites[webType] as Websites.HtmlBookScraper;
            Book.OriginalDoc = doc;
            Book.BookID = this.BookID;

            this.CurrentChapterName = Book.GetCurrentChapterName();
            Book.ParseHtml();
            string s = Book.GetNextChapter();
            string p = Book.GetPreviousChapter();

            this.NextChapter = s;
            this.PreviousChapter = p;
            //Book.ParseHtml();
            this.BookContent = Book.Contents;
            this.HTML = Book.HTML;
            //this.CurrentChapterName = Book.GetCurrentChapterName();
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
            //this.websites.Add(PageTypes.WuxiaIndex, new Websites.WuxiaWorld.WuxiaWorldIndexScraper());
            this.websites.Add(PageTypes.WuxiaBook, new Websites.WuxiaWorld.WuxiaWorldBookScraper());
            this.websites.Add(PageTypes.XianXiaWorld, new Websites.XianXiaWorld.XianXiaWorldBookScraper());
            this.websites.Add(PageTypes.GravityTales, new Websites.GravityTales.GravityTalesBookScraper());
            this.websites.Add(PageTypes.TranslationNation, new Websites.TranslationNation.TranslationNationBookScraper());
            this.websites.Add(PageTypes.MoonBunnyCafe, new Websites.MoonBunnyCafe.MoonBunnyCafeBookScraper());
            this.websites.Add(PageTypes.WalkTheJiangHu, new Websites.WalkTheJianghu.WalkTheJianghuBookScraper());
            this.websites.Add(PageTypes.TaffyTranslations, new Websites.TaffyTranslations.TaffyTranslationsBookScraper());
            this.websites.Add(PageTypes.NovelSaga, new Websites.NovelSaga.NovelSagaBookScraper());
            this.websites.Add(PageTypes.BlueSilver, new Websites.BlueSilverTranslations.BlueSilverHtmlBookScraper());
            this.websites.Add(PageTypes.Default, new Websites.Default.DefaultHtmlBookScraper());

        }
    }
}
