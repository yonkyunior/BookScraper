using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Reflection;
using System.Threading;

namespace BookScraper
{
    class MainHelper
    {
        SafeToPull sf;
        
        Timer t;
        public string wbURL;
        public Models.Book CurrentBook;
        public HtmlDocument Document { get; set; }
        public string Chapter;

        public List<string> websites =new List<String>() { "NONE","WuxiaWorld", "XianXiaWorld", "GravityTales", "TranslationNations"};

        public event Action Ready;

        private bool _isReady;

        public bool IsReady
        {
            get
            {
                return _isReady;
            }
            set
            {
                _isReady = value;
                if (value)
                {
                    Ready();
                }
            }
        }
        
        private bool _canNavigate;

        public bool CanNavigate
        {
            get
            {
                return _canNavigate;
            }
            set
            {
                _canNavigate = value;
                if (value)
                {
                    Navigate();
                }
            }
        }

        public delegate void WebBrowserNavigate();
        public event WebBrowserNavigate Navigate;

        public MainHelper()
        {
            this.Ready += MainHelper_Ready;
            sf = new SafeToPull();
            this.CurrentBook = new Models.Book();
            this.Document = new HtmlDocument();
        }

        public void loadPages()
        {
            WebsiteList lst = new WebsiteList();
            foreach (Models.Book s in lst.BookList)
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

                    manageDownload();
                    //tStart();
                }
            }
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

                manageDownload();
                //tStart();
            }
        }

        private void tStart()
        {
            //t = new Timer(nextChpater, null, 10000, 10000);

            //RunTimer();
        }
        private void tStop()
        {
            t.Dispose();
        }



        private void manageDownload()
        {
            Chapter = this.CurrentBook.BookLastChapterAddress;
            ProgramStatics.ChapterAddress = Chapter;
            while (!string.IsNullOrEmpty(Chapter))
            {
                IsReady = false;
                if (Chapter.Equals(wbURL))
                {
                    continue;
                }
                //Uri u = new Uri(Chapter);
                //wbURL = Chapter.Clone() as string;
                this.CanNavigate = true;
                //wbBrowser.Navigate(u);
                //tStart();
                //Chapter = nextChpater();
                break;
                //System.Threading.Thread.Sleep(10000);
            }
        }


        private void nextChpater()
        {
            Chapter = "";
            //System.Threading.Thread.Sleep(10000);
            if (IsReady)
            {
                //object b = wbBrowser.Document;
                Chapter = prepareBook();
                IsReady = false;
                this.CanNavigate = true;
                //tStop();
            }
            //return s;

        }



        private string prepareBook()
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
            WebScraper.StartScrape scrape = new WebScraper.StartScrape( this.CurrentBook.SourceID, doc, this.CurrentBook.BookID);

            //Thread.Sleep(15000);

            string nextChapter = scrape.NextChapter;
            string previousChapter = scrape.PreviousChapter;
            int bookID = this.CurrentBook.BookID;

            DatabaseWriter dbWrite = new DatabaseWriter();
            dbWrite.BookID = bookID;

            //dbWrite.ChapterAddress = Chapter;
            dbWrite.ChapterAddress = ProgramStatics.ChapterAddress;

            dbWrite.ChapterHtml = scrape.HTML;
            dbWrite.ChapterName = scrape.CurrentChapterName;
            dbWrite.ChapterText = scrape.BookContent;
            dbWrite.PreviousChapterAddress = scrape.PreviousChapter;
            dbWrite.SaveChapter();

            Logging.Logging.LogMessage("", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Logs", "BookScraper_DBSaved",
                " BookID" + this.CurrentBook.BookID + "\r\n\r\n Book Next Chapter Address" + this.CurrentBook.NextChapterAddress + "\r\n\r\n Book Previous Chapter Address: " +
                this.CurrentBook.PreviousChapterAddress + "\r\n\r\n Book LastChapterAddress: " + this.CurrentBook.BookLastChapterAddress + "\r\n\r\n Book SourceID: " + this.CurrentBook.SourceID +
                "\r\n\r\n Chapter: " + this.Chapter + "\r\n\r\n DB Next Chapter Address: " + scrape.NextChapter + "\r\n\r\n DB Previous Chapter Address: " + scrape.PreviousChapter + "\r\n\r\n DB Chapter Name: " + scrape.CurrentChapterName);

            //DatabaseWriter db = new DatabaseWriter(@"Server=localhost\SQLEXPRESS; Database=Book; Trusted_Connection=true;");
            //db.createSource(this.CurrentBook.AssociatedWebsite.WebsiteName, Websites.WuxiaWorld.WebsiteAddress);
            //db.createBook(this.CurrentBook.BookName, this.CurrentBook.AssociatedWebsite.WebsiteName);
            //db.createChapter(scrape.CurrentChapterName, this.CurrentBook.BookName, scrape.BookContent, ProgramStatics.ChapterAddress, scrape.HTML);


            //BookSaver saver = new BookSaver(this.CurrentBook.AssociatedWebsite.WebsiteName + "\\" + this.CurrentBook.BookName, scrape.CurrentChapterName.Trim());



            BookSaver saver = new BookSaver(this.websites[this.CurrentBook.SourceID] + "\\" + this.CurrentBook.BookName, scrape.CurrentChapterName.Trim());
            string s = scrape.BookContent;

            //s = replaceValues(s);
            //s.Trim();
            //saver.saveBook(scrape.BookContent);

            /*
            if (string.IsNullOrEmpty(scrape.NextChapter))
                return "";
            if (scrape.NextChapter.Equals("Page Not Found"))
                return "";
                */
            if(scrape.NextChapter.Equals("Page Not Found") || scrape.NextChapter.Equals("Page Not Found/") || string.IsNullOrEmpty(scrape.NextChapter) || scrape.NextChapter.Equals(ProgramStatics.ChapterAddress) || scrape.NextChapter.Equals(ProgramStatics.PreviousChapter) || ChapterAlreadyExists(scrape.NextChapter))
            {
                ProgramStatics.NextBook = true;
                return "";
            }
            ProgramStatics.PreviousChapter = ProgramStatics.ChapterAddress;
            ProgramStatics.ChapterAddress = scrape.NextChapter;
            //System.Threading.Thread.Sleep(15000);
            return scrape.NextChapter;
        }

        private bool ChapterAlreadyExists(string nextChapter)
        {
            using (ReaderService.ReaderServiceSoapClient svc = new ReaderService.ReaderServiceSoapClient())
            {
                return svc.ChapterAlreadyExists(ProgramStatics.Token, nextChapter);
            }
        }

        private string replaceValues(string s)
        {

            s.Replace("Previous Chapter", " ");
            s.Replace("Next Chapter", " ");
            return s;
        }

        private void MainHelper_Ready()
        {
            nextChpater();
            CanNavigate = false;
        }
    }
}
