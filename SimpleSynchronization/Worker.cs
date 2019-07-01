using SimpleSynchronization.Helpers;

namespace SimpleSynchronization
{
    using System.Collections.Generic;
    using System.Threading;
    using Models;
    using Processors;
    using System.IO;

    internal class Worker
    {
        private readonly string _dataFile;

        private readonly Queue<Site> _fetchedSites;

        private readonly Queue<Site> _savedSites;

        private readonly Thread _dataCollectingThread;

        private readonly Thread _writeToFileThread;

        private readonly Thread _parseSiteThread;

        private readonly FileWriter _writer;

        public Worker(string dataFile)
        {
            this._dataFile = dataFile;
            
            this._fetchedSites = new Queue<Site>();
            this._savedSites = new Queue<Site>();
            this._writer = new FileWriter();

            this._dataCollectingThread = new Thread(this.ReadSites)
            {
                Name = "1 DATA_COLLECTING_THREAD"
            };

            this._writeToFileThread = new Thread(this.WriteSite)
            {
                Name = "2 TXT_PROCESSOR_THREAD"
            };

            this._parseSiteThread = new Thread(this.ParseSite)
            {
                Name = "3 JSON_PROCESSOR_THREAD"
            };
        }

        public void Run()
        {
            this._dataCollectingThread.Start();
            this._writeToFileThread.Start();
            this._parseSiteThread.Start();

            this._parseSiteThread.Join();
            this._writeToFileThread.Join();
            this._dataCollectingThread.Join();
        }

        private void ReadSites()
        {
            var dataCollector = new SiteReader();
            var sites = File.ReadAllLines(this._dataFile);

            foreach (var site in sites)
            {
                lock (this._fetchedSites)
                {
                    this._fetchedSites.Enqueue(new Site
                    {
                        Url = site,
                        Html = dataCollector.GetSite(site)
                    });
                }
            }
        }

        private void WriteSite()
        {
            var run = true;
            var textProcessor = new TextProcessor(this._writer);

            do
            {
                int count;

                lock (this._fetchedSites)
                {
                    count = this._fetchedSites.Count;
                }

                if (count > 0)
                {
                    Site site;

                    lock (this._fetchedSites)
                    {
                        site = this._fetchedSites.Dequeue();
                    }

                    textProcessor.WriteSiteTextToFile(site);

                    lock (this._savedSites)
                    {
                        this._savedSites.Enqueue(site);
                    }
                }

                if (!this._dataCollectingThread.IsAlive && count == 0)
                {
                    run = false;
                }
            } while (run);
        }

        private void ParseSite()
        {
            var run = true;
            var textProcessor = new JsonProcessor(this._writer);

            do
            {
                int count;

                lock (this._savedSites)
                {
                    count = this._savedSites.Count;
                }

                if (count > 0)
                {
                    Site site;

                    lock (this._savedSites)
                    {
                        site = this._savedSites.Dequeue();
                    }

                    textProcessor.ParseToJson(site);
                }

                if (!this._writeToFileThread.IsAlive && count == 0)
                {
                    run = false;
                }

            } while (run);
        }
    }
}
