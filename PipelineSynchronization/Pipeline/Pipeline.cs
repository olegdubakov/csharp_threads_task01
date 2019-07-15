namespace PipelineSynchronization.Pipeline
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Collections.Concurrent;
    using Interfaces;

    public class Pipeline<T> : IPipeline
    {
        private readonly IPipelineElement<T>[] _pipelines;

        public Pipeline(
            IEnumerable<T> initialItems,
            IPipelineElementFactory<T> pipelineElementFactory, 
            IProcessor<T>[] processorsOrder)
        {
            var count = processorsOrder.Length;
            var items = new BlockingCollection<T>(new ConcurrentBag<T>(initialItems));

            items.CompleteAdding();

            this._pipelines = new IPipelineElement<T>[count];

            this._pipelines[0] = pipelineElementFactory
                .BuildPipelineElement(processorsOrder[0], items);

            for (var i = 1; i < count; i++)
            {
                this._pipelines[i] = pipelineElementFactory
                    .BuildPipelineElement(processorsOrder[i], this._pipelines[i - 1].Output);
            }
        }

        public void Run()
        {
            foreach (var pipeline in this._pipelines)
            {
                new Thread(() => pipeline.Run())
                    .Start();
            }
        }
    }
}
