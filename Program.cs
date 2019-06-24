using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace Poc.Kinesis
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string AWS_ACCESS_KEY_ID = "AWS_ACCESS_KEY_ID"; 
            const string AWS_SECRET_ACCESS_KEY = "AWS_SECRET_ACCESS_KEY"; 

            var firehoseClient = new AmazonKinesisFirehoseClient(AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, RegionEndpoint.USWest2);

            var message = new Message
            {
                Name = "Gabriela Orselli",
                Document = "22733611810",
                Phone = "+5511953494000",
                Metadata = new Dictionary<string, string> { { "amount", "250.00" } }
            };

            var kinesisPublisher = new KinesisPublisherSample(firehoseClient);

            var recordid = await kinesisPublisher.PutAsync(message, "poc-firehore-stream");

            Console.WriteLine($"Record Id {recordid}");
            

            //var s3Client = new AmazonS3Client(AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, RegionEndpoint.USWest2);

            //var s3Consumer = new S3ConsumerSample(s3Client);

            //var s3ResponseMessage = await s3Consumer.ConsumeAsync("poc-kinesis-firehose", "pkfs-2019/06/14/01/poc-firehore-stream-2-2019-06-14-01-41-18-c4289cfc-b357-4b8f-9a59-28a4d667169c");

            //Console.WriteLine($"Reponse document: {s3ResponseMessage.Document}");
            //Console.WriteLine($"Reponse document: {s3ResponseMessage.Name}");
            //Console.WriteLine($"Reponse document: {s3ResponseMessage.Phone}");

            //foreach (var metadata in s3ResponseMessage.Metadata)
            //{
            //    Console.WriteLine($"Reponse metadata: {metadata}");
            //}
        }
    }
}
