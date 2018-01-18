using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraper.Websites.WuxiaWorld
{
    class WuxiaWorldBookScraper : HtmlBookScraper
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
            string s = this.NextChapterAddress;
            int tmpi = s.Length - 1;
            //char ch = PreviousChapterAddress[tmpi];
            string l;
            if (!s[tmpi].Equals('/'))
            {
                s = s + "/";
            }
            this.NextChapter = s;
            return s;
        }


        public override string GetPreviousChapter()
        {
            if (string.IsNullOrEmpty(this.PreviousChapterAddress))
                return "Page Not Found";
            string s = this.PreviousChapterAddress;
            int tmpi = s.Length - 1;
            //char ch = PreviousChapterAddress[tmpi];
            string l;
            if (!s[tmpi].Equals('/'))
            {
                s = s + "/";
            }
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



        public WuxiaWorldBookScraper()
        {
            //OriginalDoc = new HtmlDocument();
        }

        public WuxiaWorldBookScraper(string oDoc)
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




        #region old code

        /*
        public WuxiaWorldBookScraper()
        {
            this.Contents = "";
            this.OriginalDoc = new HtmlDocument();
            this.NextChapter = "Page Not Found";
        }
        
        /// <summary>
        /// returns the address of the next chapter
        /// </summary>
        /// <returns></returns>
        public override string GetNextChapter ()
        {
            if (!this.OriginalDoc.Equals(new HtmlDocument()))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(this.OriginalDoc.DocumentNode.OuterHtml);

                //HtmlNode node = doc.GetElementbyId("main").ChildNodes["article"].ChildNodes["div"].ChildNodes["div"].ChildNodes["div"].ChildNodes["p"].ChildNodes["span"];
                HtmlNode node = GetContentNode(doc);
                if (node == null)
                    return "Page Not Found";
                nodes = new List<HtmlNode>();
                getChapterAddress(node);
                HtmlNode n = nodes[0];
                foreach(HtmlNode node1 in nodes)
                {
                    if(node1.InnerText.Contains("Next") || node1.InnerText.Contains("Next Chapter"))
                    {
                        n = node1;
                        break;
                    }
                }
                HtmlNode n1 = n.ParentNode;
                //string s = n1.Attributes["href"].Value;
                if (n.ChildNodes["a"] != null)
                {
                    if (n.ChildNodes["a"].InnerText.Contains("Previous") || n.ChildNodes["a"].InnerText.Contains("Previous Chapter"))
                    {
                        n.ChildNodes["a"].Remove();
                    }
                    if(n.ChildNodes["span"] != null)
                    {
                        if (n.InnerText.Contains("Next") || n.InnerText.Contains("Next Chapter"))
                        {
                            n = n.ChildNodes["span"];
                        }
                    }
                    string s = n.ChildNodes["a"].Attributes["href"].Value;
                    return s;
                }
                else if (n.ChildNodes["span"] != null)
                {
                    HtmlNode n2 = n.ChildNodes["span"];
                    if (n2.ChildNodes["a"].InnerText.Contains("Previous") || n2.ChildNodes["a"].InnerText.Contains("Previous Chapter"))
                    {
                        n2.ChildNodes["a"].Remove();
                    }
                    string s = n.ChildNodes["span"].ChildNodes["a"].Attributes["href"].Value;
                    return s;
                }
                else if(n.ParentNode.ChildNodes["a"] != null)
                {
                    HtmlNode n2 = n.ParentNode;
                    if (n2.ChildNodes["a"].InnerText.Contains("Previous") || n2.ChildNodes["a"].InnerText.Contains("Previous Chapter"))
                    {
                        n2.ChildNodes["a"].Remove();
                    }
                    string s = n.ParentNode.ChildNodes["a"].Attributes["href"].Value;
                    return s;
                }
                else if (n.ParentNode.ParentNode.ChildNodes["a"] != null)
                {
                    HtmlNode n2 = n.ParentNode.ParentNode;
                    if(n2.ChildNodes["a"].InnerText.Contains("Previous") || n2.ChildNodes["a"].InnerText.Contains("Previous Chapter"))
                    {
                        n2.ChildNodes["a"].Remove();
                    }
                    string s = n2.ChildNodes["a"].Attributes["href"].Value;
                    return s;
                }
                else if(n.ParentNode.ChildNodes["span"] != null)
                {
                    HtmlNode n2 = n.ParentNode;
                    if (n2.ChildNodes["a"].InnerText.Contains("Previous") || n2.ChildNodes["a"].InnerText.Contains("Previous Chapter"))
                    {
                        n2.ChildNodes["a"].Remove();
                    }
                    string s = n.ParentNode.ChildNodes["span"].ChildNodes["a"].Attributes["href"].Value;
                    return s;
                }
                    
                //string s = n.ChildNodes["a"].Attributes["href"].Value;
                //return s;
            }
            return "Page Not Found";
        }
        
        private void getChapterAddress(HtmlNode node)
        {
            if (node == null)
                return;
            if (node.HasChildNodes)
            {
                foreach (HtmlNode n in node.ChildNodes)
                {
                    if (n.Name.Equals("span"))
                    {
                        //n.Remove();
                        nodes.Add(n);
                    }
                    else
                        getChapterAddress(n);
                }
            }
        }

        public override void ParseHtml()
        {
            if (!this.OriginalDoc.Equals(new HtmlDocument()))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(this.OriginalDoc.DocumentNode.OuterHtml);

                //HtmlNode node = doc.GetElementbyId("main").ChildNodes["article"].ChildNodes["div"].ChildNodes["div"].ChildNodes["div"];
                HtmlNode node = RemoveExtraNodes(GetContentNode(doc));
                //HtmlNode n1 = node.LastChild;
                //HtmlNode n2 = node.FirstChild;

                if (node == null)
                {
                    this.Contents = "Page Not Found";
                    return;
                }

                string s = node.InnerText;
                s = s.Trim();


                this.Contents = s;
                this.HTML = node.OuterHtml.Trim();
                //return s;
            }
            else
            {
                this.Contents = "Page Not Found";
                //return "Page Not Found";
            }
        }

        public override string GetCurrentChapterName()
        {
            if (!this.OriginalDoc.Equals(new HtmlDocument()))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(this.OriginalDoc.DocumentNode.OuterHtml);
                string val;
                try
                {
                    val = doc.GetElementbyId("main").ChildNodes["article"].ChildNodes["header"].InnerText;
                }
                catch (NullReferenceException nre)
                {
                    try
                    {
                        HtmlNode node = doc.GetElementbyId("main").ChildNodes["article"].ChildNodes["div"].ChildNodes["div"].ChildNodes["div"].ChildNodes["p"].ChildNodes["span"];
                        val = node.ChildNodes["a"].Attributes["href"].Value;
                    }catch(Exception e) { return "Page Not Found"; }
                }
                if(string.IsNullOrEmpty(val))
                {
                    return "Page Not Found";
                }
                return val;
            }
            else return "Page Not Found";
        }

        public HtmlNode GetContentNode(HtmlDocument doc)
        {
            HtmlNode n;
            foreach (HtmlNode node in doc.DocumentNode.ChildNodes)
            {
                if (checkNode(node))
                    return node;
                n = GetArticleBody(node);
                if (checkNode(n))
                    return n;
            }
            return null;
        }
        //articleBody
        public HtmlNode GetArticleBody(HtmlNode node)
        {
            if (checkNode(node))
                return node;

            HtmlNode n;

            if (node.HasChildNodes)
            {
                foreach (HtmlNode n1 in node.ChildNodes)
                {
                    if (checkNode(n1))
                        return n1;

                    n = GetArticleBody(n1);
                    if (checkNode(n))
                    {
                        return n;
                    }
                }
            }
            return null;
        }

        private bool checkNode(HtmlNode n)
        {
            if (n == null)
                return false;
            if (n.HasAttributes)
            {
                if (n.Attributes[0].Value.Equals("articleBody"))
                    return true;
            }
            return false;
        }

        public HtmlNode RemoveExtraNodes(HtmlNode node)
        {
            if (node == null)
                return null;
            nodes = new List<HtmlNode>();
            removeNodes(node);
            nodes.Add(GetFootnoteBody(node));

            foreach(HtmlNode n in nodes)
            {
                if (n == null)
                    return node;
                if (n.Name.Equals("p"))//Previous Next Chapter
                {
                    string s = n.InnerText.Trim();
                    if (!s.Equals("Next Chapter") && !s.Equals("Previous Next Chapter") && !s.Equals("&gt;Teaser for Next Chapter&lt;") && !s.Equals("Previous Chapter") && !s.Equals("Previous ChapterNext Chapter&nbsp;") && !s.Equals("Previous ChapterNext Chapter") && !s.Equals("Previous Chapter Next Chapter") && !s.Equals("Previous Chapter&nbsp;ChapterNext Chapter") && !s.Equals("&nbsp;") && !s.Equals("Previous&nbsp;ChapterNext Chapter"))
                        continue;
                }
                n.Remove();
            }

            return node;
        }

        

        private void removeNodes(HtmlNode node)
        {
            if (node == null)
                return;
            if (node.HasChildNodes)
            {
                foreach (HtmlNode n in node.ChildNodes)
                {
                    if (n.Name.Equals("a") ||  n.Name.Equals("p") || n.Name.Equals("div") || n.Name.Equals("ol") || n.Name.Equals("li"))
                    {
                        //n.Remove();
                        nodes.Add(n);
                    }
                    else
                        removeNodes(n);
                }
            }
        }

        public HtmlNode GetFootnoteBody(HtmlNode node)
        {
            if (checkNodeClass(node))
                return node;

            HtmlNode n;

            if (node.HasChildNodes)
            {
                foreach (HtmlNode n1 in node.ChildNodes)
                {
                    if (checkNodeClass(n1))
                        return n1;

                    n = GetArticleBody(n1);
                    if (checkNodeClass(n))
                    {
                        return n;
                    }
                }
            }
            return null;
        }

        private bool checkNodeClass(HtmlNode n)
        {
            if (n == null)
                return false;
            if (n.HasAttributes)
            {
                foreach (HtmlAttribute a in n.Attributes)
                {
                    if (a.Value.Equals("footnote"))
                        return true;
                }
            }
            return false;
        }

        //public override HtmlDocument GetTagHtml(HtmlDocument doc)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void ParseHtml(HtmlDocument doc)
        //{
        //    throw new NotImplementedException();
        //}
        */
        #endregion
    }
}
