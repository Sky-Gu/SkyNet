using System;
using SkyNet.Core.Model;

namespace SkyNet.Core.Crawler
{
    public class ConsoleSpiderListening : ISpiderListening
    {
        public void AfterInit(ISpider spider)
        {
            Console.WriteLine("AfterInit");
        }

        public void AfterSuccess(Request request)
        {
            Console.WriteLine($"AfterSuccess :{request.Uri}");
        }

        public void ErrorHandler(Request request, Exception e)
        {
            Console.WriteLine("ErrorHandler");
        }
    }
}