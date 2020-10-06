using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpApi.Aws.Lambda
{
    public class LambdaProxyResponse
    {
        public bool IsBase64Encoded { get; set; }
        public int StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, List<string>> MultiValueHeaders { get; set; }
        public object Body { get; set; }

        public LambdaProxyResponse(int statusCode, object body = null)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public async Task<Stream> ToJsonStreamAsync()
        {
            var stream = new MemoryStream();

            await JsonSerializer.SerializeAsync(stream, this);

            return stream;
        }
    }
}
