using System;
using System.IO;
using CoreLib;

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
            Console.ReadLine();
        }
    }
}
