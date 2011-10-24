namespace CometAspNet.Core.Messages
{
    public class SimpleMessage : IMessage
    {
        public string From { get; set; }
        public string Text { get; set; }
    }
}
