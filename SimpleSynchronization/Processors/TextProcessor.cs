namespace SimpleSynchronization.Processors
{
    using HtmlAgilityPack;
    using Helpers;
    using Models;
    using System;
    using System.Threading;

    internal class TextProcessor
    {
        private readonly string _fileName;

        private readonly FileWriter _writer;

        public TextProcessor(FileWriter writer)
        {
            this._writer = writer;
            this._fileName = FileNameBuilder
                .GenerateFileName("site_text", "json");
        }

        public void WriteSiteTextToFile(Site site)
        {
            Console.WriteLine("{0}: write site to file {1}", Thread.CurrentThread.Name, this._fileName);

            this._writer
                .Write(
                    this.GetTextFromHtml(site.Html), 
                    this._fileName);
        }

        private string GetTextFromHtml(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            return htmlDocument
                .DocumentNode
                .InnerText;
        }
    }
}
