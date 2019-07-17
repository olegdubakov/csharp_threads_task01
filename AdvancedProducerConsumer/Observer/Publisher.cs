using System.Collections.Generic;
using AdvancedProducerConsumer.Interfaces;

namespace AdvancedProducerConsumer.Observer
{
    public class Publisher<T> : IPublisher<T>
    {
        private List<ISubscriber<T>> _subscribers;

        public Publisher(
            IEnumerable<IConsumer<T>> consumers)
        {
            this._subscribers = new List<ISubscriber<T>>();

            foreach (var consumer in consumers)
            {
                this._subscribers.Add(new BaseSubscriber<T>(consumer));
            }
        }

        public void Subscribe(IConsumer<T> consumer)
        {
            this._subscribers.Add(new BaseSubscriber<T>(consumer));
        }

        public void NotifySubscribers(T newItem)
        {
            foreach (var subscriber in this._subscribers)
            {
                subscriber.Update(newItem);
            }
        }

        public void WorkIsDone()
        {
            foreach (var subscriber in this._subscribers)
            {
                subscriber.WorkIsDone();
            }
        }
    }
}
