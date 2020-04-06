using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ConsoleApp53
{
    public class AlipayProvider
    {
        private HttpClient _client;
        public AlipayProvider(HttpClient client)
        {
            _client = client;
        }
    }
}
