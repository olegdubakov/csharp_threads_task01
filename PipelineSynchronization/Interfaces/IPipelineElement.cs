namespace PipelineSynchronization.Interfaces
{
    using System.Collections.Concurrent;

    public interface IPipelineElement<T> : IPipeline
    {
        BlockingCollection<T> Output { get; }
    }
}
