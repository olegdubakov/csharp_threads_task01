namespace AdvancedProducerConsumer.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IConfiguration
    {
        IEnumerable<string> GetSiteList();

        string JsonFileName { get; }

        string TextFileName { get; }
    }
}
