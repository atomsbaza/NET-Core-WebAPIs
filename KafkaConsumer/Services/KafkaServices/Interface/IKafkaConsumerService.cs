using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.KafkaServices.Interface
{
    public interface IKafkaConsumerService
    {
        Task Consume(CancellationToken cancellationToken);
    }
}
