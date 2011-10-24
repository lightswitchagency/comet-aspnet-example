using System.Web;
using CometAspNet.Core;
using CometAspNet.Core.Messages;

namespace CometAspNet.UI.Handlers
{
    public class Send : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string subscriberId = context.Request["SubscriberId"];
            string text = context.Request["Text"];
            SimpleMessage message = new SimpleMessage
            {
                From = subscriberId,
                Text = text
            };
            SubscriberStorage.Instance.PublishForAll(message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}