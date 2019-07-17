namespace AdvancedProducerConsumer.Helpers
{
    using HtmlAgilityPack;
    using Interfaces;

    public class CustomHtmlToTxtParser : IParser
    {
        public string Parse(string source)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(source);

            return htmlDocument
                .DocumentNode
                .InnerText;
        }
    }
}