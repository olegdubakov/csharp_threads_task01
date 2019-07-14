namespace PipelineSynchronization.Helpers
{
    using System.IO;
    using Interfaces;

    internal class FileWriter : IFileWriter
    {
        public void Write(string text, string fileName)
        {
            using (var file = new StreamWriter(fileName, true))
            {
                file.WriteLine(text);
                file.Write("\r\n-------------separator-------------\r\n");
            }
        }
    }
}
