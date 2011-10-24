using System;
using System.Collections.Generic;
using System.Web;
using CometAspNet.Core;
using CometAspNet.Core.Json;

namespace CometAspNet.UI.Handlers
{
    public class LongPulling : IHttpAsyncHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            string subscriberId = context.Request.QueryString["SubscriberId"];
            Subscriber subscriber = SubscriberStorage.Instance.GetSubscriber(subscriberId);
            DateTime startTime = DateTime.Now;

            DummyAsyncResult asyncResult = new DummyAsyncResult(cb);
            EventHandler messagePublished = null;
            messagePublished = (s, e) =>
            {
                subscriber.MessagePublished -= messagePublished;

                WriteResponse(context, subscriber.GetMessages(startTime));

                asyncResult.CompleteRequest();
            };
            subscriber.MessagePublished += messagePublished;
            return asyncResult;
        }

        private void WriteResponse(HttpContext context, IList<IMessage> messages)
        {
            string json = (new { messages = messages }).ToJson();

            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public void EndProcessRequest(IAsyncResult result)
        {

        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}