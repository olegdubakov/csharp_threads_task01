namespace PipelineSynchronization.Interfaces
{
    using System.Collections.Concurrent;

    public interface IPipelineElementFactory<T>
    {
        IPipelineElement<T> BuildPipelineElement(IProcessor<T> processor, BlockingCollection<T> collection);
    }
}
