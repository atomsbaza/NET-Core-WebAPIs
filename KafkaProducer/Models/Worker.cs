using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KafkaProducer.Models
{
    public class Worker
    {
        [BsonId]
        public string Msg_id { get; set; }

        [BsonElement("Name")]
        public string Sender { get; set; }
        public string Msg { get; set; }
        public DateTime Received_Time { get; set; }
    }
}

