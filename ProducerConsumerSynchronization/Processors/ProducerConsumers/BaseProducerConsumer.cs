namespace ProducerConsumerSynchronization.Processors.ProducerConsumers
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    class BaseProducerConsumer<T> : IProducerConsumer
    {
        private readonly BlockingCollection<T> _blockingCollections;

        private readonly IProducer<T> _producer;

        private readonly IConsumer<T> _consumer;

        public BaseProducerConsumer(
            IProducer<T> producer,
            IConsumer<T> consumer)
        {
            this._producer = producer;
            this._consumer = consumer;
            this._blockingCollections = new BlockingCollection<T>();
        }

        public void ProcessItems()
        {
            new Thread(this.Produce).Start();
            new Thread(this.Consume).Start();
        }

        private void Produce()
        {
            Console.WriteLine("Producer Thread Start");

            while (true)
            {
                var site = this._producer.ProduceNext();

                if (site == null)
                {
                    break;
                }

                this._blockingCollections.Add(site);
            }

            this._blockingCollections.CompleteAdding();

            Console.WriteLine("Producer Thread Finish");
        }
        private void Consume()
        {
            Console.WriteLine("Consumer Thread Start");

            foreach (var item in this._blockingCollections.GetConsumingEnumerable())
            {
                this._consumer.Consume(item);
            }

            Console.WriteLine("Consumer Thread Finish");
        }
    }
}
