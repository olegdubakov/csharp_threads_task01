namespace PipelineSynchronization.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IConfiguration
    {
        IEnumerable<Site> GetSiteList();

        string JsonFileName { get; }

        string TextFileName { get; }
    }
}
