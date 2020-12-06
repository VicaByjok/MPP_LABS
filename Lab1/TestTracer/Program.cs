using System;
using System.Threading;

namespace TestTracer
{
	static class Program
	{
		static void Main()
		{
            Console.WriteLine("Test one");
            var wholeResult = TestsForTracer.Tests.TestOneConsole();
            ISeril XmlSeril = new XmlSeril(wholeResult);
            Console.WriteLine("\nФайл XML {0} сохранён.\n", XmlSeril.Save());
            ISeril JsonSeril = new JsonSeril(wholeResult);
            Console.WriteLine("Файл Json {0} сохранён.\n", JsonSeril.Save());
            ISeril consoleView = new ConsoleView(wholeResult);
            Console.WriteLine(consoleView.Save());
            Console.WriteLine("Test Two");
            wholeResult = TestsForTracer.Tests.TestTwoConsole();
             XmlSeril = new XmlSeril(wholeResult);
            Console.WriteLine("\nФайл XML {0} сохранён.\n", XmlSeril.Save());
             JsonSeril = new JsonSeril(wholeResult);
            Console.WriteLine("Файл Json {0} сохранён.\n", JsonSeril.Save());
             consoleView = new ConsoleView(wholeResult);
            Console.WriteLine(consoleView.Save());
            Console.WriteLine("Test three");
            wholeResult = TestsForTracer.Tests.TestThreeConsole();
             XmlSeril = new XmlSeril(wholeResult);
            Console.WriteLine("\nФайл XML {0} сохранён.\n", XmlSeril.Save());
             JsonSeril = new JsonSeril(wholeResult);
            Console.WriteLine("Файл Json {0} сохранён.\n", JsonSeril.Save());
             consoleView = new ConsoleView(wholeResult);
            Console.WriteLine(consoleView.Save());
            Console.WriteLine("Test four");
            wholeResult = TestsForTracer.Tests.TestFourConsole();
             XmlSeril = new XmlSeril(wholeResult);
            Console.WriteLine("\nФайл XML {0} сохранён.\n", XmlSeril.Save());
             JsonSeril = new JsonSeril(wholeResult);
            Console.WriteLine("Файл Json {0} сохранён.\n", JsonSeril.Save());
             consoleView = new ConsoleView(wholeResult);
            Console.WriteLine(consoleView.Save());

            Console.ReadKey();

		}

	}
}
