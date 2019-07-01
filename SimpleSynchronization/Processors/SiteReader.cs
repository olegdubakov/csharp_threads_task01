namespace SimpleSynchronization.Processors
{
    using System;
    using System.Net;
    using System.Threading;

    internal class SiteReader
    {
        public string GetSite(string url)
        {
            Console.WriteLine("{0}: get html from {1}", Thread.CurrentThread.Name, url);

            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
