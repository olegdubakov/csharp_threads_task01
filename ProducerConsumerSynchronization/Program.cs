namespace ProducerConsumerSynchronization
{
    using System;
    using Helpers;
    using Models;
    using Processors;
    using Processors.ProducerConsumers;

    class Program
    {
        static void Main()
        {
            var configuration = new Configuration();

            new BaseProducerConsumer<Site>(
                new SiteReaderProducer(configuration),
                new JsonProcessorConsumer(
                    new FileWriter(),
                    new CustomHtmlToJsonParser(),
                    configuration))
                .ProcessItems();

            Console.WriteLine();
        }
    }
}
