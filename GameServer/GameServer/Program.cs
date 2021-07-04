using System;

namespace GameServer
{
    class Program
    {
        private static void Main()
        {
            AppConfigInit();
            Server.Start(30, 26950);
            Console.ReadKey();
        }
        
        
        private static void AppConfigInit()
        {
            Console.Title = $"Server";
        }
    }
    
}