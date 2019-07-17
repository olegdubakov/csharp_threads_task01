using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedProducerConsumer.Helpers;
using AdvancedProducerConsumer.Interfaces;
using AdvancedProducerConsumer.Models;
using AdvancedProducerConsumer.Observer;
using AdvancedProducerConsumer.Processors;

namespace AdvancedProducerConsumer
{
    class Program
    {
        static void Main()
        {
            var config = new Configuration();
            var fileWriter = new FileWriter();

            var publisher = new Publisher<Site>(new IConsumer<Site>[]
            {
                new JsonProcessor(fileWriter, config, new CustomHtmlToJsonParser()),
                new TextProcessor(fileWriter, config, new CustomHtmlToTxtParser()) 
            });

            var producer = new SiteReaderProducer(publisher, config);

            producer.Produce();
        }
    }
}
