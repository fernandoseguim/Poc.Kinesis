using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Kinesis
{
    public class KinesisPublisherSample
    {
        private readonly AmazonKinesisFirehoseClient _client;
        public KinesisPublisherSample(AmazonKinesisFirehoseClient client) { _client = client; }

        public async Task<string> PutAsync(Message message, string streamName)
        {
            var json = JsonConvert.SerializeObject(message);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var putRecordRequest = new PutRecordRequest { DeliveryStreamName = streamName, Record = new Record { Data = stream } };

                var response = await _client.PutRecordAsync(putRecordRequest);

                return response.RecordId;
            }
        }
    }
}
