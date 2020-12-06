using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace TestsForTracer
{
    public class Tests
    {
        public static List<Tracer.Tracer.TracerLog> TestOneConsole()
        {
            Tracer.Tracer.Start();
            Tracer.Tracer.BeginTrace();
            OneMethod();
            Tracer.Tracer.EndTrace();
            var wholeResult = Tracer.Tracer.Stop();
            return wholeResult;
        }

        public static List<Tracer.Tracer.TracerLog> TestTwoConsole()
        {
            Tracer.Tracer.Start();
            Tracer.Tracer.BeginTrace();
            SomeMethods();
            Tracer.Tracer.EndTrace();
            var wholeResult = Tracer.Tracer.Stop();

            return wholeResult;
        }

        public static List<Tracer.Tracer.TracerLog> TestThreeConsole()
        {
            Tracer.Tracer.Start();
            Tracer.Tracer.BeginTrace();
            var bgThread = new Thread(ThreadStart);
            bgThread.Start();
            bgThread.Join();
            Tracer.Tracer.EndTrace();
            var wholeResult = Tracer.Tracer.Stop();
            return wholeResult;
        }
        public static List<Tracer.Tracer.TracerLog> TestFourConsole()
        {
            Tracer.Tracer.Start();

            Tracer.Tracer.BeginTrace();
            Tracer.Tracer.EndTrace();


            Tracer.Tracer.BeginTrace();
            Tracer.Tracer.BeginTrace();
            Tracer.Tracer.BeginTrace();

            Thread.Sleep(500);

            var bgThread = new Thread(ThreadStart);
            bgThread.Start();

            SomeMethods();

            Tracer.Tracer.EndTrace();
            Tracer.Tracer.EndTrace();
            Tracer.Tracer.EndTrace();

            bgThread.Join();

            var wholeResult = Tracer.Tracer.Stop();
            return wholeResult;
        }
        static void ThreadStart()
        {
            SomeMethods();
            OneMethod();
        }

        static void SomeMethods()
        {
            Tracer.Tracer.BeginTrace();
            Tracer.Tracer.BeginTrace();

            Thread.Sleep(1100);

            OneMethod();

            Tracer.Tracer.EndTrace();
            Tracer.Tracer.EndTrace();
        }

        static void OneMethod()
        {
            Tracer.Tracer.BeginTrace();
            Thread.Sleep(100);
            Tracer.Tracer.EndTrace();
        }
    }

}
