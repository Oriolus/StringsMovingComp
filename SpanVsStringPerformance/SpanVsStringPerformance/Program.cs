using System;
using BenchmarkDotNet.Running;
using System.Runtime;

namespace SpanVsStringPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //InstanceCreator creator = new InstanceCreator();
            //GC.Collect(2, GCCollectionMode.Forced);
            //Console.WriteLine("Done");
            //Console.ReadLine();

            GCLatencyMode mode = GCSettings.LatencyMode;

            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

            GCSettings.LatencyMode = mode;
            GC.Collect(2, GCCollectionMode.Forced);
            Console.WriteLine("Hello World!");
        }
    }
}
