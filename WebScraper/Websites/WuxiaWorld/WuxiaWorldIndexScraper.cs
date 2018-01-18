using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraper.Websites.WuxiaWorld
{
    class WuxiaWorldIndexScraper : HtmlIndexScraper
    {
        public WuxiaWorldIndexScraper()
        {
            //this.TagID = "menu-home-menu";
            //this.TagType = "UL";
            this.Books = new Dictionary<string, string>();
        }

        public override void ParseHtml()
        {
            HtmlDocument doc = this.OriginalDoc;
            doc = this.GetTagHtml(doc);
            foreach (HtmlNode n in doc.DocumentNode.Elements("li"))
            {
                if (n.Attributes[0].Value.Equals("menu-item-12207") || n.Attributes[0].Value.Equals("menu-item-2165"))
                {
                    foreach (HtmlNode n1 in n.ChildNodes["ul"].ChildNodes)
                    {
                        try
                        {

                            string book = n1.InnerText;
                            string o = getLink(n1);

                            if (isSkip(book))
                                continue;

                            this.Books.Add(book, o);
                        }
                        catch (ArgumentException ae)
                        {
                            continue;
                        }
                    }
                }
            }
        }

        private bool isSkip(string book)
        {
            if (book.Equals("\r\n"))
                return true;

            string s = book.Clone() as string;
            s = s.Trim();
            if (s.Equals("Home") || s.Equals("Completed") || s.Equals("Active") || s.Equals("About Us") || s.Equals("Contact Us") || s.Equals("Terms of Service") || s.Equals("Resources") || s.Equals("General FAQ") || s.Equals("Basic Dao Primer") || s.Equals("Deathblade's Learning Chinese FAQ") || s.Equals("TJSS Biography") || s.Equals("Translator Thoughts Series") || s.Equals("Donations") || s.Equals("Forums") || s.Equals("Wiki"))
                return true;

            return false;
        }

        private string getLink(HtmlNode n)
        {
            string val = "";
            if (n.ChildNodes.Count > 0)
            {
                val = n.ChildNodes["a"].Attributes["href"].Value;
            }

            return val;
        }

        //public override Dictionary<string, string> returnBooks()
        //{
        //    throw new NotImplementedException();
        //}

        private HtmlDocument GetTagHtml(HtmlDocument doc)
        {
            HtmlNode node = doc.GetElementbyId("menu-home-menu");
            HtmlDocument doc2 = new HtmlDocument();
            doc2.LoadHtml(node.InnerHtml);

            return doc2;
        }
    }
}
