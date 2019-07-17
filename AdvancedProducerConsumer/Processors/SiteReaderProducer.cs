namespace AdvancedProducerConsumer.Processors
{
    using System;
    using System.Net;
    using Interfaces;
    using Models;

    public class SiteReaderProducer : IProducer<Site>
    {
        private readonly IPublisher<Site> _publisher;

        private readonly IConfiguration _configuration;

        public SiteReaderProducer(
            IPublisher<Site> publisher, 
            IConfiguration configuration)
        {
            this._publisher = publisher;
            this._configuration = configuration;
        }

        public void Produce()
        {
            foreach (var siteUrl in this._configuration.GetSiteList())
            {
                Console.WriteLine("Read HTML ({0})", siteUrl);

                var site = new Site
                {
                    Url = siteUrl,
                    Html = this.GetHtml(siteUrl)
                };

                this._publisher.NotifySubscribers(site);
            }

            this._publisher.WorkIsDone();
        }

        private string GetHtml(string siteUrl)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(siteUrl);
            }
        }
    }
}
