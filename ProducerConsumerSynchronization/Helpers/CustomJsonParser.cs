namespace ProducerConsumerSynchronization.Helpers
{
    using System.Linq;
    using System.Web.Script.Serialization;
    using HtmlAgilityPack;

    public class CustomHtmlToJsonParser : IJsonParser
    {
        public string Parse(string source)
        {
            var doc = new HtmlDocument();
            var serializer = new JavaScriptSerializer();

            doc.LoadHtml(source);

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