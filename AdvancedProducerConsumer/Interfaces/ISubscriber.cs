namespace AdvancedProducerConsumer.Interfaces
{
    interface ISubscriber<T>
    {
        void Update(T item);

        void WorkIsDone();
    }
}
