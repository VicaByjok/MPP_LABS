using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tracer.Tests
{
    [TestClass()]
    public class TracerTests
    {
        [TestMethod()]
        public void BeginTraceTest()
        {
            Tracer.Start();
            Tracer.BeginTrace();
            OneMethod();
            Tracer.EndTrace();
            var wholeResult = Tracer.Stop();
            Assert.IsNotNull(wholeResult);
        }

        [TestMethod()]
        public void TraceTest1()
        {
            Tracer.Start();
            Tracer.BeginTrace();
            SomeMethods();
            Tracer.EndTrace();
            var wholeResult = Tracer.Stop();
            double time1 = wholeResult[0].TraceChilds[0].Info.DeltaTime;
            double time2 = wholeResult[0].TraceChilds[0].TraceChilds[0].Info.DeltaTime;
            double time3 = wholeResult[0].TraceChilds[0].TraceChilds[0].TraceChilds[0].Info.DeltaTime;
            double morethan = 1200;
            Assert.IsTrue(time1.CompareTo(morethan)>0 && time2.CompareTo(morethan) > 0 &&time3.CompareTo(morethan) > 0);
        }
        [TestMethod()]
        public void TraceTest2()
        {
            Tracer.Start();
            Tracer.BeginTrace();
            var bgThread = new Thread(ThreadStart);
            bgThread.Start();
            bgThread.Join();
            Tracer.EndTrace();
            var wholeResult = Tracer.Stop();
            Assert.AreEqual(2, wholeResult.Count);
        }
        [TestMethod()]
        public void TraceTest3()
        {
            Tracer.Start();

            Tracer.BeginTrace();
            Tracer.EndTrace();


            Tracer.BeginTrace();
            Tracer.BeginTrace();
            Tracer.BeginTrace();

            Thread.Sleep(500);

            var bgThread = new Thread(ThreadStart);
            bgThread.Start();

            SomeMethods();

            Tracer.EndTrace();
            Tracer.EndTrace();
            Tracer.EndTrace();

            bgThread.Join();

            var wholeResult = Tracer.Stop();
            Tracer.CurrentTraceInfo Traceinfo = wholeResult[0].TraceChilds[1].TraceChilds[0].TraceChilds[0].TraceChilds[0].TraceChilds[0].TraceChilds[0].Info;
            Assert.IsTrue( Traceinfo.MethodName.Equals("OneMethod"));


        }


        static void ThreadStart()
        {
            SomeMethods();
            OneMethod();
        }

        static void SomeMethods()
        {
            Tracer.BeginTrace();
            Tracer.BeginTrace();

            Thread.Sleep(1100);

            OneMethod();

            Tracer.EndTrace();
            Tracer.EndTrace();
        }

        static void OneMethod()
        {
            Tracer.BeginTrace();
            Thread.Sleep(100);
            Tracer.EndTrace();
        }
    }
}
