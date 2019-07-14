namespace ProducerConsumerSynchronization
{
    public interface IConfiguration
    {
        string[] GetSiteList();

        string JsonFileName { get; }
    }
}
