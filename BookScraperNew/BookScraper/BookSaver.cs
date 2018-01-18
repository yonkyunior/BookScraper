using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BookScraper
{
    public class BookSaver
    {
        public string FileLocation { get; set; }
        public string Chapter { get; set; }

        /// <summary>
        /// initializes class creating default save location if location doesnt exist
        /// </summary>
        public BookSaver()
        {
            this.FileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\books";

            verifyDirectory(FileLocation);
        }

        /// <summary>
        /// initializes class creating save directory for book if directory dosnt exist
        /// </summary>
        /// <param name="bookName">the name of the source website and book in format 'website\\book'</param>
        public BookSaver(string bookName) : this()
        {
            this.FileLocation += string.Format("\\{0}", bookName);

            verifyDirectory(FileLocation);
        }

        /// <summary>
        /// initializes class creating save directory for book if directory doesnt exist, also sets the chapter number
        /// </summary>
        /// <param name="bookName">the name of the source website and book in format 'website\\book'</param>
        /// <param name="chapterNumber">Name of Chapter if Chapter is 0 this is an Index</param>
        public BookSaver(string bookName, string chapterNumber) : this(bookName)
        {
            this.Chapter = string.Format("{0}", chapterNumber);
            if(this.Chapter.Contains(':'))
            {
                this.Chapter = this.Chapter.Replace(':', ',');
            }
            try
            {
                this.FileLocation += string.Format("\\{0}.txt", this.Chapter);
            }catch(Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// Verifies the existence of directory if not, creates Directory
        /// </summary>
        /// <param name="fileLocation"></param>
        private void verifyDirectory(string fileLocation)
        {
            if (Directory.Exists(this.FileLocation))
            {
                return;
            }
            Directory.CreateDirectory(this.FileLocation);
        }

        /// <summary>
        /// Saves the contents of a chapter
        /// </summary>
        /// <param name="bookContent">the chapter itself</param>
        public void saveBook(string bookContent)
        {
            //verifyDirectory(this.FileLocation);
            try
            {
                if (File.Exists(this.FileLocation))
                {
                    File.SetAttributes(this.FileLocation, FileAttributes.Normal);
                }
                File.WriteAllText(this.FileLocation, bookContent);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// Saves a list of books by Website Name
        /// </summary>
        /// <param name="bookList"></param>
        public void saveBookList(string bookList)
        {
            File.WriteAllText(this.FileLocation, bookList);
        }
    }
}
