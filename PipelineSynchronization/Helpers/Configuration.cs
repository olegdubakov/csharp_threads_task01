using System.Collections.Generic;
using PipelineSynchronization.Models;

namespace PipelineSynchronization.Helpers
{
    using System.IO;
    using Interfaces;

    public class Configuration : IConfiguration
    {
        public IEnumerable<Site> GetSiteList()
        {
            var list = new List<Site>();

            foreach (var siteUrl in File.ReadAllLines("data.txt"))
            {
                list.Add(new Site
                {
                    Url = siteUrl
                });
            }

            return list;
        }

        public string JsonFileName =>
            FileNameBuilder
                .GenerateFileName("json", "json");

        public string TextFileName =>
            FileNameBuilder
                .GenerateFileName("txt", "txt");
    }
}
