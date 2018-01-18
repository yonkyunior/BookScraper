using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using BookScraper.Models;
using BookScraper.ReaderService;

namespace BookScraper
{

    public class DatabaseWriter
    {
        public string ChapterAddress { get; set; }
        public string PreviousChapterAddress { get; set; }
        public string ChapterHtml { get; set; }
        public int BookID { get; set; }
        public string ChapterName { get; set; }
        public string ChapterText { get; set; }
        //public int PreviousChapterID { get; set; }
        

        public void SaveChapter()
        {
            if (string.IsNullOrEmpty(this.ChapterText))
                return;
            Models.Chapter c = new Models.Chapter();
            c.ChapterAddress = this.ChapterAddress;
            c.ChapterHTML = Compression.CompressString(this.ChapterHtml);
            c.BookID = this.BookID;
            c.ChapterName = this.ChapterName;
            c.ChapterText = Compression.CompressString(this.ChapterText);

            int itemp = 0;
            if (!string.IsNullOrEmpty(this.PreviousChapterAddress))
            {
                using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
                {

                    itemp = svc.GetPreviousChapterID(ProgramStatics.Token, this.PreviousChapterAddress);// GetPreviousChapterID();
                }
            }
            c.PreviousChapterID = itemp;

            int id;
            using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
            {

                id = svc.ChapterExists(ProgramStatics.Token, this.ChapterAddress);// ChapterExists();
            }

            if (id >= 0)
            {
                c.ChapterID = id;
                using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
                {

                    svc.OverwriteChapter(ProgramStatics.Token, this.ChaptertoSvcChapter(c));// OverwriteChapter(id);
                }

            }
            else
            {
                using (ReaderServiceSoapClient svc = new ReaderServiceSoapClient())
                {

                    svc.InsertChapter(ProgramStatics.Token, this.ChaptertoSvcChapter(c));// InsertChapter();
                }
            }
        }



        #region Conversions

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


    #region old
    /*
    public List<Source> Sources { get; set; }
    public DatabaseWriter()
    {
        this.Sources = new List<Source>();
    }

    public void loadData(string path)
    {
        if (Directory.Exists(path))
        {
            crawlFolder(path);
        }
    }

    private List<Chapter> createIndex(List<FileInfo> files)
    {
        List<Chapter> sa = new List<Chapter>();
        foreach (FileInfo f in files)
        {
            string contents = File.ReadAllText(f.FullName);
            sa.Add(new Chapter() { ChapterName = f.Name, ChapterText = contents });
        }

        return sa;
    }

    private void crawlFolder(string path)
    {
        //int d = Directory.GetDirectories(path).Length;
        //if (Directory.GetDirectories(path).Length > 0)
        //{
        List<Chapter> cp = new List<Chapter>();

        DirectoryInfo Di = new DirectoryInfo(path);
        // string bs = Di.Parent.Name;
        string bt = Di.Name;


        foreach (string source in Directory.GetDirectories(path))
        {
            DirectoryInfo si = new DirectoryInfo(source);
            Source s = new Source();
            s.SourceName = si.Name;

            foreach (string book in Directory.GetDirectories(source))
            {
                DirectoryInfo bi = new DirectoryInfo(book);
                Book b = new Book();
                b.BookName = bi.Name;


                List<FileInfo> sortedFiles = new DirectoryInfo(book).GetFiles().OrderBy(f => f.LastWriteTime).ToList();

                List<Chapter> chapters = createIndex(sortedFiles);
                b.Chapters = chapters;
                s.Books.Add(b);

                Console.WriteLine($"Book {b} Added to List");
            }
            this.Sources.Add(s);
        }


    }

    private bool BookSourceExists(Source bs)
    {
        foreach (Source b in this.Sources)
        {
            if (b.SourceName.Equals(bs.SourceName))
            {
                return true;
            }
        }
        return false;
    }

    private int getBookSourceIndex(Source bs)
    {
        for (int i = 0; i < this.Sources.Count; i++)
        {
            if (this.Sources[i].SourceName.Equals(bs.SourceName))
            {
                return i;
            }
        }
        return -1;
    }



    public string ConnectionString { get; set; }
    public DatabaseWriter(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    public void createSource(string sourceName, string sourceAddress)
    {
        int sourceIndex = getSourceID(sourceName);
        if (sourceIndex != -1) return;// throw new Exception("Source already exists in database");

        string sCmd = "Insert INTO [Source] ([SourceName], [SourceAddress]) VALUES (@0, @1);";
        try
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sCmd, con))
                {
                    cmd.Parameters.AddWithValue("@0", sourceName);
                    cmd.Parameters.AddWithValue("@1", sourceAddress);
                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    Console.WriteLine($"rows affected {i}");
                }
            }
        }catch(SqlException se)
        {
            return;
        }
    }
    public void createBook(string bookName, string bookSourceName)
    {
        int sourceIndex = getSourceID(bookSourceName);
        if (sourceIndex == -1) throw new Exception("Source Doesnt Exist in Database");
        int bookIndex = this.getBookID(bookName);
        if (bookIndex != -1) return;//throw new Exception("Book already exists in database");

        string sCmd = "Insert INTO [Book] ([BookName], [SourceID]) VALUES ( @0, @1);";
        try
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sCmd, con))
                {
                    cmd.Parameters.AddWithValue("@0", bookName);
                    cmd.Parameters.AddWithValue("@1", sourceIndex);


                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    Console.WriteLine($"rows affected {i}");
                }
            }
        }catch(SqlException se)
        {
            return;
        }

    }



    public void createChapter(string chapterName, string bookName, string content, string chapterAddress, string chapterHTML)
    {
        int bookIndex = getBookID(bookName);
        if (bookIndex == -1) throw new Exception("Book Doesnt Exist in Database");
        int previousChapter = getPreviousChapterID(bookIndex);
        if (previousChapter == -1) previousChapter = 0;
        if (chapterExists(chapterName, bookIndex))
        {
            return;
        }

        string sCmd = "Insert INTO [Chapter] ([ChapterName], [ChapterText], [BookID], [PreviousChapterID], [ChapterAddress], [ChapterHTML] ) VALUES ( @0, @1, @2, @3, @4, @5);";
        try
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sCmd, con))
                {

                    cmd.Parameters.AddWithValue("@0", chapterName);
                    cmd.Parameters.AddWithValue("@1", content);
                    cmd.Parameters.AddWithValue("@2", bookIndex);
                    cmd.Parameters.AddWithValue("@3", previousChapter);
                    cmd.Parameters.AddWithValue("@4", chapterAddress);
                    cmd.Parameters.AddWithValue("@5", chapterHTML);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    Console.WriteLine($"rows affected {i}");
                }
            }
        }catch(SqlException se)
        {
            return;
        }
    }



    private int getSourceID(string bookSourceName)
    {
        try
        {
            int ID;
            string scmd = "SELECT [ID] FROM [Source] WHERE [SourceName] = @0";
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(scmd, conn))
                {

                    cmd.Parameters.AddWithValue("@0", bookSourceName);

                    conn.Open();
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                    if (ID == 0)
                        return -1;
                    return ID;
                }
            }
        }
        catch (SqlException sq)
        {
            return -1;
        }
    }

    private int getPreviousChapterID(int bookIndex)
    {
        try
        {
            string scmd = "SELECT [ID] FROM [Chapter] WHERE [BookID] = @0";
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(scmd, conn))
                {

                    cmd.Parameters.AddWithValue("@0", bookIndex);

                    conn.Open();

                    List<int> ids = new List<int>();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ids.Add(Convert.ToInt32(rdr["ID"]));
                    }
                    if (ids.Count == 0)
                        return -1;
                    return ids.Max();
                }
            }
        }
        catch (SqlException sq)
        {
            return -1;
        }
    }

    private int getBookID(string bookName)
    {
        try
        {
            int ID;
            string scmd = "SELECT [ID] FROM [Book] WHERE [BookName] = @0";
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(scmd, conn))
                {

                    cmd.Parameters.AddWithValue("@0", bookName);

                    conn.Open();

                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                    if (ID == 0)
                        return -1;
                    return ID;
                }
            }
        }
        catch (SqlException sq)
        {
            return -1;
        }
    }

    private bool chapterExists(string chapterName, int bookIndex)
    {
        try
        {
            int ID;
            string scmd = "SELECT [ID] FROM [Chapter] WHERE [ChapterName] = @0 AND [BookID] = @1;";
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(scmd, conn))
                {

                    cmd.Parameters.AddWithValue("@0", chapterName);
                    cmd.Parameters.AddWithValue("@1", bookIndex);

                    conn.Open();

                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                    if (ID == 0)
                        return false;
                    return true;
                }
            }
        }
        catch (SqlException sq)
        {
            throw new Exception($"Something went Wrong!!! {sq.Message}");
        }
    }


}

public class Source
{
    public string SourceName { get; set; }
    public string SourceAddress { get; set; }
    public List<Book> Books { get; set; }
    public Source()
    {
        Books = new List<Book>();
    }
    public override string ToString()
    {
        return this.SourceName;
    }
}

public class Book
{
    public string BookName { get; set; }
    public List<Chapter> Chapters { get; set; }
    public Book()
    {
        Chapters = new List<Chapter>();
    }
    public override string ToString()
    {
        return BookName;
    }
}

public class Chapter
{
    public string ChapterName { get; set; }
    public string ChapterText { get; set; }
    public string ChapterHTML { get; set; }
    public string ChapterAddress { get; set; }

    public override string ToString()
    {
        return this.ChapterName;
    }
}
*/

    #endregion
}


