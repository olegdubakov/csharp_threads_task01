using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedProducerConsumer.Models;

namespace AdvancedProducerConsumer.Interfaces
{
    public interface IPublisher<T>
    {
        void Subscribe(IConsumer<T> consumer);

        void NotifySubscribers(T newSite);

        void WorkIsDone();
    }
}
