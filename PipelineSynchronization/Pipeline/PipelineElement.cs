namespace PipelineSynchronization.Pipeline
{
    using System.Collections.Concurrent;
    using Interfaces;

    public class PipelineElement<T> : IPipelineElement<T>
    {
        private readonly BlockingCollection<T> _input;

        private readonly BlockingCollection<T> _output;

        private readonly IProcessor<T> _processor;

        public BlockingCollection<T> Output
        {
            get
            {
                return this._output;
            }
        }

        public PipelineElement(
            BlockingCollection<T> input, 
            IProcessor<T> processor)
        {
            this._input = input;
            this._processor = processor;
            this._output = new BlockingCollection<T>();
        }


        public void Run()
        {
            foreach (var item in this._input.GetConsumingEnumerable())
            {
                var result = this._processor.ProcessItem(item);
                this._output.Add(result);
            }

            this._output.CompleteAdding();
        }
    }
}
