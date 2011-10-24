using System;
using System.Collections.Generic;
using System.Linq;

namespace CometAspNet.Core
{
    public class Subscriber
    {
        private class MessageContainer
        {
            public DateTime Timestamp = DateTime.Now;
            public IMessage Message;
        }

        public Subscriber(string id)
        {
            Id = id;
        }
        public string Id { get; private set; }
        public event EventHandler MessagePublished;
        private IList<MessageContainer> messages = new List<MessageContainer>();

        public void Publish(IMessage message)
        {
            messages.Add(new MessageContainer { Message = message });
            if (MessagePublished != null)
            {
                MessagePublished(this, EventArgs.Empty);
            }
        }

        public IList<IMessage> GetMessages(DateTime fromDate)
        {
            return messages
                .Where(e => e.Timestamp > fromDate)
                .Select(e => e.Message)
                .ToList();
        }
    }
}
