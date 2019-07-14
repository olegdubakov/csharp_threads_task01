namespace ProducerConsumerSynchronization
{
    interface IFileWriter
    {
        void Write(string text, string fileName);
    }
}
