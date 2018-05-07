using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace IGC
{
    
    class Program
    {
        static void Main(string[] args)
        {
              string filePath = @"list.txt";
              string filePathTemp = @"listoutput.txt";
              
              System.IO.File.WriteAllText(@filePathTemp,string.Empty);
        try 
        {
//FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamWriter stream_writer = new StreamWriter(filePathTemp);
            Proxies proxiesFIFO = new Proxies();
            
            using(FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {  
                              
                using(StreamReader stream_reader = new StreamReader(fs))
                {
                    string lastUsername = null;
                    while(!stream_reader.EndOfStream)
                    {
                      
                        {
                         string username = stream_reader.ReadLine().Trim(); 
                         string proxy = proxiesFIFO.FIFO();
                         ThreadTask threadtask = new ThreadTask( username,  proxy, stream_writer);  
                         threadtask.setLastUsername(lastUsername);
                         Thread executeThreadUserWasVerified = new Thread(new ThreadStart(threadtask.executeThreadUserWasVerified));  
                         executeThreadUserWasVerified.Start(); 
                         lastUsername = username;

                        }
                      
                    }
                }
                fs.Close();
            }    

                
            
            
        } 
        catch (Exception e) 
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
        }
    }
   
}
