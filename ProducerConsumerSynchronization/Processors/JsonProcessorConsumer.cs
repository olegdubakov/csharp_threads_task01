namespace ProducerConsumerSynchronization.Processors
{
    using System;
    using Models;

    internal class JsonProcessorConsumer : IConsumer<Site>
    {
        private readonly string _fileName;

        private readonly IFileWriter _writer;

        private readonly IJsonParser _jsonParser;

        public JsonProcessorConsumer(
            IFileWriter writer, 
            IJsonParser jsonParser,
            IConfiguration configuration)
        {
            this._writer = writer;
            this._jsonParser = jsonParser;
            this._fileName = configuration.JsonFileName;
        }
        public void Consume(Site item)
        {
            Console.WriteLine("Put JSON ({0})", item.Url);

            this._writer
                .Write(
                    this._jsonParser.Parse(item.Html),
                    this._fileName);
        }
    }
}
