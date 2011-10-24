namespace CometAspNet.Core.Messages
{
    public class NewSubscriberEnteredMessage : IMessage
    {
        public NewSubscriberEnteredMessage(string name)
        {
            Message = string.Format("Subscriber {0} was entered", name);
        }

        public string Message { get; private set; }
    }
}
