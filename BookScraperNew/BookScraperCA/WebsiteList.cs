using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookScraperCA.Models;
using BookScraperCA.ReaderService;

namespace BookScraperCA
{
    public class Websites
    {
        private Websites(string name, string address) { WebsiteName = name; WebsiteAddress = address; }

        public string WebsiteAddress { get; set; }
        public string WebsiteName { get; set; }

        public static Websites WuxiaWorld { get { return new Websites("WuxiaWorld","http://www.WuxiaWorld.com"); } }
        
    }

    #region old
    /*
    public class Books
    {
        private Books(string bookName, string value, Websites web, WebScraper.PageTypes pageType) { BookName = bookName; BookAddress = value; AssociatedWebsite = web; PageType = pageType; }

        public Books()
        {
        }
        public string BookName { get; set; }
        public string BookAddress { get; set; }
        public WebScraper.PageTypes PageType { get; set; }
        public Websites AssociatedWebsite { get; set; }


        #region WuxiaWorld books
        public static Books CoilingDragon
        { get { return new Books("Coiling Dragon", "http://www.wuxiaworld.com/cdindex-html/rwx-afterword/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books MartialGodAsura
        { get { return new Books("Martial God Asura", "http://www.wuxiaworld.com/mga-index/mga-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books IShallSealTheHeavens
        { get { return new Books("I Shall Seal The Heavens", "http://www.wuxiaworld.com/issth-index/issth-book-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books AgainstTheGods
        { get { return new Books("Against The Gods", "http://www.wuxiaworld.com/atg-index/prologue/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books ChildOfLight
        { get { return new Books("Child Of Light", "http://www.wuxiaworld.com/col-index/col-volume-4-chapter-17/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books GateOfRevelation
        { get { return new Books("Gate Of Revelation", "http://www.wuxiaworld.com/gor-index/gor-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books PerfectWorld
        { get { return new Books("Perfect World", "http://www.wuxiaworld.com/pw-index/pw-chapter-150/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SkyfireAvenue
        { get { return new Books("Skyfire Avenue", "http://www.wuxiaworld.com/sfl-index/skyfire-lane-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books TerrorInfinity
        { get { return new Books("Terror Infinity", "http://www.wuxiaworld.com/ti-index/ti-vol-15-chapter-9-2/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books UpgradeSpecialistInAnotherWorld
        { get { return new Books("Upgrade Specialist In Another World", "http://www.wuxiaworld.com/usaw-index/usaw-prologue/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books WuDongQianKun
        { get { return new Books("Wu Dong Qian Kun", "http://www.wuxiaworld.com/wdqk-index/wdqk-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books DesolateEra
        { get { return new Books("Desolate Era", "http://www.wuxiaworld.com/desolate-era-index/de-book-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books HeavenlyJewelChange
        { get { return new Books("Heavenly Jewel Change", "http://www.wuxiaworld.com/hjc-index/hjc-chapter-1-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books RenegadeImmortal
        { get { return new Books("Renegade Immortal", "http://www.wuxiaworld.com/renegade-index/renegade-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SovereignOfTheThreeRealms
        { get { return new Books("Sovereign of the Three Realms", "http://www.wuxiaworld.com/sotr-index/sotr-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books TalesOfDeamonsAndGods
        { get { return new Books("Tales of Deamons and Gods", "http://www.wuxiaworld.com/tdg-index/tdg-chapter-314/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books TheGreatRuler
        { get { return new Books("The Great Ruler", "http://www.wuxiaworld.com/tgr-index/tgr-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books WarlockOfTheMagusWorld
        { get { return new Books("Warlock of the Magus World", "http://www.wuxiaworld.com/wmw-index/wmw-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SevenKillers
        { get { return new Books("Seven Killers", "http://www.wuxiaworld.com/master-index/7-killers-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books DragonKingWithSevenStars
        { get { return new Books("Dragon King with Seven Stars", "http://www.wuxiaworld.com/master-index/dkwss-chapter-25/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books HerosShedNoTears
        { get { return new Books("Heros Shed no Tears", "http://www.wuxiaworld.com/master-index/prologue/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books HorizonBrightmoonSabre
        { get { return new Books("Horizon, Bright Moon, Sabre", "http://www.wuxiaworld.com/tymyd-index/chapter-24/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        //public static Books StellarTransformations
        //{ get { return new Books("Stellar Transformations", "http://www.wuxiaworld.com/st-index/st-book-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SpiritRealm
        { get { return new Books("Spirit Realm", "http://www.wuxiaworld.com/sr-index/sr-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books BattleThroughTheHeavens
        { get { return new Books("Battle Through The Heavens", "http://www.wuxiaworld.com/btth-index/btth-chapter-3/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        #endregion
    }
    */
    #endregion
    public class WebsiteList
    {
        public List<Models.Book> BookList;
        
        public WebsiteList()
        {

            //Models.Book bk = new Models.Book() { BookID = 120, BookIndexAddress = "http://moonbunnycafe.com/purple-river/", BookLastChapterAddress = "http://moonbunnycafe.com/purple-river/purple-river-chapter-01-part-01/", BookName = "Purple River", SourceID = 5 };
            //Models.Book bk = new Models.Book() { BookID = 114, BookIndexAddress = "http://moonbunnycafe.com/my-disciple-died-yet-again/", BookLastChapterAddress = "http://moonbunnycafe.com/my-disciple-died-yet-again/disciple-chapter-149/", BookName = "My Disciple Died Yet Again", SourceID = 5 };
            //Models.Book bk = new Models.Book() { BookID = 70, BookIndexAddress = "http://www.translationnations.com/translations/swallowed-star/", BookLastChapterAddress = "http://www.translationnations.com/?p=2867/", BookName = "SWALLOWED STAR", SourceID = 4 };
            //Models.Book bk = new Models.Book() { BookID = 96, BookIndexAddress = "http://moonbunnycafe.com/happy-peach/", BookLastChapterAddress = "http://moonbunnycafe.com/happy-peach/hp-ch1/", BookName = "Happy Peach (18+)", SourceID = 5 };
            //Models.Book bk = new Models.Book() { BookID = 140, BookIndexAddress = "http://www.novelsaga.com/martial-god-space-index/", BookLastChapterAddress = "http://www.novelsaga.com/319-2/", BookName = "Martial God Space", SourceID = 8 };
            //Models.Book bk = new Models.Book() { BookID = 89, BookIndexAddress = "http://moonbunnycafe.com/dragons-bloodline/", BookLastChapterAddress = "http://moonbunnycafe.com/dragons-bloodline/db-ch78/", BookName = "Dragon’s Bloodline", SourceID = 5 };
            //Models.Book bk = new Models.Book() { BookID = 93, BookIndexAddress = "http://moonbunnycafe.com/genius-doctor-black-belly-miss/", BookLastChapterAddress = "http://moonbunnycafe.com/genius-doctor-black-belly-miss/gdbbm-chapter-469/", BookName = "Genius Doctor : Black Belly Miss", SourceID = 5 };
            //Models.Book bk = new Models.Book() { BookID = 47, BookIndexAddress = "http://gravitytales.com/novel/true-martial-world", BookLastChapterAddress = "http://gravitytales.com/novel/true-martial-world/tmw-chapter-748", BookName = "True Martial World ", SourceID = 3 };
            //Models.Book bk = new Models.Book() { BookID = 54, BookIndexAddress = "http://gravitytales.com/novel/martial-world", BookLastChapterAddress = "http://gravitytales.com/novel/martial-world/mw-chapter-538", BookName = "Martial World ", SourceID = 3 };
            //Models.Book bk = new Models.Book() { BookID = 46, BookIndexAddress = "http://gravitytales.com/novel/the-nine-cauldrons", BookLastChapterAddress = "http://gravitytales.com/novel/the-nine-cauldrons/tnc-chapter-284", BookName = "The Nine Cauldrons", SourceID = 3 };
            //Models.Book bk = new Models.Book() { BookID = 62, BookIndexAddress = "http://gravitytales.com/novel/blue-phoenix", BookLastChapterAddress = "http://gravitytales.com/novel/blue-phoenix/bp-chapter-448", BookName = "Blue Phoenix", SourceID = 3 };
            //Models.Book bk = new Models.Book() { BookID = 139, BookIndexAddress = "https://walkthejianghu.com/ttnh-index/", BookLastChapterAddress = "https://walkthejianghu.com/chapter-220/", BookName = "Transcending The Nine Heavens", SourceID = 6 };
            //Models.Book bk = new Models.Book() { BookID = 20, BookIndexAddress = "http://www.wuxiaworld.com/usaw-index/", BookLastChapterAddress = "http://www.wuxiaworld.com/usaw-index/usaw-book-2-chapter-132/", BookName = "Upgrade Specialist In Another World", SourceID = 1 };
            //Models.Book bk = new Models.Book() { BookID = 151, BookIndexAddress = "https://bluesilvertranslations.wordpress.com/chapter-list/", BookLastChapterAddress = "https://bluesilvertranslations.wordpress.com/2014/12/10/001-douluo-continent-otherworldly-tang-san/", BookName = "Douluo Dalu", SourceID = 9 };
            //Models.Book bk = new Models.Book() { BookID = 152, BookIndexAddress = "http://volaretranslations.com/age-of-lazurite-tower-of-glass/", BookLastChapterAddress = "http://volaretranslations.com/age-of-lazurite-tower-of-glass/altg-prologue/", BookName = "Age of Lazurite, Tower of Glass", SourceID = 10 };
            //Models.Book bk = new Models.Book() { BookID = 153, BookIndexAddress = "http://www.radianttranslations.com/battle-frenzy/", BookLastChapterAddress = "http://www.radianttranslations.com/battle-frenzy/bf-chapter-5/", BookName = "Battle Frenzy", SourceID = 11 };
            //Models.Book bk = new Models.Book() { BookID = 71, BookIndexAddress = "http://www.translationnations.com/translations/limitless-sword-god/", BookLastChapterAddress = "http://www.translationnations.com/translations/limitless-sword-god/lsg-chapter-0090/", BookName = "Limitless Sword God", SourceID = 4 };
            Models.Book bk = new Models.Book() { BookID = 3, BookIndexAddress = "http://www.wuxiaworld.com/issth-index/", BookLastChapterAddress = "http://www.wuxiaworld.com/issth-index/issth-book-10-chapter-1600/", BookName = "I Shall Seal The Heavens", SourceID = 1 };

            BookList = new List<Models.Book>();
            BookList.Add(bk);
            return;

            BookList = new List<Models.Book>();

            Books = new List<Models.Book>();
            Sources = GenerateIndex();


            List<Models.Book> val = new List<Models.Book>();
            foreach( Models.Book b in this.Books)
            {
                if (b.SourceID >= 1 &&b.SourceID <=8 || b.SourceID>9 && b.SourceID<=11 /*b.SourceID == 5*/ )
                    val.Add(b);
            }

            //BookList = this.Books;

            BookList = val;
        }


        public List<Models.Source> Sources { get; set; }
        public List<Models.Book> Books { get; set; }

        public Models.Chapter GetLastChapter(int bookID)
        {
            //MasterController mc = new MasterController();
            Models.Chapter chap = new Models.Chapter();

            using (ReaderService.ReaderServiceSoapClient svc = new ReaderService.ReaderServiceSoapClient())
            {
                chap = this.SvcChaptertoChapter(svc.GetLastChapter(ProgramStatics.Token, bookID));
            }

            return chap;
        }

        public List<Models.Source> GenerateIndex()
        {
            List<Models.Source> val = new List<Models.Source>();
            val.AddRange(GetSources());

            return val;
        }

        private List<Models.Source> GetSources()
        {
            List<Models.Source> s = new List<Models.Source>();//SvcSourcetoSource(svc.getSourceslst(ProgramStatics.Token));
            using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            {
                s = ConvertSources(svc.getSourceslst(ProgramStatics.Token));
            }
            //svcClosed();

            foreach (Models.Source tmp in s)
            {
                tmp.Books.AddRange(GetBooks(tmp));
                Books.AddRange(tmp.Books);
            }

            return s;
        }

        private List<Models.Book> GetBooks(Models.Source source)
        {

            List<Models.Book> s = new List<Models.Book>();
            List<ReaderService.Book> tmp = new List<ReaderService.Book>();
            using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            {
                tmp.AddRange(svc.getBookslst(ProgramStatics.Token));
            }
            //svcClosed();

            foreach (ReaderService.Book b in tmp)
            {
                if (b.SourceID == source.SourceID)
                {
                    s.Add(SvcBooktoBook(b));
                }
            }

            foreach (Models.Book b in s)
            {
                b.BookLastChapterAddress = this.GetLastChapter(b.BookID).ChapterAddress;
                b.Chapters.AddRange(GetChapters(b));
            }
            return s;
        }

        private List<Models.Chapter> GetChapters(Models.Book book)
        {
            List<Models.Chapter> s = new List<Models.Chapter>();
            List<ReaderService.Chapter> tmp = new List<ReaderService.Chapter>();
            using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            {
                tmp.AddRange(svc.getChapterslstByBook(ProgramStatics.Token, BooktoSvcBook(book)));
            }
            //svcClosed();

            foreach (ReaderService.Chapter b in tmp)
            {
                if (b.BookID == book.BookID)
                {
                    s.Add(SvcChaptertoChapter(b));
                }
            }
            return s;
        }
        
        private List<Models.Source> ConvertSources(ReaderService.Source[] source)
        {
            List<Models.Source> val = new List<Models.Source>();
            foreach (ReaderService.Source s in source)
            {
                val.Add(SvcSourcetoSource(s));
            }
            return val;
        }

        public ReaderService.Source SourcetoSvcSource(Models.Source b)
        {
            ReaderService.Source val = new ReaderService.Source();
            val.Books = ConvertBooks(b.Books);
            val.SourceAddress = b.SourceAddress;
            val.SourceID = b.SourceID;
            val.SourceName = b.SourceName;

            return val;
        }



        public Models.Source SvcSourcetoSource(ReaderService.Source b)
        {
            Models.Source val = new Models.Source();
            val.Books = ConvertBooks(b.Books);
            val.SourceAddress = b.SourceAddress;
            val.SourceID = b.SourceID;
            val.SourceName = b.SourceName;

            return val;
        }

        private List<Models.Book> ConvertBooks(ReaderService.Book[] books)
        {
            List<Models.Book> val = new List<Models.Book>();
            foreach (ReaderService.Book b in books)
            {
                val.Add(SvcBooktoBook(b));
            }
            return val;
        }
        private ReaderService.Book[] ConvertBooks(List<Models.Book> books)
        {
            List<ReaderService.Book> val = new List<ReaderService.Book>();
            foreach (Models.Book b in books)
            {
                val.Add(BooktoSvcBook(b));
            }
            return val.ToArray();
        }

        public ReaderService.Book BooktoSvcBook(Models.Book b)
        {
            ReaderService.Book val = new ReaderService.Book();
            val.BookID = b.BookID;
            val.BookIndexAddress = b.BookIndexAddress;
            val.BookName = b.BookName;
            val.Chapters = ConvertChapters(b.Chapters);
            val.NextChapterAddress = b.NextChapterAddress;
            val.PreviousChapterAddress = b.PreviousChapterAddress;
            val.SourceID = b.SourceID;

            return val;
        }


        #region New
        public Models.Book SvcBooktoBook(ReaderService.Book b)
        {
            Models.Book val = new Models.Book();
            val.BookID = b.BookID;
            val.BookIndexAddress = b.BookIndexAddress;
            val.BookName = b.BookName;
            val.Chapters = ConvertChapters(b.Chapters);
            val.NextChapterAddress = b.NextChapterAddress;
            val.PreviousChapterAddress = b.PreviousChapterAddress;
            val.SourceID = b.SourceID;
            return val;
        }

        private List<Models.Chapter> ConvertChapters(ReaderService.Chapter[] chapters)
        {
            List<Models.Chapter> val = new List<Models.Chapter>();
            foreach (ReaderService.Chapter c in chapters)
            {
                val.Add(SvcChaptertoChapter(c));
            }
            return val;
        }
        private List<ReaderService.Chapter> ConvertChapters(Models.Chapter[] chapters)
        {
            List<ReaderService.Chapter> val = new List<ReaderService.Chapter>();
            foreach (Models.Chapter c in chapters)
            {
                val.Add(ChaptertoSvcChapter(c));
            }
            return val;
        }
        private ReaderService.Chapter[] ConvertChapters(List<Models.Chapter> chapters)
        {
            List<ReaderService.Chapter> val = new List<ReaderService.Chapter>();
            foreach (Models.Chapter c in chapters)
            {
                val.Add(ChaptertoSvcChapter(c));
            }
            return val.ToArray();
        }

        public Models.Chapter SvcChaptertoChapter(ReaderService.Chapter b)
        {
            Models.Chapter val = new Models.Chapter();
            val.BookID = b.BookID;
            val.ChapterAddress = b.ChapterAddress;
            val.ChapterHTML = b.ChapterHtml;
            val.ChapterID = b.ChapterID;
            val.ChapterName = b.ChapterName;
            val.ChapterText = b.ChapterText;
            val.PreviousChapterID = b.PreviousChapterID;

            return val;
        }
        public ReaderService.Chapter ChaptertoSvcChapter(Models.Chapter b)
        {
            ReaderService.Chapter val = new ReaderService.Chapter();
            val.BookID = b.BookID;
            val.ChapterAddress = b.ChapterAddress;
            val.ChapterHtml = b.ChapterHTML;
            val.ChapterID = b.ChapterID;
            val.ChapterName = b.ChapterName;
            val.ChapterText = b.ChapterText;
            val.PreviousChapterID = b.PreviousChapterID;
            return val;
        }

        #endregion







    }
    
}
