using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Reflection;
using System.Threading;
using System.Resources;
using System.IO;

namespace BookScraper
{
    class MainHelper
    {
        SafeToPull sf;
        
        Timer t;
        public string wbURL;
        public Books CurrentBook;
        public HtmlDocument Document { get; set; }
        public string Chapter;

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
            this.CurrentBook = new Books();
            this.Document = new HtmlDocument();
        }

        public void loadPages()
        {
            WebsiteList lst = new WebsiteList();
            foreach (Books s in lst.BookList)
            {
                bool val = sf.isSafePull();
                if (!val)
                {
                    while (!val) { val = sf.isSafePull(); }
                    //break;
                }
                if (s.BookAddress != null)
                {
                    this.CurrentBook = s;

                    manageDownload();
                    //tStart();
                }
            }
        }

        public void loadPages(Books s)
        {
            bool val = sf.isSafePull();
            if (!val)
            {
                while (!val) { val = sf.isSafePull(); }
                //break;
            }
            if (s.BookAddress != null)
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
            Chapter = this.CurrentBook.BookAddress;
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
            WebScraper.StartScrape scrape = new WebScraper.StartScrape(this.CurrentBook.PageType, doc);

            //string myString = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\btth2.html");
            //HtmlDocument d = new HtmlDocument();
            //d.LoadHtml(myString);

            //WebScraper.StartScrape scrape = new WebScraper.StartScrape(this.CurrentBook.PageType, d);





            DatabaseWriter db = new DatabaseWriter(@"Server=localhost\SQLEXPRESS01; Database=Books; Trusted_Connection=true;");
            db.createSource(this.CurrentBook.AssociatedWebsite.WebsiteName, Websites.WuxiaWorld.WebsiteAddress);
            db.createBook(this.CurrentBook.BookName, this.CurrentBook.AssociatedWebsite.WebsiteName);
            db.createChapter(scrape.CurrentChapterName.Trim(), this.CurrentBook.BookName, scrape.BookContent, ProgramStatics.ChapterAddress, scrape.HTML);


            BookSaver saver = new BookSaver(this.CurrentBook.AssociatedWebsite.WebsiteName + "\\" + this.CurrentBook.BookName, scrape.CurrentChapterName.Trim());
            string s = scrape.BookContent;

            //s = replaceValues(s);
            //s.Trim();
            saver.saveBook(scrape.BookContent);


            if (string.IsNullOrEmpty(scrape.NextChapter))
            {
                ProgramStatics.NextBook = true;
                return "";
            }
            if (scrape.NextChapter.Equals("Page Not Found"))
            {
                ProgramStatics.NextBook = true;
                return "";
            }

            if(scrape.NextChapter.Equals(ProgramStatics.ChapterAddress) || scrape.NextChapter.Equals(ProgramStatics.PreviousChapter))
            {
                ProgramStatics.NextBook = true;
                return "";
            }
            ProgramStatics.PreviousChapter = ProgramStatics.ChapterAddress;
            ProgramStatics.ChapterAddress = scrape.NextChapter;
            System.Threading.Thread.Sleep(2000);
            return scrape.NextChapter;
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
