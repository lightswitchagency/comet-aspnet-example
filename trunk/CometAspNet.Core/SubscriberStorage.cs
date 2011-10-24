using System;
using System.Collections.Generic;
using System.Linq;
using CometAspNet.Core.Messages;

namespace CometAspNet.Core
{
    public class SubscriberStorage
    {
        public static readonly SubscriberStorage Instance = new SubscriberStorage();
        private readonly IList<Subscriber> subscribers = new List<Subscriber>();

        private SubscriberStorage()
        {

        }

        public Subscriber GetSubscriber(string id)
        {
            lock (subscribers)
            {
                Subscriber subscriber = subscribers.FirstOrDefault(e => e.Id == id);
                if (subscriber == null)
                {
                    foreach (Subscriber oldSubscriber in subscribers)
                    {
                        oldSubscriber.Publish(new NewSubscriberEnteredMessage(id));
                    }
                    subscriber = new Subscriber(id);
                    subscribers.Add(subscriber);
                }
                return subscriber;
            }
        }

        public void PublishForAll(IMessage message)
        {
            lock (subscribers)
            {
                foreach (Subscriber subscriber in subscribers)
                {
                    subscriber.Publish(message);
                }
            }
        }
    }
}
