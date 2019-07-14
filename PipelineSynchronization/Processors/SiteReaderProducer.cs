namespace PipelineSynchronization.Processors
{
    using System;
    using System.Net;
    using Interfaces;
    using Models;

    public class SiteReaderProcessor : IProcessor<Site>
    {
        public Site ProcessItem(Site site)
        {
            using (var client = new WebClient())
            {
                Console.WriteLine("Read HTML ({0})", site.Url);
                site.Html = client.DownloadString(site.Url);
            }

            return site;
        }
    }
}
