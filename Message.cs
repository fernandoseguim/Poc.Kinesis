using System;
using System.Collections.Generic;

namespace Poc.Kinesis
{
    [Serializable]
    public class Message
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}