using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BookScraperCA
{
    public class ProgramHome
    {

        MainHelper helper;
        WebsiteList wb;
        string NextChapter;
        int index;
        SafeToPull sf;
        bool Loop { get; set; }

        Models.Book CurrentBook { get; set; }

        public ProgramHome()
        {
            //Initialize();

            //return;

            sf = new SafeToPull();

            System.Threading.Thread thdBrowser = new System.Threading.Thread(Initialize);
            thdBrowser.SetApartmentState(System.Threading.ApartmentState.STA);
            thdBrowser.Start();

            //HideScriptErrors();

        }

        public static string DownloadPage(string url)
        {
            using (WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(url);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (StreamReader rd = new StreamReader(ms))
                    {
                        return rd.ReadToEnd();
                    }
                }
                //return Encoding.Default.GetString(data);
            }
        }


        private void Initialize()
        {
            //frm = new Form();


            //wbBrowser = new System.Windows.Forms.WebBrowser();
            //wbBrowser.Dock = DockStyle.Fill;

            //frm.Controls.Add(wbBrowser);

            //System.Windows.Forms.Application.Run(frm);


            wb = new WebsiteList();

            index = 0;
            Loop = true;
            //beginLoop();
            loop();

        }

        private void loop()
        {
            
            string path = beginLoop();
            LoadCompleted(path);
            while(Loop)
            {
                if (ProgramStatics.NextBook)
                {

                    ProgramStatics.NextBook = false;
                    index++;
                    string s = beginLoop();
                    if (string.IsNullOrEmpty(s))
                        return;
                    else
                        LoadCompleted(s);
                }
                else
                {

                    Console.WriteLine("\r\n(" + DateTime.Now + ") " + "Advancing to next Chapter in " + this.CurrentBook.BookName + ": ChapterAddress: " + this.NextChapter + "...");
                    string s = this.NextChapter;
                    Uri u = new Uri(s);
                    LoadCompleted(s);
                }
            }
        }

        public void LoadCompleted(string url)
        {
            while(!sf.isSafePull())
            {
                
            }
            try {
                //Thread.Sleep(30000);

                helper = null;

                helper = new MainHelper();
                helper.loadPages(CurrentBook);

                string HTML = DownloadPage(url);



                HtmlDocument doc = new HtmlDocument();
                if (string.IsNullOrEmpty(HTML))
                {
                    return;
                }

                doc.LoadHtml(HTML);
                HtmlDocument doc2 = new HtmlDocument();
                doc2.LoadHtml(doc.DocumentNode.ChildNodes["html"].InnerHtml);

                helper.Document.LoadHtml(doc2.DocumentNode.InnerHtml);

                Logging.Logging.LogMessage("", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Logs", "BookScraper_ChapterScraped",
                    " BookID" + this.CurrentBook.BookID + "\r\n\r\n Book Next Chapter Address" + this.CurrentBook.NextChapterAddress + "\r\n\r\n Book Previous Chapter Address: " +
                    this.CurrentBook.PreviousChapterAddress + "\r\n\r\n Book LastChapterAddress: " + this.CurrentBook.BookLastChapterAddress +
                    "\r\n\r\n Book SourceID: " + this.CurrentBook.SourceID + "\r\n\r\n Chapter HTML: " + HTML);

                Console.WriteLine("(" + DateTime.Now + ") " + "Grabbed HTML About to Parse...");



                this.NextChapter = this.helper.prepareBook();

                //Next();
            }catch(WebException we)
            {
                ProgramStatics.NextBook = true;
                //Next();
            }
        }

        private void Next()
        {
            if (ProgramStatics.NextBook)
            {

                ProgramStatics.NextBook = false;
                index++;
                beginLoop();
            }
            else
            {

                Console.WriteLine("\r\n(" + DateTime.Now + ") " + "Advancing to next Chapter in " + this.CurrentBook.BookName + ": ChapterAddress: " + this.NextChapter + "...");
                string s = this.NextChapter;
                Uri u = new Uri(s);
                LoadCompleted(s);
            }
        }



        public string beginLoop()
        {

            try
            {

                this.CurrentBook = null;

                this.CurrentBook = wb.BookList[index];

                Console.WriteLine("\r\n(" + DateTime.Now + ") " + "Advancing to next Book " + this.CurrentBook.BookName + ": ChapterAddress: " + this.CurrentBook.BookLastChapterAddress + "...");

                ProgramStatics.ChapterAddress = this.CurrentBook.BookLastChapterAddress;
                Uri u = new Uri(CurrentBook.BookLastChapterAddress);
                //u = new Uri("https://www.google.com");

                this.NextChapter = string.Empty;
                //LoadCompleted(CurrentBook.BookLastChapterAddress);
                return CurrentBook.BookLastChapterAddress;
            }catch(ArgumentOutOfRangeException aoore)
            {
                this.Loop = false;
                return null;
            }
            catch(NullReferenceException nre)
            {
                return null;
            }
        }
    }
}
