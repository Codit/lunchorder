using System;

namespace Lunchorder.Domain.Entities.Eventing
{
    [Serializable]
    public class Message
    {
        public Message(string type, string jsonPayload)
        {
            Type = type;
            JsonPayload = jsonPayload;
        }

        public string Type { get; set; }
        public string JsonPayload { get; set; }
    }
}
