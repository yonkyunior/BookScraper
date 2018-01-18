using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Websites
{
    abstract class HtmlBookScraper : HtmlScraper
    {
        /// <summary>
        /// Stores a Book's Chapter {ChapterNumber, ChapterAddress}
        /// </summary>
        //public KeyValuePair<int, string> ChapterInfo { get; set; }
        ///// <summary>
        ///// stores the address to chapter
        ///// </summary>
        //public string ChapterAddress { get; set; }
        public string ChapterAddress { get; set; }
        public string  HTML { get; set; }
        public string  Contents { get; set; }
        public int BookID { get; set; }


        /// <summary>
        /// Gets the address of the next chapter to be parsed
        /// </summary>
        /// <returns>Returns the Address of the next Chapter, if no Chapter returns 'Page Not Found'</returns>
        public abstract string GetNextChapter();

        public abstract string GetPreviousChapter();


        protected HtmlNode RemoveBulkofNodes(HtmlNode doc)
        {
            List<HtmlNode> nodes = new List<HtmlNode>();
            foreach (HtmlNode node in doc.ChildNodes)
            {
                if (node.Name.Equals("head", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("header", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("foot", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("footer", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("script", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("meta", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("ul", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("link", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("iframe", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("images", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("video", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.Name.Equals("audio", StringComparison.OrdinalIgnoreCase))
                    nodes.Add(node);
                if (node.NodeType == HtmlNodeType.Comment)
                    nodes.Add(node);
                if (node.Name.Equals("div"))
                {
                    if (node.Id.Equals("comments") || node.Id.Equals("comment") || node.Id.Equals("Comments") || node.Id.Equals("Comment"))
                        nodes.Add(node);
                }
                if (node.Attributes["class"] != null)
                {//sharedaddy
                    if (node.Attributes["class"].Value.Equals("sd-content") || node.Attributes["class"].Value.Contains("sharedaddy"))
                        nodes.Add(node);
                }
                RemoveBulkofNodes(node);
            }

            foreach (HtmlNode n in nodes)
                n.Remove();

            return doc;
        }
        ///// <summary>
        ///// stores the chapter number
        ///// </summary>
        //public int ChapterNumber { get; set; }

        /// <summary>
        /// Gets the Name of the current Chapter Being Parsed
        /// </summary>
        /// <returns>Returns The Current Chapter Name</returns>
        public abstract string GetCurrentChapterName();
    }
}
