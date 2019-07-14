namespace ProducerConsumerSynchronization.Helpers
{
    using System.IO;

    public class Configuration : IConfiguration
    {
        public string[] GetSiteList()
        {
            return File.ReadAllLines("data.txt");
        }

        public string JsonFileName =>
            FileNameBuilder
                .GenerateFileName("json", "json");
    }
}
