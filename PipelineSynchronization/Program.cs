namespace PipelineSynchronization
{
    using Helpers;
    using Interfaces;
    using Models;
    using Pipeline;
    using Processors;
    using Factories;

    class Program
    {
        static void Main()
        {
            var config = new Configuration();
            var fileWriter = new FileWriter();

            new Pipeline<Site>(
                    config.GetSiteList(), 
                    new PipelineElementFactory<Site>(), 
                    new IProcessor<Site>[]
                    {
                        new SiteReaderProcessor(),
                        new JsonProcessor(fileWriter, config, new CustomHtmlToJsonParser()),
                        new TextProcessor(fileWriter, config, new CustomHtmlToTxtParser())
                    })
                .Run();
        }
    }
}
