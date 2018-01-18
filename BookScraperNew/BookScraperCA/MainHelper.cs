using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Web;

namespace BookScraperCA
{
    public class MainHelper
    {
        public Models.Book CurrentBook { get; set; }
        public HtmlDocument Document { get; set; }
        SafeToPull sf;
        public string Chapter;



        public MainHelper()
        {
            sf = new SafeToPull();
            this.CurrentBook = new Models.Book();
            this.Document = new HtmlDocument();
        }


        public void loadPages(Models.Book s)
        {
            bool val = sf.isSafePull();
            if (!val)
            {
                while (!val) { val = sf.isSafePull(); }
                //break;
            }
            if (s.BookLastChapterAddress != null)
            {
                this.CurrentBook = s;

            }
        }




        public string prepareBook()
        {
            bool val = sf.isSafePull();
            if (!val)
            {
                while (!val) { val = sf.isSafePull(); }
                //break;
            }
            //string HTML = (wbBrowser.Document as mshtml.IHTMLDocument2).body.innerHTML;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(this.Document.DocumentNode.OuterHtml);

            this.Document = doc;

            //WebScraper.PageTypes p = WebScraper.PageTypes[this.CurrentBook.SourceID];
            WebScraper.StartScrape scrape = new WebScraper.StartScrape(this.CurrentBook.SourceID, doc, this.CurrentBook.BookID);

            //Thread.Sleep(15000);

            string nextChapter = scrape.NextChapter;
            string previousChapter = scrape.PreviousChapter;
            int bookID = this.CurrentBook.BookID;


            Console.WriteLine("(" + DateTime.Now + ") " + "Parsed About to Save to DB...");
            DatabaseWriter dbWrite = new DatabaseWriter();
            dbWrite.BookID = bookID;

            //dbWrite.ChapterAddress = Chapter;
            dbWrite.ChapterAddress = ProgramStatics.ChapterAddress;
            if (!string.IsNullOrEmpty(scrape.HTML) && !string.IsNullOrEmpty(scrape.BookContent))
            {
                if (scrape.BookContent.Length > 300)
                {
                    dbWrite.ChapterHtml = HttpUtility.HtmlDecode(scrape.HTML).Trim(); //scrape.HTML;
                    dbWrite.ChapterName = HttpUtility.HtmlDecode(scrape.CurrentChapterName).Trim(); //scrape.CurrentChapterName;
                    dbWrite.ChapterText = HttpUtility.HtmlDecode(scrape.BookContent).Trim(); //scrape.BookContent;
                    dbWrite.PreviousChapterAddress = scrape.PreviousChapter;
                    dbWrite.SaveChapter();

                    Console.WriteLine("(" + DateTime.Now + ") " + "Saved Parse... Prograssing to next Book\\Chapter...");

                    int bookid = scrape.BookID;

                    Logging.Logging.LogMessage("", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Logs", "BookScraper_DBSaved",
                        " BookID" + this.CurrentBook.BookID + "\r\n\r\n Book Next Chapter Address" + this.CurrentBook.NextChapterAddress + "\r\n\r\n Book Previous Chapter Address: " +
                        this.CurrentBook.PreviousChapterAddress + "\r\n\r\n Book LastChapterAddress: " + this.CurrentBook.BookLastChapterAddress + "\r\n\r\n Book SourceID: " + this.CurrentBook.SourceID +
                        "\r\n\r\n Chapter: " + this.Chapter + "\r\n\r\n DB Next Chapter Address: " + scrape.NextChapter + "\r\n\r\n DB Previous Chapter Address: " + scrape.PreviousChapter + "\r\n\r\n DB Chapter Name: " + scrape.CurrentChapterName);
                }
            }

            //scrape.NextChapter = "http://moonbunnycafe.com/dragons-bloodline/db-ch17/";

            //BookSaver saver = new BookSaver(this.CurrentBook.AssociatedWebsite.WebsiteName + "\\" + this.CurrentBook.BookName, scrape.CurrentChapterName.Trim());



            //BookSaver saver = new BookSaver(this.websites[this.CurrentBook.SourceID] + "\\" + this.CurrentBook.BookName, scrape.CurrentChapterName.Trim());
            string s = scrape.BookContent;

            string index = "";
            using (ReaderService.ReaderServiceSoapClient svc = new ReaderService.ReaderServiceSoapClient())
            {
                index = svc.getBookIndexAddress(ProgramStatics.Token, this.CurrentBook.BookID);
                
            }

                if (scrape.NextChapter.Equals("Page Not Found") || scrape.NextChapter.Equals("Page Not Found/") ||
                    string.IsNullOrEmpty(scrape.NextChapter) || scrape.NextChapter.Equals(ProgramStatics.ChapterAddress) ||
                    scrape.NextChapter.Equals(ProgramStatics.PreviousChapter) || ChapterAlreadyExists(scrape.NextChapter) ||
                    scrape.NextChapter.Equals(index, StringComparison.OrdinalIgnoreCase)
                    )
                {
                    ProgramStatics.NextBook = true;
                    return "";
                }
            ProgramStatics.PreviousChapter = ProgramStatics.ChapterAddress;
            ProgramStatics.ChapterAddress = scrape.NextChapter;
            this.Chapter = scrape.NextChapter;
            return scrape.NextChapter;
        }

        private bool ChapterAlreadyExists(string nextChapter)
        {
            using (WebScraper.ReaderService.ReaderServiceSoapClient svc = new WebScraper.ReaderService.ReaderServiceSoapClient())
            {
                return svc.ChapterAlreadyExists(ProgramStatics.Token, nextChapter);
            }
        }
    }
}
