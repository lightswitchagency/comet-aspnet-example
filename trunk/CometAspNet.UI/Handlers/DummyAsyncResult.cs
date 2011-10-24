using System;
using System.Threading;

namespace CometAspNet.UI.Handlers
{
    public class DummyAsyncResult : IAsyncResult
    {
        public DummyAsyncResult(AsyncCallback cb)
        {
            callback = cb;
        }
        private AsyncCallback callback;
        private bool isCompleted = false;
        public object AsyncState
        {
            get
            {
                return null;
            }
        }
        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
        }
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                return null;
            }
        }
        public void CompleteRequest()
        {
            isCompleted = true;
            if (callback != null)
                callback(this);
        }
    }
}
