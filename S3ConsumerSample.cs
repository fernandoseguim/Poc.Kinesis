using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace Poc.Kinesis
{
    public class S3ConsumerSample
    {
        private readonly AmazonS3Client _client;
        public S3ConsumerSample(AmazonS3Client client) { _client = client; }

        public async Task<Message> ConsumeAsync(string bucketName, string objectKey)
        {
            var getObjectRequest = new GetObjectRequest() { BucketName = bucketName, Key = objectKey };

            using (var response = await _client.GetObjectAsync(getObjectRequest))
            using (var responseStream = response.ResponseStream)
            using (var reader = new StreamReader(responseStream))
            {
                var responseBody = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<Message>(responseBody);
            }
        }
    }
}
