namespace SimpleSynchronization.Processors
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Web.Script.Serialization;
    using HtmlAgilityPack;
    using Helpers;
    using Models;

    internal class JsonProcessor
    {
        private readonly string _fileName;

        private readonly FileWriter _writer;

        public JsonProcessor(FileWriter writer)
        {
            this._writer = writer;
            this._fileName = FileNameBuilder
                .GenerateFileName("json", "json");
        }

        public void ParseToJson(Site site)
        {
            Console.WriteLine("{0}: parse site {1} to file {2}", Thread.CurrentThread.Name, site.Url.ToUpper(), this._fileName);

            this._writer
                .Write(
                    this.ParseHtmlToJson(site.Html),
                    this._fileName);
        }

        private string ParseHtmlToJson(string html)
        {
            var doc = new HtmlDocument();
            var serializer = new JavaScriptSerializer();

            doc.LoadHtml(html);

            var json = doc.DocumentNode
                .SelectNodes("//*")
                .GroupBy(node => node.Name)
                .Select(n => new
                {
                    Name = n.Key,
                    Count = n.Count()
                });

            return serializer.Serialize(json);
        }
    }
}
