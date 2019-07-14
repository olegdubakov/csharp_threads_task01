namespace ProducerConsumerSynchronization.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Models;

    public class SiteReaderProducer : IProducer<Site>
    {
        private readonly Stack<string> _siteUrls;

        public SiteReaderProducer(
            IConfiguration configuration)
        {
            this._siteUrls = new Stack<string>(configuration.GetSiteList());
        }

        public Site ProduceNext()
        {
            try
            {
                var siteUrl = this._siteUrls.Pop();

                using (var client = new WebClient())
                {
                    Console.WriteLine("Read HTML ({0})", siteUrl);
                    var siteHtml = client.DownloadString(siteUrl);

                    return new Site
                    {
                        Url = siteUrl,
                        Html = siteHtml
                    };
                }

            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
