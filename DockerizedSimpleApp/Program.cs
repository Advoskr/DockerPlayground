using System;
using System.IO;
using System.Threading;
using CoreLib;
using EasyNetQ;

namespace DockerizedSimpleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Logging.Configure();
            Logging.LogInfo("This shit works");
            Console.WriteLine("Hello World!");
            Logging.LogInfo("This shit printed");
            var filePath = Directory.GetCurrentDirectory()+"/test.log";
            var fileDesc = File.Create(filePath);
            using (var writer = new StreamWriter(fileDesc))
            {
                writer.WriteLine("Blabla");
            }
            fileDesc.Close();
            Console.WriteLine(filePath);
            Console.WriteLine(Directory.GetCurrentDirectory());
            DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
            var files = d.GetFiles("*"); //Getting Text files
            string str = "";
            foreach (FileInfo file in files)
            {
                str = str + ", " + file.Name;
            }
            Console.WriteLine("Files:"+str);
            Logging.LogInfo("This shit turned off");
            
            var maxRetries = 10;
            for (int tryCount = 0; tryCount < maxRetries; tryCount++)
            {
                Console.WriteLine($"Attempt to connect to db #{tryCount}");
                var (isOk, message) = DbTest.TestConnection("User ID=postgres;Password=postgres;Host=postgres;Port=5432;");
                if (!isOk)
                    Console.WriteLine(message);
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully connected to DB!");
                    Console.ResetColor();
                    break;
                }
            }
            
            //while (true)
            //{
            //    Console.WriteLine($"Checking DB State");
            //    var (isOk, message) = DbTest.TestConnection("User ID=postgres;Password=postgres;Host=postgres;Port=5432;");
            //    if (!isOk)
            //        Console.WriteLine(message);
            //    else
            //    {
            //        Console.WriteLine("Successfully connected to DB!");
            //    }
            //    Thread.Sleep(2000);
            //}
            
            for (int tryCount = 0; tryCount < maxRetries; tryCount++)
            {
                Console.WriteLine($"Try #{tryCount}");
                using (var bus = RabbitHutch.CreateBus("host=rabbitmq;username=rmq;password=azsxdc!2;virtualHost=vhost"))
                {
                    if (!bus.IsConnected)
                        Console.WriteLine("Bus is not connected, check RMQ");
                    else
                    {
                        Console.WriteLine($"Rmq bus status: {(bus.IsConnected ? "ok" : "fail")}");
                        bus.Advanced.QueueDeclare("TestQueue", false, durable: true, exclusive: false,
                            autoDelete: true);
                        break;
                    }
                }
                Thread.Sleep(2000);
            }

            Console.ReadLine();
        }
    }
}
