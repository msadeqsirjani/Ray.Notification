using Microsoft.Owin.Hosting;
using System;

namespace Ray.Notification.Service
{
    public class Program
    {
        public static void Main()
        {
            using (WebApp.Start<Startup>("http://localhost:11111"))
            {
                Console.ReadLine();
            }
        }
    }
}
