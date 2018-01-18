using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraper.Websites
{
    public abstract class HtmlScraper
    {
        public HtmlDocument OriginalDoc { get; set; }

        ///// <summary>
        ///// The tag Type that the desired contents are stored in
        ///// </summary>
        //public string TagType { get; set; }

        ///// <summary>
        ///// The TagID that the desired contents are stored in
        ///// </summary>
        //public string TagID { get; set; }

        /// <summary>
        /// Parses the HtmlDoc and stores the values in a class property
        /// </summary>
        /// <param name="doc">The Doc To be Parsed</param>
        public abstract void ParseHtml();

        //public abstract HtmlDocument GetTagHtml(HtmlDocument doc);
    }
}
