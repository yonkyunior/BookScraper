using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebScraper.ReaderService;

namespace WebScraper.Websites.XianXiaWorld
{
    class XianXiaWorldBookScraper : HtmlBookScraper
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
            if (string.IsNullOrEmpty(this.NextChapterAddress) || this.NextChapterAddress.Equals("./")
                || this.NextChapterAddress.Equals(".") || this.NextChapterAddress.Equals("/"))
                return "Page Not Found";

            string index = "";
            using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            {
                index = svc.getBookIndexAddress(ProgramStatics.Token, this.BookID);
            }

                string s = index + this.NextChapterAddress;
            this.NextChapter = s;
            return s;
        }


        public override string GetPreviousChapter()
        {
            if (string.IsNullOrEmpty(this.PreviousChapterAddress))
                return "Page Not Found";

            string index = "";
            using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            {
                index = svc.getBookIndexAddress(ProgramStatics.Token, this.BookID);
            }
            
            string s = index + this.PreviousChapterAddress;
            this.PreviousChapterAddress = s;
            return s;
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



        public XianXiaWorldBookScraper()
        {
            //OriginalDoc = new HtmlDocument();
        }

        public XianXiaWorldBookScraper(string oDoc)
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
            List<HtmlNode> nodes = new List<HtmlNode>();


            foreach (HtmlNode node in doc.ChildNodes)
            {
                //class="entry-content clear
                //div id="content"



                if (node.Name.Equals("div"))
                {


                    if (node.Attributes != null)
                    {
                        if (node.Attributes["class"] != null)
                        {
                            if (node.Attributes["class"].Value.Equals("innerContent"))
                            {
                                this.ReducedDoc = node;
                                return;
                            }
                            else
                                if (node.Attributes["class"].Value.Equals("entry-content clear"))
                            {
                                this.ReducedDoc = node;
                                return;
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
                    if (!node.InnerText.Trim().Contains("Previous") && !node.InnerText.Trim().Contains("Next"))
                        nodes.Add(node);
                    //else if(!node.InnerText.Trim().Equals("Previous"))
                    //    nodes.Add(node);
                    else//Previous&nbsp;Chapter
                    if (node.InnerText.Trim().Equals("Previous Chapter") || node.InnerText.Trim().Equals("Previous") || node.InnerText.Trim().Equals("Previous&nbsp;Chapter"))
                    {
                        if (node.Attributes["href"] != null)
                            this.PreviousChapterAddress = node.Attributes["href"].Value;

                        nodes.Add(node);
                    }
                    else
                    if (node.InnerText.Trim().Equals("Next Chapter") || node.InnerText.Trim().Equals("Next") || node.InnerText.Trim().Equals("Next&nbsp;Chapter"))
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
