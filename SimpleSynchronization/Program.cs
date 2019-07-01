namespace SimpleSynchronization
{
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main()
        {
            new Worker("data.txt")
                .Run();
        }
    }
}
