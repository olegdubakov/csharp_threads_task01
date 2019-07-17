namespace AdvancedProducerConsumer.Processors
{
    using System;
    using Interfaces;
    using Models;

    internal class JsonProcessor : IConsumer<Site>
    {
        private readonly string _fileName;

        private readonly IFileWriter _writer;

        private readonly IParser _parser;

        public JsonProcessor(
            IFileWriter writer,
            IConfiguration configuration,
            IParser parser)
        {
            this._writer = writer;
            this._parser = parser;
            this._fileName = configuration.JsonFileName;
        }

        public void Consume(Site site)
        {
            Console.WriteLine("Put JSON ({0})", site.Url);

            this._writer
                .Write(this._parser.Parse(site.Html), this._fileName);
        }
    }
}
