using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;


public class ThreadTask
{
    public string username;
    public string proxy;
    public StreamWriter stream_writer;

    public string last_username;

     public ThreadTask(string username, string proxy , StreamWriter stream_writer)
   {
    this.username = username;
    this.proxy = proxy;
    this.stream_writer = stream_writer;
    this.last_username = null;
   }
   public void executeThreadUserWasVerified()
   {
                Client client = new Client(); 
                string url = string.Concat("http://instaram.com/", this.username.Trim());
               
                    try
                    {
                        client.HeadOnly = false;
                        
                        WebProxy proxy = new WebProxy(this.proxy);
                        if(this.last_username!=username)
                        {
                            string source_code = client.DownloadString(url);
                        Console.WriteLine(this.username+" last: "+this.last_username+" ");
                        bool contains = source_code.Contains("is_verified\":false");
                        if(!contains)
                        {
                        stream_writer.WriteLine(this.username+" true");
                        }
                        else
                        {
                        stream_writer.WriteLine(this.username+" false");
                        }
                        
                         stream_writer.Flush();
                        }
                        
                    }
                    catch (Exception error)
                    {
                     Console.WriteLine( "Exception: " + error.Message);
                    }
                    

   }
   public void setLastUsername(string username)
   {
     this.last_username = username;
   }

}
