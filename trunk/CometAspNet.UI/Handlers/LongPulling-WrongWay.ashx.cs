using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CometAspNet.Core;
using CometAspNet.Core.Json;

namespace CometAspNet.UI.Handlers
{
    public class LongPulling_WrongWay : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string subscriberId = context.Request.QueryString["SubscriberId"];
            Subscriber subscriber = SubscriberStorage.Instance.GetSubscriber(subscriberId);
            DateTime startTime = DateTime.Now;

            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            EventHandler messagePublished = null;
            messagePublished = (s, e) =>
            {
                subscriber.MessagePublished -= messagePublished;

                WriteResponse(context, subscriber.GetMessages(startTime));

                manualResetEvent.Set();
            };
            subscriber.MessagePublished += messagePublished;
            manualResetEvent.WaitOne();
        }

        private void WriteResponse(HttpContext context, IList<IMessage> messages)
        {
            string json = (new { messages = messages }).ToJson();

            context.Response.ContentType = "application/json";
            context.Response.Write(json);
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