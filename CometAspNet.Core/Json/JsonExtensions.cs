using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CometAspNet.Core.Json
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
