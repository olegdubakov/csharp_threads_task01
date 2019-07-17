using System.Collections.Concurrent;
using System.Threading;
using AdvancedProducerConsumer.Interfaces;
using AdvancedProducerConsumer.Models;

namespace AdvancedProducerConsumer.Observer
{
    public class BaseSubscriber<T> : ISubscriber<T>
    {
        private readonly BlockingCollection<T> _items;

        public BaseSubscriber(
            IConsumer<T> consumer)
        {
            this._items = new BlockingCollection<T>();

            new Thread(() =>
            {
                foreach (var item in this._items.GetConsumingEnumerable())
                {
                    consumer.Consume(item);
                }
            }).Start();
        }

        public void Update(T item)
        {
            this._items.Add(item);
        }

        public void WorkIsDone()
        {
            this._items.CompleteAdding();
        }
    }
}
