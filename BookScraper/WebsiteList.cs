using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookScraper
{
    public class Websites
    {
        private Websites(string name, string address) { WebsiteName = name; WebsiteAddress = address; }

        public string WebsiteAddress { get; set; }
        public string WebsiteName { get; set; }

        public static Websites WuxiaWorld { get { return new Websites("WuxiaWorld","http://www.WuxiaWorld.com"); } }
        
    }

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
        { get { return new Books("Coiling Dragon", "http://www.wuxiaworld.com/cdindex-html/book-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books MartialGodAsura
        { get { return new Books("Martial God Asura", "http://www.wuxiaworld.com/mga-index/mga-chapter-968/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books IShallSealTheHeavens
        { get { return new Books("I Shall Seal The Heavens", "http://www.wuxiaworld.com/issth-index/issth-book-3-chapter-303/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books AgainstTheGods
        { get { return new Books("Against The Gods", "http://www.wuxiaworld.com/atg-index/prologue/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books ChildOfLight
        { get { return new Books("Child Of Light", "http://www.wuxiaworld.com/col-index/col-volume-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books GateOfRevelation
        { get { return new Books("Gate Of Revelation", "http://www.wuxiaworld.com/gor-index/gor-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books PerfectWorld
        { get { return new Books("Perfect World", "http://www.wuxiaworld.com/pw-index/pw-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SkyfireAvenue
        { get { return new Books("Skyfire Avenue", "http://www.wuxiaworld.com/sfl-index/skyfire-lane-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books TerrorInfinity
        { get { return new Books("Terror Infinity", "http://www.wuxiaworld.com/ti-index/ti-vol-1-chapter-1-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books UpgradeSpecialistInAnotherWorld
        { get { return new Books("Upgrade Specialist In Another World", "http://www.wuxiaworld.com/usaw-index/usaw-prologue/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books WuDongQianKun
        { get { return new Books("Wu Dong Qian Kun", "http://www.wuxiaworld.com/wdqk-index/wdqk-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books DesolateEra
        { get { return new Books("Desolate Era", "http://www.wuxiaworld.com/desolate-era-index/de-book-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books HeavenlyJewelChange
        { get { return new Books("Heavenly Jewel Change", "http://www.wuxiaworld.com/hjc-index/hjc-chapter-96-2/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books RenegadeImmortal
        { get { return new Books("Renegade Immortal", "http://www.wuxiaworld.com/renegade-index/renegade-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SovereignOfTheThreeRealms
        { get { return new Books("Sovereign of the Three Realms", "http://www.wuxiaworld.com/sotr-index/sotr-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books TalesOfDeamonsAndGods
        { get { return new Books("Tales of Deamons and Gods", "http://www.wuxiaworld.com/tdg-index/tdg-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books TheGreatRuler
        { get { return new Books("The Great Ruler", "http://www.wuxiaworld.com/tgr-index/tgr-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books WarlockOfTheMagusWorld
        { get { return new Books("Warlock of the Magus World", "http://www.wuxiaworld.com/wmw-index/wmw-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SevenKillers
        { get { return new Books("Seven Killers", "http://www.wuxiaworld.com/master-index/7-killers-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books DragonKingWithSevenStars
        { get { return new Books("Dragon King with Seven Stars", "http://www.wuxiaworld.com/master-index/dkwss-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books HerosShedNoTears
        { get { return new Books("Heros Shed no Tears", "http://www.wuxiaworld.com/master-index/prologue/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books HorizonBrightmoonSabre
        { get { return new Books("Horizon, Bright Moon, Sabre", "http://www.wuxiaworld.com/tymyd-index/chapter-23/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        //public static Books StellarTransformations
        //{ get { return new Books("Stellar Transformations", "http://www.wuxiaworld.com/st-index/st-book-1-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books SpiritRealm
        { get { return new Books("Spirit Realm", "http://www.wuxiaworld.com/sr-index/sr-chapter-1/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        public static Books BattleThroughTheHeavens
        { get { return new Books("Battle Through The Heavens", "http://www.wuxiaworld.com/btth-index/btth-chapter-3/", Websites.WuxiaWorld, WebScraper.PageTypes.WuxiaBook); } }
        #endregion
    }

    public class WebsiteList
    {
        public List<Books> BookList;
        
        public WebsiteList()
        {
            this.BookList = new List<Books>();

            #region WuxiaWorld Books
            //this.BookList.Add(Books.CoilingDragon);
            //this.BookList.Add(Books.MartialGodAsura);
            //this.BookList.Add(Books.IShallSealTheHeavens);
            //this.BookList.Add(Books.AgainstTheGods);
            //this.BookList.Add(Books.ChildOfLight);
            //this.BookList.Add(Books.DesolateEra);
            //this.BookList.Add(Books.DragonKingWithSevenStars);
            //this.BookList.Add(Books.GateOfRevelation);
            //this.BookList.Add(Books.HeavenlyJewelChange);
            //this.BookList.Add(Books.HerosShedNoTears);
            //this.BookList.Add(Books.HorizonBrightmoonSabre);
            //this.BookList.Add(Books.PerfectWorld);
            //this.BookList.Add(Books.RenegadeImmortal);
            //this.BookList.Add(Books.SevenKillers);
            //this.BookList.Add(Books.SkyfireAvenue);
            //this.BookList.Add(Books.SovereignOfTheThreeRealms);
            //this.BookList.Add(Books.TalesOfDeamonsAndGods);
            //this.BookList.Add(Books.TerrorInfinity);
            //this.BookList.Add(Books.TheGreatRuler);
            //this.BookList.Add(Books.UpgradeSpecialistInAnotherWorld);
            //this.BookList.Add(Books.WarlockOfTheMagusWorld);
            //this.BookList.Add(Books.WuDongQianKun);
            //this.BookList.Add(Books.SpiritRealm);
            this.BookList.Add(Books.BattleThroughTheHeavens);
            #endregion

        }
    }
}
