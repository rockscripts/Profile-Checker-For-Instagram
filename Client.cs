 using System;
 using System.Net;
 class Client : WebClient
    {
        public bool HeadOnly { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest req = base.GetWebRequest(address);

            if (HeadOnly && req.Method == "GET")
            {
                req.Method = "HEAD";
            }
            return req;
        }
    }