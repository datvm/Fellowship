using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fellowship.Server.Models
{
    internal static class Extensions
    {

        public static void EnsureSuccessful(this IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                throw new WebException(response.StatusCode.ToString() + response.Content);
            }
        }

    }
}
