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
            IProcessor<T>[] processorsOrder)
        {
            var count = processorsOrder.Length;
            var items = new BlockingCollection<T>(new ConcurrentBag<T>(initialItems));

            items.CompleteAdding();

            this._pipelines = new IPipelineElement<T>[count];

            this._pipelines[0] = new PipelineElement<T>(
                items, 
                processorsOrder[0]);

            for (var i = 1; i < count; i++)
            {
                this._pipelines[i] = new PipelineElement<T>(
                    this._pipelines[i-1].Output, 
                    processorsOrder[i]);
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
