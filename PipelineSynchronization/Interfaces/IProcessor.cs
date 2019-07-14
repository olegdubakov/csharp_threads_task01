namespace PipelineSynchronization.Interfaces
{
    public interface IProcessor<T>
    {
        T ProcessItem(T item);
    }
}
