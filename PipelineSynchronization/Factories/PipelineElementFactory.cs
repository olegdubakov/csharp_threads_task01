namespace PipelineSynchronization.Factories
{
    using System.Collections.Concurrent;
    using Interfaces;
    using Pipeline;

    public class PipelineElementFactory<T> : IPipelineElementFactory<T>
    {
        public IPipelineElement<T> BuildPipelineElement(IProcessor<T> processor, BlockingCollection<T> collection)
        {
            return new PipelineElement<T>(collection, processor);
        }
    }
}
