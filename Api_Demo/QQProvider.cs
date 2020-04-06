using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ConsoleApp53
{
   public class QQProvider
    {
        private HttpClient _client;
        public QQProvider(HttpClient client)
        {
            _client = client;
        }
    }
}
