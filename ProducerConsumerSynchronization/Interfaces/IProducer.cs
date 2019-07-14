namespace ProducerConsumerSynchronization
{
    public interface IProducer<T>
    {
        T ProduceNext();
    }
}
