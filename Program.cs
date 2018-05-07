using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace IGC
{
    
    class Program
    {
        static void Main(string[] args)
        {
           List<string> statusList = new List<string>();
           string file_path = @"list.txt";
           string[] lines = System.IO.File.ReadAllLines(file_path);
           Parallel.ForEach(lines, (url) =>
            {
                MyClient client = new MyClient(); 
                //Boolean valid = CheckURL(x);
                // Do something with the result or save it to a List/Dictionary or ...
                string username = url.Trim();
                url = string.Concat("http://instaram.com/", url.Trim());

               
                    try
                    {
                        client.HeadOnly = false;
                        
                        WebProxy proxy = new WebProxy("104.143.198.29:3128");
                        client.Proxy = proxy;

                        // fine, no content downloaded
                        string source_code = client.DownloadString(url);
                        bool contains = source_code.Contains("is_verified\":false");
                        if(!contains)
                        {
                         statusList.Add(username+" true");
                        }
                        else
                        {
                         statusList.Add(username+" false");
                        }
                        
                    }
                    catch (Exception error)
                    {
                     Console.WriteLine( "Not Found: " + url);
                    }
                
            });
             
            string [] new_lines = statusList.ToArray();
           // Console.WriteLine(new_lines);
            foreach(string line in new_lines)
        {
            Console.WriteLine(line);
        }
            File.WriteAllLines("new.txt", new_lines);
        }
    }
    class MyClient : WebClient
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
}
