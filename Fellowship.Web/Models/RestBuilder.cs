using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Fellowship.Web.Models
{

    public class RestBuilder
    {

        private static string _baseUrl;
        private static Uri baseUri;

        public static string BaseUrl
        {
            get
            {
                return _baseUrl;
            }
            set
            {
                _baseUrl = value;
                baseUri = new Uri(value);
            }
        }

        private HttpClient client;
        public string Resource { get; set; }
        public NameValueCollection QueryParameters { get; set; }
        public NameValueCollection Headers { get; set; }
        public HttpMethod Method { get; set; } = HttpMethod.Get;

        public RestBuilder(HttpClient client)
        {
            this.client = client;
            this.QueryParameters = HttpUtility.ParseQueryString("");
            this.Headers = HttpUtility.ParseQueryString("");
        }

        public RestBuilder(HttpClient client, string resource) : this(client)
        {
            this.Resource = resource;
        }

        public Uri BuildUri()
        {
            Uri uri;
            if (baseUri != null)
            {
                uri = new Uri(baseUri, this.Resource);
            }
            else
            {
                uri = new Uri(this.Resource);
            }

            var builder = new UriBuilder(uri);
            builder.Query = this.QueryParameters.ToString();

            return builder.Uri;
        }

        public RestBuilder AddQueryParameter(string name, string value)
        {
            this.QueryParameters.Add(name, value);
            return this;
        }

        public RestBuilder AddHeader(string name, string value)
        {
            this.Headers.Add(name, value);
            return this;
        }

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            var uri = this.BuildUri();
            var message = new HttpRequestMessage(this.Method, uri);

            foreach (string header in this.Headers)
            {
                message.Headers.Add(header, this.Headers[header]);
            }

            var response = await this.client.SendAsync(message);
            return response;
        }

    }

}
