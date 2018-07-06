using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fellowship.Web.Models
{

    internal static class Utils
    {

        public static RestBuilder Build(this HttpClient client, string resource)
        {
            return new RestBuilder(client, resource);
        }

    }

}
