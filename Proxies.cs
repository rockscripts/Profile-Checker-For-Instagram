using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

 class Proxies
    {
         public List<string> proxies;
        public Proxies()
        {
           this.proxies = this.initProxies(); 
        }
        public List<string> initProxies()
        {
            string filePathproxies = @"proxies.txt";
            var proxiesFile = File.ReadAllLines(filePathproxies);
            List<string> proxies = new  List<string>(proxiesFile);
            return proxies;
        }
        public string FIFO()
        {
            string proxy = null;
           if(this.proxies.Count==0)
           {
             this.proxies = this.initProxies();
           }

           if(this.proxies.Count>0) 
           {
              proxy = this.proxies[0];
              this.proxies.RemoveAt(0);
           }
           return proxy;
        }
    }