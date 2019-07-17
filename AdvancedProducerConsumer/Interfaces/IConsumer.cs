namespace AdvancedProducerConsumer.Interfaces
{
    public interface IConsumer<in T>
    {
        void Consume(T item);
    }
}
