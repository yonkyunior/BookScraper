using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebScraper.Websites.BlueSilverTranslations
{
    class BlueSilverHtmlBookScraper: HtmlBookScraper
    {
        public string NextChapter { get; set; }
        public string PreviousChapter { get; set; }

        List<HtmlNode> nodes;

        /*
        private HtmlDocument _originalDoc { get; set; }
        public  HtmlDocument OriginalDoc
        {
            get
            {
                return _originalDoc;
            }
            set
            {
                _originalDoc = value;
                OriginalHtml = _originalDoc.DocumentNode.OuterHtml;
            }
        }
        */
        public HtmlNode ReducedDoc { get; set; }

        public string OriginalHtml { get; set; }
        public string ChapterName { get; set; }
        public string ChapterAddress { get; set; }
        public string NextChapterAddress { get; set; }
        public string PreviousChapterAddress { get; set; }
        public string CHTML { get; set; }
        public string CText { get; set; }



        public override string GetNextChapter()
        {
            if (string.IsNullOrEmpty(this.NextChapterAddress))
                return "Page Not Found";
            //string s = this.NextChapterAddress;

            if (this.NextChapterAddress.Equals("http://moonbunnycafe.com/genius-doctor-black-belly-miss/gdbbm-chapter-#/"))
            {
                this.NextChapterAddress = "Page Not Found";
                this.NextChapter = "Page Not Found";
                return "Page Not Found";
            }

            string index = "";
            index = this.NextChapterAddress.Substring(0, 24);

            string index2 = "";
            index2 = this.NextChapterAddress.Substring(0, 28);

            bool v1 = !index.Equals("http://moonbunnycafe.com");
            bool v2 = !index2.Equals("http://www.moonbunnycafe.com");

            string s;

            if (index.Equals("http://moonbunnycafe.com") || index2.Equals("http://www.moonbunnycafe.com"))
            {
                s = this.NextChapterAddress;
            }
            else
            {
                s = "http://moonbunnycafe.com" + this.NextChapterAddress;
            }

            //if (!index.Equals("http://moonbunnycafe.com") && !index2.Equals("http://www.moonbunnycafe.com"))
            //{
            //    s = "http://moonbunnycafe.com" + this.NextChapterAddress;
            //}
            //else
            //{
            //    s = this.NextChapterAddress;
            //}


            string tmp = HttpUtility.HtmlDecode(s);

            if (tmp[0] == '\"' || tmp[0] == '\'')
                tmp = tmp.Substring(1);


            string k = tmp;//this.NextChapterAddress;
            int tmpi = k.Length - 1;
            //char ch = PreviousChapterAddress[tmpi];
            string l;
            if (!s[tmpi].Equals('/'))
            {
                k = k + "/";
            }

            string tmps = "";
            for (int i = 0; i < k.Length; i++)
            {
                if (k[i].Equals('\n') || k[i].Equals('\r'))
                    tmps = k.Remove(i, 1);
            }

            if (!string.IsNullOrEmpty(tmps))
                k = tmps;

            this.NextChapterAddress = k;
            this.NextChapter = k;
            return k;
        }


        public override string GetPreviousChapter()
        {
            if (string.IsNullOrEmpty(this.PreviousChapterAddress))
                return "Page Not Found";
            //string s = this.PreviousChapterAddress;

            //string index = "";
            //using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            //{
            //    index = svc.getBookIndexAddress(ProgramStatics.Token, this.BookID);
            //}
            string index = "";
            index = this.PreviousChapterAddress.Substring(0, 24);

            string index2 = "";
            index2 = this.PreviousChapterAddress.Substring(0, 28);

            bool v1 = !index.Equals("http://moonbunnycafe.com");
            bool v2 = !index2.Equals("http://www.moonbunnycafe.com");

            string s;

            if (index.Equals("http://moonbunnycafe.com") || index2.Equals("http://www.moonbunnycafe.com"))
            {
                s = this.PreviousChapterAddress;
            }
            else
            {
                s = "http://moonbunnycafe.com" + this.PreviousChapterAddress;
            }

            //if (!index.Equals("http://moonbunnycafe.com") && !index2.Equals("http://www.moonbunnycafe.com"))
            //{
            //    s = "http://moonbunnycafe.com" + this.PreviousChapterAddress;
            //}
            //else
            //{
            //    s = this.PreviousChapterAddress;
            //}


            string tmp = HttpUtility.HtmlDecode(s);

            if (tmp[0] == '\"' || tmp[0] == '\'')
                tmp = tmp.Substring(1);




            string k = tmp;//this.NextChapterAddress;
            int tmpi = k.Length - 1;
            //char ch = PreviousChapterAddress[tmpi];
            string l;
            if (!s[tmpi].Equals('/'))
            {
                k = k + "/";
            }

            string tmps = "";
            for (int i = 0; i < k.Length; i++)
            {
                if (k[i].Equals('\n') || k[i].Equals('\r'))
                    tmps = k.Remove(i, 1);
            }
            if (!string.IsNullOrEmpty(tmps))
                k = tmps;
            //string s = this.PreviousChapterAddress;
            this.PreviousChapter = k;
            this.PreviousChapterAddress = k;
            return k;
        }

        public override string GetCurrentChapterName()
        {

            this.OriginalHtml = this.OriginalDoc.DocumentNode.OuterHtml;
            return GetChapterName();
        }

        public override void ParseHtml()
        {
            this.OriginalHtml = this.OriginalDoc.DocumentNode.OuterHtml;
            RemoveUnnecessaryTags();
            //throw new NotImplementedException();
        }



        public BlueSilverHtmlBookScraper()
        {
            //OriginalDoc = new HtmlDocument();
        }

        public BlueSilverHtmlBookScraper(string oDoc)
        {
            OriginalDoc = new HtmlDocument();
            OriginalDoc.LoadHtml(oDoc);
            OriginalHtml = oDoc;


            ChapterName = GetChapterName();
        }

        private string GetChapterName()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(OriginalHtml);
            HtmlNode n;

            if (doc.DocumentNode.ChildNodes["HEAD"] != null)
                n = doc.DocumentNode.ChildNodes["HEAD"].ChildNodes["TITLE"];
            else
                n = doc.DocumentNode.ChildNodes["HEADER"].ChildNodes["TITLE"];


            return n.InnerText;
        }

        public string RemoveUnnecessaryTags()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(OriginalHtml);

            //HtmlNode n = RemoveHeaderFooter(doc).DocumentNode.ChildNodes["BODY"];
            HtmlNode n = doc.DocumentNode;
            //n = RemoveMeta(n);
            n = RemoveBulkofNodes(n);
            n = GetChapterAddresses(n);
            GetArticleBody(n);
            //ReducedDoc = FindContentBody(n);
            if (ReducedDoc == null)
                return null;
            this.CText = ReducedDoc.InnerText.Trim();
            this.CHTML = ReducedDoc.OuterHtml.Trim();
            this.HTML = ReducedDoc.OuterHtml.Trim();
            this.Contents = ReducedDoc.InnerText.Trim();
            //ReducedDoc = doc;
            return doc.DocumentNode.OuterHtml;
        }

        private void GetArticleBody(HtmlNode doc)
        {

            this.ReducedDoc = OriginalDoc.GetElementbyId("contents");
            return;

            List<HtmlNode> nodes = new List<HtmlNode>();


            foreach (HtmlNode node in doc.ChildNodes)
            {
                //class="entry-content clear
                //div id="contents"



                if (node.Name.Equals("div"))
                {


                    if (node.Attributes != null)
                    {
                        if (node.Attributes["class"] != null)
                        {

                            if (node.Attributes["class"].Value.Equals("innerContent"))
                            {
                                if (!(node.ChildNodes.Count < 10))
                                {
                                    this.ReducedDoc = node;
                                    return;
                                }
                            }
                            else
                                if (node.Attributes["class"].Value.Equals("entry-content clear"))
                            {
                                if (!(node.ChildNodes.Count < 10))
                                {
                                    this.ReducedDoc = node;
                                    return;
                                }
                            }
                            else
                                if (node.Attributes["class"].Value.Equals("contents"))
                            {
                                if (!(node.ChildNodes.Count < 10))
                                {
                                    this.ReducedDoc = node;
                                    return;
                                }
                            }
                            else
                                if (node.Attributes["class"].Value.Equals("entry-content"))
                            {
                                if (!(node.ChildNodes.Count < 10))
                                {
                                    this.ReducedDoc = node;
                                    return;
                                }
                            }
                        }
                        else
                    if (node.Attributes["itemprop"] != null)
                        {
                            if (node.Attributes["itemprop"].Value.Equals("articleBody"))
                            {
                                this.ReducedDoc = node;
                                return;
                            }
                        }
                        else if (node.Id != null)
                        {
                            if (node.Id.Equals("content"))
                            {

                                this.ReducedDoc = node;
                                return;

                            }

                        }

                    }
                }

                GetArticleBody(node);
            }


        }

        private HtmlNode GetChapterAddresses(HtmlNode doc)
        {
            List<HtmlNode> nodes = new List<HtmlNode>();
            foreach (HtmlNode node in doc.ChildNodes)
            {
                if (node.Name.Equals("a"))
                {
                    if (node.Attributes["rel"] != null)
                    {
                        if (node.Attributes["rel"].Value.Equals("prev") || node.Attributes["rel"].Value.Equals("Prev") ||
                            node.Attributes["rel"].Value.Equals("previous") || node.Attributes["rel"].Value.Equals("Previous"))
                        {
                            this.PreviousChapterAddress = node.Attributes["href"].Value;
                            nodes.Add(node);
                        }
                        else if (node.Attributes["rel"].Value.Equals("next") || node.Attributes["rel"].Value.Equals("Next"))
                        {
                            this.NextChapterAddress = node.Attributes["href"].Value;
                            nodes.Add(node);
                        }
                    }

                    if (!node.InnerText.Trim().Contains("Previous") && !node.InnerText.Trim().Contains("Next"))
                        nodes.Add(node);
                    //else if(!node.InnerText.Trim().Equals("Previous"))
                    //    nodes.Add(node);
                    else//&lt;&lt;Previous
                    if (node.InnerText.Trim().Equals("Previous Chapter") || node.InnerText.Trim().Equals("Previous")
                        || node.InnerText.Trim().Equals("Previous&nbsp;Chapter")
                        || node.InnerText.Trim().Equals("<- Previous Chapter")
                        || node.InnerText.Trim().Equals("&lt;- Previous Chapter") || node.InnerText.Trim().Equals("&lt;&lt;Previous")
                        || node.InnerText.Trim().Equals("&lt;- Previous Part")
                        || node.InnerText.Trim().Equals("[Previous Chapter]"))
                    {
                        if (node.Attributes["href"] != null)
                            this.PreviousChapterAddress = node.Attributes["href"].Value;

                        nodes.Add(node);
                    }
                    else //Next&gt;&gt;
                    if (node.InnerText.Trim().Equals("Next Chapter") || node.InnerText.Trim().Equals("Next")
                        || node.InnerText.Trim().Equals("Next&nbsp;Chapter") || node.InnerText.Trim().Equals("Next Chapter ->")
                        || node.InnerText.Trim().Equals("Next Chapter -&gt;") || node.InnerText.Trim().Equals("Next&gt;&gt;")
                        || node.InnerText.Trim().Equals("Next Part -&gt;")
                        || node.InnerText.Trim().Equals("[Next Chapter]"))
                    {
                        if (node.Attributes["href"] != null)
                            this.NextChapterAddress = node.Attributes["href"].Value;

                        nodes.Add(node);
                    }
                }

                GetChapterAddresses(node);
            }

            foreach (HtmlNode n in nodes)
                n.Remove();

            return doc;
        }


        private HtmlDocument RemoveHeaderFooter(HtmlDocument doc)
        {
            List<HtmlNode> tmp = new List<HtmlNode>();
            if (doc.DocumentNode.ChildNodes["HEAD"] != null)
                tmp.Add(doc.DocumentNode.ChildNodes["HEAD"]);
            if (doc.DocumentNode.ChildNodes["HEADER"] != null)
                tmp.Add(doc.DocumentNode.ChildNodes["FOOTER"]);
            if (doc.DocumentNode.ChildNodes["FOOT"] != null)
                tmp.Add(doc.DocumentNode.ChildNodes["FOOT"]);

            foreach (HtmlNode n in tmp)
                n.Remove();
            return doc;
        }

        public HtmlDocument FindContentBody(HtmlNode doc)
        {
            /* if (!loop)
                 return doc;
                 */

            HtmlDocument n = new HtmlDocument();
            n.LoadHtml(doc.OuterHtml);
            return n;
        }
    }
}
