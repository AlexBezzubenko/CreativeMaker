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
            }
        }

        static Document MapHeader(Header header)
        {
            var document = new Document();

            document.Add(new NumericField("HeaderId", Field.Store.YES, false).SetLongValue(header.Id));
            document.Add(new Field("HeaderName", header.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Text", MarkdownToPlainText(header.Text), Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Tags", String.Join(" ",header.Tags), Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("UserName", header.Creative.ApplicationUser.UserName, 
                                    Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new NumericField("CreativeId", Field.Store.YES, false).SetLongValue(header.Creative.Id));
            document.Add(new Field("CreativeName", header.Creative.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new NumericField("Rating", Field.Store.YES, false).SetDoubleValue(header.Creative.Rating));

            return document;
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

        public static Query GetQuery(string keywords)
        {
            using (var analyzer = GetAnalyzer())
            {
                /*var parser = new QueryParser(Version.LUCENE_30, "HeaderName", analyzer);
                var query = new BooleanQuery();
                var keywordsQuery = parser.Parse(keywords);
                query.Add(keywordsQuery, Occur.MUST);*/

                var parser = new MultiFieldQueryParser
                    (Version.LUCENE_30, new[] { "HeaderName", "Text", "Tags",
                        "UserName", "CreativeName", }, analyzer);
                var query = parser.Parse(keywords);

                return query;  
            }
        }

        public static List<CreativeResult> Search(string keywords, int limit)
        {
            using (var directory = GetDirectory())
            using (var searcher = new IndexSearcher(directory))
            {
                var query = GetQuery(keywords);

                var docs = searcher.Search(query, limit);
                var count = docs.TotalHits;

                var creatives = new List<CreativeResult>();
                foreach (var scoreDoc in docs.ScoreDocs)
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    var creative = new CreativeResult
                    {
                        HeaderId = long.Parse(doc.Get("HeaderId")),
                        HeaderName = doc.Get("HeaderName"),
                        UserName = doc.Get("UserName"),
                        CreativeId = long.Parse(doc.Get("CreativeId")),
                        CreativeName = doc.Get("CreativeName"),
                        Rating = double.Parse(doc.Get("Rating"))
                    };
                    creatives.Add(creative);
                }

                return creatives;
            }
        }
    }
}