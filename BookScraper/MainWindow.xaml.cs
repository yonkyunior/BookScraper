using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using System.Reflection;
using System.Threading;
namespace BookScraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public HtmlDocument Document { get; set; }
        SafeToPull sf;
        MainHelper helper;
        WebsiteList wb;
        int index;

        private bool _isReady;

        event Action Ready;

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

        

        Thread tr;

        public MainWindow()
        {
            InitializeComponent();
            sf = new SafeToPull();
            dynamic activeX = this.wbBrowser.GetType().InvokeMember("ActiveXInstance",
                    BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, this.wbBrowser, new object[] { });

            activeX.Silent = true;

            wb = new WebsiteList();
            index = 0;
            //ProgramStatics.NextBookEvent += ProgramStatics_NextBookEvent;

            //this.Ready += MainWindow_Ready;

            //helper = new MainHelper();
            //helper.Navigate += Helper_Navigate;
            //helper.loadPages();

            //tr = new Thread(helper.loadPages);
            //tr.Start();

            //this.CurrentBook = new Books();
            this.Document = new HtmlDocument();



            //Document = new HtmlDocument();
            //Document.Load(@"C:\Users\jd\Desktop\WuxiaHome.html");
            //WebScraper.StartScrape scrape = new WebScraper.StartScrape(WebScraper.PageTypes.WuxiaIndex,this.Document);

            //Document = new HtmlDocument();
            //Document.Load(@"C:\Users\jd\Desktop\CoilingDragon2.html");
            //Document.Load(@"C:\Users\jd\Desktop\CoilingDragon.html");
            //Document.Load(@"C:\Users\jd\Desktop\MGA245.html");
            //WebScraper.StartScrape scrape1 = new WebScraper.StartScrape(WebScraper.PageTypes.WuxiaBook, this.Document);

            //loadPages();
        }

        //private void ProgramStatics_NextBookEvent()
        //{
        //    if (ProgramStatics.NextBook)
        //    {
        //        index++;
        //        helper.Navigate += Helper_Navigate;
        //        helper.loadPages(wb.BookList[index]);
        //        ProgramStatics.NextBook = false;
        //    }
        //    return;
        //}

        private void Helper_Navigate()
        {
            this.IsReady = true;
            //string s = helper.Chapter;
        }

        private void MainWindow_Ready()
        {
            if (ProgramStatics.NextBook)
            {
                Thread.Sleep(10000);
                ProgramStatics.NextBook = false;
                index++;
                helper.Navigate += Helper_Navigate;
                helper.loadPages(wb.BookList[index]);
                //ProgramStatics.NextBook = false;
            }
            else
            {
                string s = helper.Chapter;
                Uri u = new Uri(s);
                helper.wbURL = helper.Chapter.Clone() as string;
                this.wbBrowser.Navigate(u);
                this.IsReady = false;
            }
        }

        private void wbBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            
            string HTML = (wbBrowser.Document as mshtml.IHTMLDocument2).body.innerHTML;
            HtmlDocument doc = new HtmlDocument();
            if (string.IsNullOrEmpty(HTML))
            {
                return;
            }
            doc.LoadHtml(HTML);
            HtmlNode node = doc.GetElementbyId("challenge-form");
            if (string.IsNullOrEmpty(HTML) || node != null)
                return;
            helper.Document.LoadHtml(HTML);
            //helper.Document = this.Document;
            helper.IsReady = true;
            //nextChpater(null);
            //tStart();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.Ready += MainWindow_Ready;

            helper = new MainHelper();
            helper.Navigate += Helper_Navigate;
            helper.loadPages(wb.BookList[index]);
            ProgramStatics.ChapterAddress = wb.BookList[index].BookAddress;
        }
    }
}
