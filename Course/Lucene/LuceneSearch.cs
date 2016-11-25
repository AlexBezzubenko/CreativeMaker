using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Course.Models;
using HtmlAgilityPack;
using Version = Lucene.Net.Util.Version;

namespace Course.Lucene
{
    public class LuceneSearch
    {
        const string HeaderId = "HeaderId";
        const string HeaderName = "HeaderName";
        const string Text = "Text";
        const string Tags = "Tags";
        const string UserName = "UserName";
        const string CreativeId = "CreativeId";
        const string CreativeName = "CreativeName";

        public static void BuildIndex(IEnumerable<Header> headers)
        {
            using (var directory = GetDirectory())
            using (var analyzer = GetAnalyzer())
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.DeleteAll();
                foreach (var h in headers)
                {
                    var document = MapHeader(h);
                    writer.AddDocument(document);
                }
                writer.Optimize();
            }
        }

        static Document MapHeader(Header header)
        {
            var document = new Document();

            document.Add(new Field(HeaderId, header.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field(HeaderName, header.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(Text, MarkdownToPlainText(header.Text), 
                                    Field.Store.NO, Field.Index.ANALYZED));
            String tags = "";
            foreach (var t in header.Tags)
            {
                tags += t.Name + " ";
            }
            //String tags = String.Join(" ", header.Tags);
            document.Add(new Field(Tags, tags, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field(UserName, header.Creative.ApplicationUser.UserName, 
                                    Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new NumericField(CreativeId, Field.Store.YES, false).SetLongValue(header.Creative.Id));
            document.Add(new Field(CreativeName, header.Creative.Name, Field.Store.YES, Field.Index.ANALYZED));

            return document;
        }

        public static void AddToIndex(Header h)
        {
            using (var directory = GetDirectory())
            using (var analyzer = GetAnalyzer())
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var document = MapHeader(h);
                writer.AddDocument(document);
            }
        }

        public static void UpdateDocument(Header h)
        {
            using (var directory = GetDirectory())
            using (var analyzer = GetAnalyzer())
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var document = MapHeader(h);
                writer.UpdateDocument(new Term(HeaderId, h.Id.ToString()), document);
            }
        }

        private static string MarkdownToPlainText(string markdown)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(Markdig.Markdown.ToHtml(markdown));

            return htmlDocument.DocumentNode.InnerText;
        }

        static SimpleFSDirectory GetDirectory()
        {
            return new SimpleFSDirectory(new DirectoryInfo(
                Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "lucene_index")));
        }

        static Analyzer GetAnalyzer()
        {
            return new StandardAnalyzer(Version.LUCENE_30);
        }

        public static void DeleteDocument(Header header) 
        {
            using (var directory = GetDirectory())
            using (var analyzer = GetAnalyzer())
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var searchQuery = new TermQuery(new Term(HeaderId, header.Id.ToString()));
                writer.DeleteDocuments(searchQuery);
            }
             
        }

        public static Query GetQuery(string keywords)
        {
            using (var analyzer = GetAnalyzer())
            {
                var parser = new MultiFieldQueryParser
                    (Version.LUCENE_30, new[] { HeaderName, Text, Tags,
                        UserName, CreativeName, }, analyzer);
                var query = parser.Parse(keywords);

                return query;  
            }
        }

        public static List<CreativeResult> Search(string keywords, int limit)
        {
            if (String.IsNullOrEmpty(keywords))
            {
                return new List<CreativeResult>();
            }
            using (var directory = GetDirectory())
            using (var searcher = new IndexSearcher(directory))
            {
                var query = GetQuery(keywords);
                var docs = searcher.Search(query, limit);
                var count = docs.TotalHits;
                var creatives = GetResultsFromDocs(docs, searcher);

                return creatives;
            }
        }

        private static List<CreativeResult> GetResultsFromDocs(TopDocs docs, IndexSearcher searcher)
        {
            var creativeResults = new List<CreativeResult>();
            foreach (var scoreDoc in docs.ScoreDocs)
            {
                var doc = searcher.Doc(scoreDoc.Doc);
                var cr = GetCreativeResultFromDoc(doc);
                creativeResults.Add(cr);
            }
            return creativeResults;
        }

        private static CreativeResult GetCreativeResultFromDoc(Document doc)
        {
            var cr = new CreativeResult
            {
                HeaderId = long.Parse(doc.Get(HeaderId)),
                HeaderName = doc.Get(HeaderName),
                UserName = doc.Get(UserName),
                CreativeId = long.Parse(doc.Get(CreativeId)),
                CreativeName = doc.Get(CreativeName),
            };
            return cr;
        }

    }
}