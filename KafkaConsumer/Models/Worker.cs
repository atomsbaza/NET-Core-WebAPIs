using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaConsumer.Models
{
    public class Worker
    {
        [BsonId]
        public int Msg_id { get; set; }

        [BsonElement("Name")]
        public string Sender { get; set; }
        public string Msg { get; set; }
        public DateTime Received_Time { get; set; }
    }
}
