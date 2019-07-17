using System.Collections.Generic;
using AdvancedProducerConsumer.Models;

namespace AdvancedProducerConsumer.Helpers
{
    using System.IO;
    using Interfaces;

    public class Configuration : IConfiguration
    {
        public IEnumerable<string> GetSiteList()
        {
            return File.ReadAllLines("data.txt");
        }

        public string JsonFileName =>
            FileNameBuilder
                .GenerateFileName("json", "json");

        public string TextFileName =>
            FileNameBuilder
                .GenerateFileName("txt", "txt");
    }
}
